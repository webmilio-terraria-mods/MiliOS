using Microsoft.Xna.Framework;
using System;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using Terraria.UI;
using WebmilioCommons.Extensions;
using WebmilioCommons.UI;

namespace MiliOS.Explorer.Dialogs;

public class MessageBox : DialogBox
{
    private Action<MessageBox> _callback;
    private readonly HorizontalGroup _group;

    public MessageBox(LocalizedText text, Buttons buttons, Action<MessageBox> callback)
    {
        _callback = callback;

        _group = new HorizontalGroup()
        {
            VAlign = 1,
            HAlign = .5f,

            Width = new(0, .9f),
            Height = new(40, 0),

            BackgroundColor = Color.Transparent,
            BorderColor = Color.Transparent
        }
            .SetPaddingFluid(0);

        AddButton(_group, buttons, Buttons.OK);
        AddButton(_group, buttons, Buttons.Cancel);
        AddButton(_group, buttons, Buttons.Yes);
        AddButton(_group, buttons, Buttons.No);
        Append(_group);

        Append(new UIText(text)
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill,

            IsWrapped = true
        });
    }

    ~MessageBox()
    {
        _group.Children.Do(element => element.OnClick -= Button_OnClick);
    }

    private void AddButton(HorizontalGroup group, Buttons buttons, Buttons flag)
    {
        if (!buttons.HasFlag(flag))
            return;

        const string template = "UI.{0}";
        var button = new UIButton(MiliOS.Instance.GetText(string.Format(template, flag.ToString())))
        {
            Height = StyleDimension.Fill,

            Tags = new()
            {
                { nameof(State), flag }
            }
        };

        group.AddElement(button);
        button.OnClick += Button_OnClick;
    }

    private void Button_OnClick(UIMouseEvent evt, UIElement element)
    {
        var button = (UIButton) element;
        State = (Buttons) button.Tags[nameof(State)];

        Exit();
        _callback(this);
    }

    public Buttons State { get; private set; }

    [Flags]
    public enum Buttons : byte
    {
        OK = 1,
        Cancel = OK << 1,
        Yes = Cancel << 1,
        No = Yes << 1
    }
}