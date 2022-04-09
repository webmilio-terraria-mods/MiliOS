using MiliOS.Applications;
using MiliOS.Explorer.Wrapper;
using Terraria.UI;

namespace MiliOS.Explorer;

public interface IExplorerWrapInPanel
{
    public WrapperPanel WrapInPanel(Explorer container, IApplication application, IApplicationDescriptor descriptor, ref UIElement element)
    {
        WrapperPanel wrapper = new(container, application, descriptor, WrapperSettings, element)
        {
            BackgroundColor = WrapperSettings.BackgroundColor,
            BorderColor = WrapperSettings.BorderColor
        };

        container.RemoveElement(element);
        wrapper.AppContainer.Append(element);

        element = wrapper;
        container.Append(wrapper);

        OnWrapped(wrapper);
        return wrapper;
    }

    public void OnWrapped(WrapperPanel wrapper) { }

    public IWrapperSettings WrapperSettings { get; }
}