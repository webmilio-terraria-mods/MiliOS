using Microsoft.Xna.Framework.Graphics;
using Terraria;
using WebmilioCommons.Helpers;

namespace MiliOS.Applications.Donut;

public class DonutApplication : Application
{
    public override bool Launch(Explorer.Explorer explorer, Player caller, object[] args)
    {
        Main.NewText("Allo Chad!");

        return false;
    }

    //public override IApplicationDescriptor Descriptor { get; } =
    //    new ApplicationDescriptor(LocalizedTextHelpers.GetModKey(nameof(MiliOS), "Applications.Donut.Name"), 
    //        MiliOS.Instance.Assets.Request<Texture2D>("Applications/Donut/icon"));

    public sealed class Descriptor : ApplicationDescriptor<DonutApplication>
    {
        public Descriptor() : base(
            LocalizedTextHelpers.GetModKey(nameof(MiliOS), $"Applications.{nameof(Donut)}.Name"),
            MiliOS.Instance.Assets.Request<Texture2D>($"Applications/{nameof(Donut)}/icon")
            )
        {

        }
    }
}