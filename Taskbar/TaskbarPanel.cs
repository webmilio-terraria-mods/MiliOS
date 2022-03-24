using MiliOS.Applications;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;

namespace MiliOS.Taskbar;

public class TaskbarPanel : UIPanel
{
    private readonly TaskbarGrid _grid;

    public TaskbarPanel()
    {
        Append(_grid = new());
    }

    public void BuildTaskbar(IList<IApplicationDescriptor> descriptors) => _grid.BuildTaskbar(descriptors);
}