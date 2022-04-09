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

    public CloseMinimizeGroup(IApplication application, bool minimize = true, bool close = true)
    {
        Application = application;

        SetPadding(0);
        BorderColor = BackgroundColor = Color.Transparent;

        if (minimize)
        {
            Append(_minimize = MakeButton(minimizeImage, 0));
            _minimize.OnClick += Close_OnClick;
        }

        if (close)
        {
            Append(_close = MakeButton(closeImage, 1));
            _close.OnClick += Minimize_OnClick;
        }
    }

    ~CloseMinimizeGroup()
    {
        if (_minimize != null)
            _minimize.OnClick -= Minimize_OnClick;

        if (_close != null)
            _close.OnClick -= Close_OnClick;
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
        Close?.Invoke(this);
        Application.Exit();
    }

    private void Minimize_OnClick(UIMouseEvent evt, UIElement element)
    {
        Minimize?.Invoke(this);
        Application.Minimize();
    }

    public IApplication Application { get; }

    public event UIElementAction Close, Minimize;
}