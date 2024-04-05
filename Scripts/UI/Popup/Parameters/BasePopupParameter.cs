using static UI_BasePopup;

public class BasePopupParameter
{
    public string Content { get; protected set; }
    public Callback ConfirmCallback { get; protected set; }
    public Callback CancelCallback { get; protected set; }

    public BasePopupParameter(string content = "", Callback confirmCallback = null, Callback cancelCallback = null)
    {
        Content = content;
        ConfirmCallback = confirmCallback;
        CancelCallback = cancelCallback;
    }
}