using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using WebmilioCommons.DependencyInjection;
using WebmilioCommons.Helpers;

namespace MiliOS.Applications.TaskManager;

public class TaskManager : Application
{
    private Explorer.Explorer _explorer;
    private UIElement _state;

    public override bool Launch(Explorer.Explorer explorer, Player caller, object[] args)
    {
        State = 1;
        
        _explorer = explorer;
        _state = new State(_explorer);

        Maximize();
        return true;
    }

    public override void Maximize()
    {
        _state = _explorer.SignalApplicationInterface(this, _state);
        Maximized = true;
    }

    public override void Minimize()
    {
        _explorer.SignalTerminateInterface(this, _state);
        Maximized = false;
    }

    public sealed class Descriptor : ApplicationDescriptor<TaskManager>
    {
        public Descriptor() : base(
            LocalizedTextHelpers.GetModKey(nameof(MiliOS), $"Applications.{nameof(TaskManager)}.Name"),
            () => MiliOS.Instance.Assets.Request<Texture2D>($"Applications/{nameof(TaskManager)}/icon"))
        {
            AppIndex = int.MinValue + 1;
        }
    }
}