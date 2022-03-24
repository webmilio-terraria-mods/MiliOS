using Microsoft.Xna.Framework;
using MiliOS.Applications;
using MiliOS.UI.Menues;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MiliOS.Explorer.Wrapper;
using Terraria;
using Terraria.UI;
using WebmilioCommons.DependencyInjection;
using WebmilioCommons.Extensions;

namespace MiliOS.Explorer;

// TODO Split this into an Explorer class and an internal ExplorerState class.
public class Explorer : UIState
{
    private readonly List<IApplication> _running = new(); // Redundant listing to keep a list of references.
    private readonly Dictionary<IApplication, List<UIElement>> _states = new();

    private readonly List<UIElement> _children = new();

    public const int StartAutoPosition = 50;
    private Vector2 _lastAutoPosition = new(StartAutoPosition, StartAutoPosition);

    public Explorer()
    {
        Services = MiliOS.Instance.Services;
        Descriptors = Services.GetService<DescriptorsLoader>();

        Width = Height = StyleDimension.Fill;

        Append(Taskbar = new()
        {
            Width = new(0, .7f),
            Height = new(global::MiliOS.Taskbar.Taskbar.CHeight, 0),

            HAlign = .5f
        });

        RunningApplications = _running.AsReadOnly();
    }

    #region Launch/Size

    public bool TryMaximizeMinimizeOrLaunch(IApplicationDescriptor descriptor, Player player, object[] args)
    {
        if (TryMaximizeMinimize(descriptor))
            return true;

        return TryLaunch(descriptor, player, args);
    }

    public bool TryMaximizeMinimize(IApplicationDescriptor descriptor)
    {
        var running = _running.Find(app => descriptor.Describes == app.GetType());

        if (running == null)
            return false;

        if (running.Maximized)
            running.Minimize();
        else
            running.Maximize();

        return true;

    }

    public bool TryLaunch(IApplicationDescriptor descriptor, Player player, object[] args)
    {
        return Services.Make(descriptor.Describes) is IApplication application &&
               TryLaunch(application, player, args);
    }

    public bool TryLaunch(IApplication application, Player player, object[] args)
    {
        if (!application.Launch(this, player, args))
            return false;

        _running.Add(application);
        return true;
    }

    public UIElement SignalApplicationInterface(IApplication application, UIElement element)
    {
        AddElement(element);

        if (element is IExplorerAutoPosition eap) eap.AutoPosition(this, element, ref _lastAutoPosition);
        if (element is IExplorerWrapInPanel wip) wip.WrapInPanel(this, application, Descriptors.Get(application), ref element);

        _states.AddOrGet(application, out var states, () => new());
        states.Add(element);

        return element;
    }

    #endregion

    #region Terminate

    public bool TerminateApplication(IApplicationDescriptor descriptor)
    {
        for (int i = _running.Count - 1; i >= 0; i--)
        {
            var app = _running[i];

            if (descriptor.Describes == app.GetType())
                if (!TerminateApplication(app))
                    return false;
        }

        return true;
    }

    public bool TerminateApplication(IApplication application)
    {
        if (application.State > 0 && !application.Exit())
            return false;

        if (_states.TryGetValue(application, out var states))
            RemoveElements(states);

        _states.Remove(application);
        _running.Remove(application);

        return true;
    }

    public bool SignalTerminateInterface(IApplication application, UIElement state)
    {
        if (state is IExplorerWrapInPanel wip && _states.TryGetValue(application, out var states))
        {
            var tmpState = states.Find(element => element is WrapperPanel wp && wp.Wrapped == state);

            if (tmpState != null)
                state = tmpState;
        }

        if (!_states.TryGetValue(application, out states) ||
            !states.Remove(state))
            return false;

        if (states.Count == 0)
            _states.Remove(application);

        RemoveElement(state);
        return true;
    }

    #endregion

    public void ForRunning(Action<IApplication> action) => _running.Do(action);

    public override void Update(GameTime gameTime)
    {
        _running.DoInverted(delegate (IApplication application)
        {
            if (application.State > 0)
                return;

            TerminateApplication(application);
        });

        base.Update(gameTime);
    }

    #region UI

    public void AddContextMenu(ContextMenu menu) => AddContextMenu(menu, true);
    public void AddContextMenu(ContextMenu menu, bool unique)
    {
        if (unique)
            RemoveElements<ContextMenu>();

        AddElement(menu);
    }

    public void AddElement(UIElement element)
    {
        _children.Add(element);
        Append(element);
    }

    protected override void DrawChildren(SpriteBatch spriteBatch)
    {
        base.DrawChildren(spriteBatch);

        ForRunning(application => application.Draw(spriteBatch));
    }

    public void RemoveElements<T>(IList<T> elements) where T : UIElement => elements.Do(RemoveElement);

    public void RemoveElement(UIElement element)
    {
        RemoveChild(element);
        _children.Remove(element);

        Recalculate();
    }

    public void RemoveElement<T>(Predicate<T> condition)
    {
        var element = _children.Find(delegate (UIElement uiElement)
        {
            if (uiElement is T t && condition(t))
                return true;

            return false;
        });

        RemoveElement(element);
    }

    public void RemoveElements<T>()
    {
        _children.DoInverted(delegate (UIElement element)
        {
            if (element is T)
                _RemoveElement(element);
        });

        Recalculate();
    }

    private void _RemoveElement(UIElement element)
    {
        RemoveChild(element);
        _children.Remove(element);
    }

    public void Focus(UIElement element)
    {
        Elements.Move(element, Elements.Count - 1);
    }

    #endregion

    public SimpleServices Services { get; }
    public DescriptorsLoader Descriptors { get; }

    public Taskbar.Taskbar Taskbar { get; }

    public IReadOnlyCollection<IApplication> RunningApplications { get; }
}