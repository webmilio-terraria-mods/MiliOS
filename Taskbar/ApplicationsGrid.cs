using MiliOS.Softwares.Donut;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using WebmilioCommons.Extensions;

namespace MiliOS.Taskbar;

public class ApplicationsGrid : UIGrid
{
    private IApplicationIconSubscriber _subscriber;

    public ApplicationsGrid(IApplicationIconSubscriber subscriber)
    {
        _subscriber = subscriber;

        Width = StyleDimension.Fill;
        Height = StyleDimension.Fill;

        ListPadding = 12;

        new DonutTaskbarDescriptor[]
        {
            new(),
            new(),
            new()
        }.Do(AddDescriptor);
    }

    private void AddDescriptor(TaskbarDescriptor descriptor)
    {
        _items.Add(descriptor);
        Append(descriptor);

        descriptor.OnMouseOver += Descriptor_OnMouseOver;
        descriptor.OnMouseOut += Descriptor_OnMouseOut;
        descriptor.OnClick += Descriptor_OnClick;
    }

    private void Descriptor_OnMouseOver(UIMouseEvent evt, UIElement element) => _subscriber.HoveredApplicationChanged((TaskbarDescriptor) element);
    private void Descriptor_OnMouseOut(UIMouseEvent evt, UIElement element) => _subscriber.HoveredApplicationChanged(null);

    private void Descriptor_OnClick(UIMouseEvent evt, UIElement listeningelement) => _subscriber.HoveredApplicationChanged(null);

    private void RemoveDescriptor(TaskbarDescriptor descriptor)
    {
        descriptor.OnMouseOver -= Descriptor_OnMouseOver;
        descriptor.OnMouseOut -= Descriptor_OnMouseOut;
        descriptor.OnClick -= Descriptor_OnClick;
    }
}