using Microsoft.Xna.Framework.Graphics;
using MiliOS.Applications.AppBrowser.Interface;
using Terraria;
using WebmilioCommons.Helpers;

namespace MiliOS.Applications.AppBrowser;

public class AppBrowser : Application
{
    private readonly ApplicationsRepository _repository;

    public AppBrowser(ApplicationsRepository repository)
    {
        _repository = repository;
    }

    public override bool Launch(Explorer.Explorer explorer, Player caller, object[] args)
    {
        State = 1;

        explorer.SignalApplicationInterface(this, new State(this, _repository.Installed));
        Maximized = true;

        return true;
    }

    public override void Minimize()
    {
        Exit();
    }

    public sealed class Descriptor : ApplicationDescriptor<AppBrowser>
    {
        public Descriptor() : base(
            LocalizedTextHelpers.GetModKey(nameof(MiliOS), $"Applications.{nameof(AppBrowser)}.Name"),
            () => MiliOS.Instance.Assets.Request<Texture2D>($"Applications/{nameof(AppBrowser)}/icon")
            )
        {
            InAppBrowser = false;

            AppIndex = int.MinValue;
        }
    }
}