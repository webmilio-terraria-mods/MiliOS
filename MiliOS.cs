using Terraria.ModLoader;
using WebmilioCommons;
using WebmilioCommons.DependencyInjection;

namespace MiliOS;

public class MiliOS : Mod
{
    public MiliOS()
    {
        Instance = this;

        Services = new();
        Services.AddSingleton(_ => ModContent.GetInstance<MiliOSSystem>());

        WebmilioCommonsMod.CommonServices.AddContainer(Services);
    }

    public override void Unload()
    {
        Services = null;
    }

    public SimpleServices Services { get; private set; }

    public static MiliOS Instance { get; private set; }
}