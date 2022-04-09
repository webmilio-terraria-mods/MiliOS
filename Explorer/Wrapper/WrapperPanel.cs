using Microsoft.Xna.Framework;
using MiliOS.Applications;
using MiliOS.UI;
using Terraria.UI;

namespace MiliOS.Explorer.Wrapper;

public class WrapperPanel : UIPanel
{
    private readonly WrapperTopBar _topBar;

    public WrapperPanel(Explorer explorer, IApplication application, IApplicationDescriptor descriptor, IWrapperSettings settings, UIElement wraps)
    {
        Explorer = explorer;

        const int padding = 5;

        Wrapped = wraps;
        AutoAdjustToContainer = true;

        SetPadding(padding);

        // TOP BAR
        Append(_topBar = new(application, descriptor, settings)
        {
            Width = StyleDimension.Fill,
            Height = new(WrapperTopBar.CHeight, 0)
        });
        _topBar.Close += TopBar_OnClose;
        _topBar.Minimize += TopBar_OnMinimize;

        var dimensions = wraps.GetDimensions();
        float height = dimensions.Height + padding * 2;

        // WRAPPER POSITION/SIZE
        {
            // TODO Make this thing resize with screen width/height if the original did so.
            Left = new(dimensions.X - padding, 0);
            Top = new(dimensions.Y - padding, 0);

            Width = new(dimensions.Width + padding * 2, 0);
            Height = new(height + WrapperTopBar.CHeight, 0);
        }

        // APP
        {
            AppContainer = new(explorer)
            {
                Width = StyleDimension.Fill,
                Height = new(height - padding * 3, 0),

                Top = new(WrapperTopBar.CHeight + padding, 0),

                BackgroundColor = BorderColor = Color.Transparent
            };
            Append(AppContainer);

            AppContainer.SetPadding(0);

            wraps.VAlign = wraps.HAlign = 0;
            wraps.Left = wraps.Top = StyleDimension.Empty;

            AppContainer.Append(wraps);
        }
    }

    ~WrapperPanel()
    {
        _topBar.Close -= TopBar_OnClose;
        _topBar.Minimize -= TopBar_OnMinimize;
    }

    public override void MouseDown(UIMouseEvent evt)
    {
        Explorer.TryFocus(this);

        base.MouseDown(evt);
    }

    private void TopBar_OnClose(UIElement element) => Close?.Invoke(this);
    private void TopBar_OnMinimize(UIElement element) => Minimize?.Invoke(this);

    public Explorer Explorer { get; }
    public UIElement Wrapped { get; }

    internal AppContainer AppContainer { get; }

    public event UIElementAction Close, Minimize;
}