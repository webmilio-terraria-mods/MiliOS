using Microsoft.Xna.Framework;

namespace MiliOS.Explorer.Wrapper;

public class WrapperSettings : IWrapperSettings
{
    public Color BackgroundColor { get; set; } = Color.Gray;
    public Color BorderColor { get; set; } = Color.Black;

    public bool ShowMinimize { get; set; } = true;
    public bool ShowClose { get; set; } = true;
}