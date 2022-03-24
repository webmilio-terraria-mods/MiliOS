using Microsoft.Xna.Framework;
using MiliOS.Explorer;
using MiliOS.UI;
using Terraria.UI;

namespace MiliOS.Applications.TaskManager;

public class State : UIState, IExplorerWrapInPanel
{
    public const int
        CWidth = 500,
        CHeight = 650;

    private readonly Explorer.Explorer _explorer;

    public State(Explorer.Explorer explorer)
    {
        _explorer = explorer;

        Width = StyleDimension.FromPixels(CWidth);
        Height = StyleDimension.FromPixels(CHeight);

        Append(new TasksList(_explorer)
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill
        });
    }
}