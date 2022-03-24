using Microsoft.Xna.Framework;
using MiliOS.UI;
using Terraria.UI;

namespace MiliOS.Explorer;

public interface IExplorerAutoPosition
{
    public void AutoPosition(UIElement container, UIElement element, ref Vector2 lastPosition)
    {
        element.Left = new(lastPosition.X, 0);
        element.Top = new(lastPosition.Y, 0);

        element.Recalculate();

        InterfacesHelper.FitInState(element);

        var elementDimensions = element.GetDimensions();

        lastPosition.X += elementDimensions.Width / 2;
        lastPosition.Y += elementDimensions.Height / 2;

        var dimensions = container.GetDimensions();

        if (lastPosition.X > dimensions.Width) lastPosition.X = Explorer.StartAutoPosition;
        if (lastPosition.Y > dimensions.Height) lastPosition.Y = Explorer.StartAutoPosition;
    }
}