using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MiliOS.Applications;
using MiliOS.Helpers;
using MiliOS.Taskbar;
using Terraria;
using Terraria.UI;

namespace MiliOS.Taskbar;

public class Taskbar : UIState
{
    public const int
        IconSize = 64,
        CHeight = IconSize + VerticalPadding * 2,
        VerticalPadding = 5,
        HorizontalPadding = 15;

    private const float
        HiddenVAlign = 1.05f,
        ShownVAlign = .99f;

    private float _destination = HiddenVAlign;
    private readonly TaskbarPanel _container;

    public Taskbar()
    {
        Append(_container = new()
        {
            Width = new(0, 1),
            Height = new(0, 1),
            
            VAlign = _destination,

            PaddingLeft = Taskbar.HorizontalPadding,
            PaddingRight = Taskbar.HorizontalPadding,

            PaddingTop = Taskbar.VerticalPadding,
            PaddingBottom = Taskbar.VerticalPadding,

            BackgroundColor = new(.1f, .1f, .1f, .75f)
        });

        VAlign = _destination;
    }

    public override void Update(GameTime gameTime)
    {
        if (ContainsPoint(Main.MouseScreen))
            Main.LocalPlayer.mouseInterface = true;

        if (MathF.Abs(VAlign - _destination) > 0.0001f)
        {
            VAlign += AnimationHelpers.SmoothTranslation(VAlign, _destination);
            Recalculate();
        }

        base.Update(gameTime);
    }

    public void BuildTaskbar(IList<IApplicationDescriptor> descriptors) => _container.BuildTaskbar(descriptors);

    public void Toggle() => _destination = _destination == ShownVAlign ? HiddenVAlign : ShownVAlign;
}