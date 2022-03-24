using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace MiliOS.Applications.AppBrowser.Interface;

public class Entry : UIPanel
{
    public Entry(IApplicationDescriptor descriptor)
    {
        Descriptor = descriptor;

        Width = new(150, 0);
        Height = new(150, 0);

        BorderColor = BackgroundColor = Color.Transparent;

        Append(new UIImage(Descriptor.Icon)
        {
            HAlign = .5f,
            
            Width = new(Taskbar.Taskbar.IconSize, 0),
            Height = new(Taskbar.Taskbar.IconSize, 0),

            IgnoresMouseInteraction = true
        });

        Append(new UIText(Descriptor.Name)
        {
            HAlign = .5f,
            VAlign = 1,

            Width = StyleDimension.Fill,
            Height = new(100 - Taskbar.Taskbar.IconSize, 0),

            IsWrapped = true
        });
    }

    public IApplicationDescriptor Descriptor { get; }
}