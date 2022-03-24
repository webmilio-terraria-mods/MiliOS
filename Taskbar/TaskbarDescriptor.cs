using Microsoft.Xna.Framework;
using MiliOS.Applications;
using System;
using MiliOS.UI;
using MiliOS.UI.Menues;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using WebmilioCommons;

namespace MiliOS.Taskbar;

public class TaskbarDescriptor : UIImage
{
    private readonly Explorer.Explorer _explorer;

    private StyleDimension? _originalLeft;

    protected int animationTimer;
    protected AnimationState state;

    public TaskbarDescriptor(IApplicationDescriptor descriptor) : base(descriptor.Icon)
    {
        SetPadding(0);

        ScaleToFit = true;

        Descriptor = descriptor;
        _explorer = ModContent.GetInstance<MiliOSSystem>().Explorer;

        Width = Height = new(Taskbar.IconSize, 0);
    }

    #region Animations
    private void SetAnimation(AnimationState state)
    {
        animationTimer = 0;
        this.state = state;
    }

    public void LaunchingAnimation()
    {
        const float xRatio = 1 / 15f;
        animationTimer++;

        Top = new(LaunchingOffset(animationTimer, xRatio), 0);

        if (animationTimer >= Constants.TicksPerSecond)
        {
            Top = StyleDimension.Empty;
            state = AnimationState.Idle;
        }
    }

    private static float LaunchingOffset(int time, float xRatio)
    {
        return 10 * MathF.Sin(MathHelper.PiOver2 * time * xRatio);
    }

    public void InvalidAnimation()
    {
        if (_originalLeft == null)
            _originalLeft = Left;

        const float xRatio = 1 / 7.5f;
        animationTimer++;

        Left = new(_originalLeft.Value.Pixels + InvalidOffset(animationTimer, xRatio), 0);

        if (animationTimer >= Constants.TicksPerSecond / 3)
        {
            Left = _originalLeft.Value;
            _originalLeft = null;

            state = AnimationState.Idle;
        }
    }

    private static float InvalidOffset(int time, float xRatio)
    {
        return 5 * MathF.Sin(MathHelper.TwoPi * time * xRatio);
    }
    #endregion

    public override void Update(GameTime gameTime)
    {
        switch (state)
        {
            case AnimationState.Launching:
                LaunchingAnimation();
                break;
            case AnimationState.Invalid:
                InvalidAnimation();
                break;
            default:
                base.Update(gameTime);
                break;
        }
    }

    public override void Click(UIMouseEvent evt)
    {
        SetAnimation(_explorer.TryMaximizeMinimizeOrLaunch(Descriptor, Main.LocalPlayer, Array.Empty<object>()) ?
            AnimationState.Launching : AnimationState.Invalid);

        base.Click(evt);
    }

    public override void RightClick(UIMouseEvent evt)
    {
        _explorer.AddContextMenu(new ContextMenu()
        {
            Left = new(Main.MouseScreen.X, 0),
            Top = new(Main.MouseScreen.Y - 25, 0),

            Width = new(250, 0),
            Height = new(300, 0),

            TopIsBottom = true,

            AutoAdjustToContainer = true,
            CloseWhenClickOutside = true
        }
            .AddButton(Language.GetText("Test123"), button => Main.NewText("123"))
        );
    }

    public override void MouseOver(UIMouseEvent evt)
    {
        var tooltip = new Tooltip(Descriptor.Name)
        {
            FollowMouse = true,
            Owner = evt.Target,

            XOffset = t => -t.GetDimensions().Width / 2,
            YOffset = _ => -50
        };

        _explorer.AddElement(tooltip);
        tooltip.MoveToMouse(); // Removes the weird initial top-left frame of the tooltip.
    }

    public override void MouseOut(UIMouseEvent evt)
    {
        _explorer.RemoveElement<Tooltip>(t => t.Owner == evt.Target);
    }

    public IApplicationDescriptor Descriptor { get; }

    public enum AnimationState
    {
        Idle,
        Launching,
        Invalid
    }
}