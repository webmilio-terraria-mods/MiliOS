using Microsoft.Xna.Framework;
using MiliOS.Applications;
using MiliOS.Explorer.Wrapper;
using Terraria.UI;

namespace MiliOS.Explorer;

public interface IExplorerWrapInPanel : IExplorerAutoPosition
{
    public void WrapInPanel(Explorer container, IApplication application, IApplicationDescriptor descriptor, ref UIElement element)
    {
        WrapperPanel wrapper = new(container, application, descriptor, element)
        {
            BackgroundColor = WrapperBackgroundColor,
            BorderColor = WrapperBorderColor
        };

        container.RemoveElement(element);
        wrapper.AppContainer.Append(element);

        element = wrapper;
        container.Append(wrapper);
    }

    public Color WrapperBackgroundColor => Color.Gray;
    public Color WrapperBorderColor => Color.Black;
}