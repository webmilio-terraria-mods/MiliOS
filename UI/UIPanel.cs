using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace MiliOS.UI;

public class UIPanel : Terraria.GameContent.UI.Elements.UIPanel
{
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (AutoAdjustToContainer)
            InterfacesHelper.FitInState(this);

        if (CaptureMouse && ContainsPoint(Main.MouseScreen))
        {
            Main.LocalPlayer.mouseInterface = true;
        }
        else if (Parent != null && CloseWhenClickOutside && Main.mouseLeft)
        {
            Parent.OnUpdate += Parent_OnUpdate;
        }

        base.DrawSelf(spriteBatch);
    }

    private void Parent_OnUpdate(Terraria.UI.UIElement element)
    {
        OutsideClose();

        Parent.OnUpdate -= Parent_OnUpdate;

        Remove();
        Recalculate();
    }

    public virtual void OutsideClose() { }

    public bool CaptureMouse { get; init; } = true;
    public bool AutoAdjustToContainer { get; init; }
    public bool CloseWhenClickOutside { get; init; }
}