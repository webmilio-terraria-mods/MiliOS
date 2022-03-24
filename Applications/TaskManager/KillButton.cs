using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;
using WebmilioCommons.Helpers;
using WebmilioCommons.UI;

namespace MiliOS.Applications.TaskManager;

public class KillButton : UIButton
{
    public KillButton(Explorer.Explorer explorer, IApplication application) : base(Language.GetText(LocalizedTextHelpers.GetModKey(nameof(MiliOS), "Applications.TaskManager.Kill")))
    {
        Explorer = explorer;
        Application = application;

        Width = new(FontAssets.MouseText.Value.MeasureString(Label.Text).X + 25, 0);
    }

    public override void Click(UIMouseEvent evt)
    {
        Explorer.TerminateApplication(Application);
    }

    public Explorer.Explorer Explorer { get; }
    public IApplication Application { get; }
}