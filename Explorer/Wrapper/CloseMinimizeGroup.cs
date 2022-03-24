using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiliOS.Applications;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace MiliOS.Explorer.Wrapper;

public class CloseMinimizeGroup : UIPanel
{
    public static readonly Asset<Texture2D> 
        closeImage = MiliOS.Instance.Assets.Request<Texture2D>("Explorer/Wrapper/close"), 
        minimizeImage = MiliOS.Instance.Assets.Request<Texture2D>("Explorer/Wrapper/minimize");

    private UIImageButton _close, _minimize;

    public CloseMinimizeGroup(IApplication application)
    {
        Application = application;

        SetPadding(0);
        BorderColor = BackgroundColor = Color.Transparent;

        Append(_minimize = MakeButton(minimizeImage, 0));
        Append(_close = MakeButton(closeImage, 1));

        _close.OnClick += Close_OnClick;
        _minimize.OnClick += Minimize_OnClick;
    }

    ~CloseMinimizeGroup()
    {
        _close.OnClick -= Close_OnClick;
        _minimize.OnClick -= Minimize_OnClick;
    }

    private UIImageButton MakeButton(Asset<Texture2D> texture, float hAlign)
    {
        return new(texture)
        {
            HAlign = hAlign,

            Width = new(WrapperTopBar.CHeight, 0),
            Height = new(WrapperTopBar.CHeight, 0),
        };
    }

    private void Close_OnClick(UIMouseEvent evt, UIElement element)
    {
        Application.Exit();
    }

    private void Minimize_OnClick(UIMouseEvent evt, UIElement element)
    {
        Application.Minimize();
    }

    public IApplication Application { get; }
}