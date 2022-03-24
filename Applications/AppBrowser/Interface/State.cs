using System.Collections.Generic;
using Terraria.UI;

namespace MiliOS.Applications.AppBrowser.Interface;

public class State : UIState
{
    public State(AppBrowser browser, IList<IApplicationDescriptor> descriptors)
    {
        VAlign = .5f;
        HAlign = .5f;

        Width = new(0, .5f);
        Height = new(0, .75f);

        Container container = new(browser)
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill,

            BackgroundColor = new(23 / 255f, 7 / 255f, 6 / 255f, .5f)
        };

        Append(container);
        container.Grid.BuildGrid(descriptors);
    }
}