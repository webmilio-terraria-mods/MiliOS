using System;
using Terraria.Localization;
using Terraria.UI;
using WebmilioCommons.UI;

namespace MiliOS.UI.Menues;

public partial class ContextMenu
{
    public class Button : UIButton
    {
        private readonly Action<Button> _onClick;

        public Button(LocalizedText text, Action<Button> onClick) : this(text, onClick, 1, false) { }

        public Button(LocalizedText text, Action<Button> onClick, float textScale = 1, bool large = false) : base(text, textScale, large)
        {
            _onClick = onClick;
        }

        public override void Click(UIMouseEvent evt)
        {
            _onClick(this);
        }
    }
}