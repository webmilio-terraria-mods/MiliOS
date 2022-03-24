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
        Services
            .MapServices(typeof(MiliOS).Assembly)
            .AddSingleton(_ => ModContent.GetInstance<MiliOSSystem>())
            
            .AddContainer(WebmilioCommonsMod.CommonServices);
    }

    public override void Unload()
    {
        Services = null;
    }

    public SimpleServices Services { get; private set; }

    public static MiliOS Instance { get; private set; }
}