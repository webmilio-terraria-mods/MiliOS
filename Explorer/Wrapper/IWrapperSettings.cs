using Microsoft.Xna.Framework;

namespace MiliOS.Explorer.Wrapper;

public interface IWrapperSettings
{
    public Color BackgroundColor => Color.Gray;
    public Color BorderColor => Color.Black;

    public bool ShowMinimize => true;
    public bool ShowClose => true;
}