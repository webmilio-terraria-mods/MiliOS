using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;

namespace MiliOS.UI.Menues;

public partial class ContextMenu : UIPanel
{
    public ContextMenu AddButton(LocalizedText text, Action<Button> onClick)
    {
        var dimensions = FontAssets.MouseText.Value.MeasureString(text.Value);

        SetPadding(0);

        Append(new Button(text, onClick)
        {
            Width = StyleDimension.Fill,
            Height = new(dimensions.Y, 0)
        });

        return this;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        var dimensions = GetDimensions();

        if (TopIsBottom)
        {
            Top = new(Top.Pixels - dimensions.Height, 0);
            TopIsBottom = false;

            Recalculate();
            return;
        }

        base.DrawSelf(spriteBatch);
    }

    public bool TopIsBottom { get; set; }
}