using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;
using WebmilioCommons;
using WebmilioCommons.Extensions;

namespace MiliOS.Taskbar;

public abstract class TaskbarDescriptor : UIImage
{
    protected int animationTimer;
    protected AnimationState state;

    protected TaskbarDescriptor(Asset<Texture2D> texture) : base(texture)
    {
        Width = Height = new(TaskBar.IconSize, 0);
    }

    private void SetAnimation(AnimationState state)
    {
        animationTimer = 0;
        this.state = state;
    }

    public void UpDownAnimation()
    {
        const float xRatio = 1 / 15f;

        float YOffset(int time)
        {
            return 10 * MathF.Sin(MathHelper.PiOver2 * time * xRatio);
        }

        animationTimer++;

        Top = new(YOffset(animationTimer), 0);

        if (animationTimer >= Constants.TicksPerSecond * 3)
        {
            Top = StyleDimension.Empty;
            state = AnimationState.Idle;
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (state == AnimationState.UpDown)
            UpDownAnimation();

        base.Update(gameTime);
    }

    public override void Click(UIMouseEvent evt)
    {
        base.Click(evt);

        SetAnimation(AnimationState.UpDown);
    }

    public virtual LocalizedText GetTooltip()
    {
        // TODO Change this to support any mod.
        return MiliOS.Instance.GetText("Softwares.Donut.Tooltip");
    }

    public enum AnimationState
    {
        Idle,
        UpDown
    }
}