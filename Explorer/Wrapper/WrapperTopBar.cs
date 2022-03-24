using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiliOS.Applications;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using UIPanel = MiliOS.UI.UIPanel;

namespace MiliOS.Explorer.Wrapper;

public class WrapperTopBar : UIPanel
{
    public const int Padding = 2;
    public const int CHeight = 24 + Padding * 2;

    private bool _previousMouse;
    private Vector2? _mouseOffset;

    public WrapperTopBar(IApplication application, IApplicationDescriptor descriptor)
    {
        SetPadding(Padding);

        var textDimensions = FontAssets.MouseText.Value.MeasureString(descriptor.Name.Value);

        Append(new UIText(descriptor.Name)
        {
            Left = new(CHeight + Padding * 2, 0),
            Top = new(Padding, 0),

            Height = new(textDimensions.Y, 0),

            TextOriginX = 0,

            VAlign = .5f,
            Width = StyleDimension.Fill,

            OverflowHidden = true
        });

        Append(new UIImage(descriptor.Icon)
        {
            Width = new(CHeight, 0),
            Height = new(CHeight, 0),

            VAlign = .5f,

            ScaleToFit = true
        });

        Append(new CloseMinimizeGroup(application)
        {
            HAlign = 1,

            Width = new(CHeight * 2, 0),
            Height = new(CHeight, 0)
        });

        BackgroundColor = BorderColor = Color.Transparent;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (Main.mouseLeft)
        {
            if (!_previousMouse && ContainsPoint(Main.MouseScreen) && _mouseOffset == null)
                _mouseOffset = Main.MouseScreen - Parent.GetDimensions().Position();

            if (_mouseOffset != null)
            {
                var newPosition = Main.MouseScreen - _mouseOffset.Value;

                Parent.Left = new(newPosition.X, 0);
                Parent.Top = new(newPosition.Y, 0);
            }
        }
        else
            _mouseOffset = null;

        _previousMouse = Main.mouseLeft;
        base.DrawSelf(spriteBatch);
    }
}