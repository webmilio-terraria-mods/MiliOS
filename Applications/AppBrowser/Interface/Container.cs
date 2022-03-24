using Terraria.UI;

namespace MiliOS.Applications.AppBrowser.Interface;

public class Container : UI.UIPanel
{
    public Container(AppBrowser browser)
    {
        Browser = browser;

        Append(Grid = new()
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill
        });

        CloseWhenClickOutside = true;
    }

    public override void OutsideClose()
    {
        Browser.Exit();
    }

    public AppBrowser Browser { get; }

    public Grid Grid { get; }
}