using System.Collections.Generic;
using MiliOS.Applications;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using WebmilioCommons.Extensions;

namespace MiliOS.Taskbar;

public class TaskbarGrid : UIGrid
{
    public TaskbarGrid()
    {
        Width = StyleDimension.Fill;
        Height = StyleDimension.Fill;

        ListPadding = 12;
    }

    public void BuildTaskbar(IList<IApplicationDescriptor> descriptors)
    {
        ClearTaskbar();

        descriptors.Do(AddApplication);
    }

    public void AddApplication(IApplicationDescriptor descriptor)
    {
        TaskbarDescriptor element = new(descriptor)
        {
            OverflowHidden = false
        };

        _items.Add(element);
        Append(element);
    }

    public void RemoveApplication(IApplicationDescriptor descriptor)
    {
        var element = _items.Find(delegate(UIElement element)
        {
            if (element is TaskbarDescriptor te)
                return te.Descriptor == descriptor;

            return false;
        });

        if (element == null)
            return;

        RemoveDescriptor(element as TaskbarDescriptor);
    }

    public void RemoveApplication(int index) => RemoveDescriptor(_items[index] as TaskbarDescriptor);

    private void RemoveDescriptor(TaskbarDescriptor descriptor)
    {
        _items.Remove(descriptor);
        RemoveChild(descriptor);
    }

    public void ClearTaskbar()
    {
        _items.DoInverted((_, i) => RemoveApplication(i));
    }
}