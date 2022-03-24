using Terraria;
using Terraria.UI;

namespace MiliOS.UI;

public static class InterfacesHelper
{
    public static void FitInState(UIElement element)
    {
        var dimensions = element.GetDimensions();
        var parentDimensions = element.Parent.GetDimensions();

        if (dimensions.Y < 0)
            element.Top = new(element.Top.Pixels - dimensions.Y, 0);
        else
        {
            var bottomY = dimensions.Y + dimensions.Height;

            if (bottomY > parentDimensions.Height)
                element.Top = new(element.Top.Pixels - (bottomY - parentDimensions.Height), 0);
        }

        if (dimensions.X < 0)
            element.Left = new(element.Left.Pixels - dimensions.X, 0);
        else
        {
            var bottomX = dimensions.X + dimensions.Width;

            if (bottomX > parentDimensions.Width)
                element.Left = new(element.Left.Pixels - (bottomX - parentDimensions.Width), 0);
        }
    }
}