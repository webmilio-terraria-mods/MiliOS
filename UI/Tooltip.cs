using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;

namespace MiliOS.UI;

public class Tooltip : Terraria.GameContent.UI.Elements.UIPanel
{
    public Tooltip(LocalizedText text)
    {
        var textValue = text.Value;
        var textDimensions = FontAssets.MouseText.Value.MeasureString(textValue);

        UIText tooltip = new(textValue)
        {
            HAlign = .5f,

            Width = new(textDimensions.X + 10, 0),
            Height = new(textDimensions.Y + 10, 0)
        };

        Append(tooltip);

        var dimensions = tooltip.GetDimensions();

        Width = new(dimensions.Width + 10 * 2, 0);
        Height = new(dimensions.Height + 10 * 2, 0);

        BackgroundColor = Color.Black;
        BorderColor = Color.Transparent;

        IgnoresMouseInteraction = true;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (FollowMouse)
            MoveToMouse();

        base.DrawSelf(spriteBatch);
    }

    public void MoveToMouse()
    {
        Left = new(Main.MouseScreen.X + XOffset(this), 0);
        Top = new(Main.MouseScreen.Y + YOffset(this), 0);
    }

    public object Owner { get; set; }

    public bool FollowMouse { get; set; }

    public Func<Tooltip, float> XOffset { get; init; }
    public Func<Tooltip, float> YOffset { get; init; }
}