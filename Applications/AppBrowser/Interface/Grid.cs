using System.Collections.Generic;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using WebmilioCommons.Extensions;

namespace MiliOS.Applications.AppBrowser.Interface;

public class Grid : UIGrid
{
    public void BuildGrid(IList<IApplicationDescriptor> descriptors)
    {
        ClearTaskbar();
        
        descriptors.Do(delegate(IApplicationDescriptor descriptor)
        {
            if (descriptor.InAppBrowser)
                AddApplication(descriptor);
        });
    }

    public void AddApplication(IApplicationDescriptor descriptor)
    {
        Entry entry = new(descriptor);

        _items.Add(entry);
        Append(entry);
    }

    public void RemoveApplication(IApplicationDescriptor descriptor)
    {
        var element = _items.Find(delegate (UIElement element)
        {
            if (element is Entry e)
                return e.Descriptor == descriptor;

            return false;
        });

        if (descriptor == null)
            return;

        RemoveDescriptor(element as Entry);
    }

    public void RemoveApplication(int index) => RemoveDescriptor(_items[index] as Entry);

    private void RemoveDescriptor(Entry descriptor)
    {
        _items.Remove(descriptor);
        RemoveChild(descriptor);
    }

    public void ClearTaskbar()
    {
        _items.DoInverted((_, i) => RemoveApplication(i));
    }
}