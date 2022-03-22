using System;
using Microsoft.Xna.Framework;
using MiliOS.Interfaces;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace MiliOS.Taskbar;

public class TaskbarPanel : UIPanel
{
    private const float
        HiddenVAlign = 1.11f,
        ShownVAlign = .99f;

    private const int 
        _Padding = 5,
        _Height = TaskBar.IconSize + _Padding * 2;

    private float _destination = HiddenVAlign;

    private readonly ApplicationsGrid _applications;

    public TaskbarPanel(IApplicationIconSubscriber subscriber)
    {
        VAlign = 1;
        HAlign = .5f;

        Width = new(0, .92f);
        Height = new(_Height, 0);

        SetPadding(_Padding);
        PaddingLeft = 15;
        PaddingRight = 15;

        Append(_applications = new(subscriber));
    }

    public override void Update(GameTime gameTime)
    {
        if (ContainsPoint(Main.MouseScreen))
        {
            Main.LocalPlayer.mouseInterface = true;
        }

        if (Math.Abs(VAlign - _destination) > 0.0001f)
        {
            const float minSpeed = 0.00005f;
            const float y = 0.05f - minSpeed;

            float delta = y * (_destination - VAlign);

            if (delta < 0)
                delta -= minSpeed;
            else
                delta += minSpeed;

            VAlign += delta;
            Recalculate();
        }

        base.Update(gameTime);
    }

    public void ToggleState()
    {
        _destination = _destination == ShownVAlign ?
            HiddenVAlign : ShownVAlign;
    }
}

public class TaskBar : UIState, IApplicationIconSubscriber
{
    public const int IconSize = 64;

    private TaskbarPanel _container;
    private Tooltip _tooltipContainer;

    public TaskBar()
    {
        Append(_container = new(this));
    }

    public void ToggleHidden()
    {
        _container.ToggleState();
    }

    public void HoveredApplicationChanged(TaskbarDescriptor icon)
    {
        if (icon == null)
        {
            if (_tooltipContainer != null)
            {
                RemoveChild(_tooltipContainer);
                _tooltipContainer = null;
            }

            return;
        }

        _tooltipContainer = new(icon.GetTooltip())
        {
            XOffset = t => -t.GetDimensions().Width / 2,
            YOffset = _ => -50
        };
        Append(_tooltipContainer);
    }
}