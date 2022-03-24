using MiliOS.Applications;
using Terraria;
using Terraria.ModLoader;
using WebmilioCommons.Players;

namespace MiliOS.Players;

public class User : BetterModPlayer
{
    public override void OnEnterWorld(Player player)
    {
        // TODO Change this to follow the user's favorites and instantiate all apps upon entering a world.
        var services = MiliOS.Instance.Services;

        var appsLoader = services.GetService<DescriptorsLoader>();
        var appsRepo = services.GetService<ApplicationsRepository>();

        appsRepo.Clear();
        appsRepo.Install(appsLoader.Generics);

        ModContent.GetInstance<MiliOSSystem>().Explorer.Taskbar.BuildTaskbar(appsRepo.Installed);
    }
}