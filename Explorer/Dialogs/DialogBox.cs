using MiliOS.Explorer.Wrapper;
using MiliOS.UI;
using System;
using Terraria.UI;

namespace MiliOS.Explorer.Dialogs;

public class DialogBox : UIPanel, IExplorerWrapInPanel
{
    private WrapperPanel _wrapper;
    private Action<DialogBox> _callback;

    public DialogBox()
    {
    }

    public DialogBox(Action<DialogBox> action)
    {
        _callback = action;
        AutoAdjustToContainer = true;
    }

    public DialogBox(UIElement element, Action<DialogBox> action) : this(action)
    {
        Append(element);
    }

    ~DialogBox()
    {
        if (_wrapper != null)
            _wrapper.Close -= Wrapper_OnClose;
    }

    public void OnWrapped(WrapperPanel wrapper)
    {
        (_wrapper = wrapper).Close += Wrapper_OnClose;
    }

    private void Wrapper_OnClose(UIElement element) => Exit();

    protected virtual void Exit()
    {
        _callback?.Invoke(this);
        Close?.Invoke(this);
    }

    public IWrapperSettings WrapperSettings { get; init; } = new WrapperSettings()
    {
        ShowClose = false,
        ShowMinimize = false
    };

    public object Owner { get; init; }

    public bool HoldsFocus { get; init; } = true;

    public event Action<DialogBox> Close;
}