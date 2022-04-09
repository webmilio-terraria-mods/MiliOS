using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using WebmilioCommons.Extensions;

namespace MiliOS.Explorer.Wrapper;

internal class AppContainer : UIPanel
{
    private readonly Explorer _explorer;

    public AppContainer(Explorer explorer)
    {
        _explorer = explorer;
    }
}