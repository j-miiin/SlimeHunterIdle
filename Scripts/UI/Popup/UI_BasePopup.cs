using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BasePopup : UI_Base
{
    public delegate void Callback();
    private Callback _callbackConfirm;
    private Callback _callbackCancel;

    [SerializeField] private Text _contentText;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;

    protected virtual void Awake()
    {
//#if UNITY_EDITOR
//        Debug.Assert(_contentText, "Null Exception : _contentText");
//        Debug.Assert(_confirmButton, "Null Exception : _confirmButton");
//        Debug.Assert(_cancelButton, "Null Exception : _cancelButton");
//#endif

        if (_confirmButton != null) _confirmButton.onClick.AddListener(() => ClosePopup(Enums.PopupButtonType.Confirm));
        if (_cancelButton != null) _cancelButton.onClick.AddListener(() => ClosePopup(Enums.PopupButtonType.Cancel));
    }

    public virtual void ShowPopup(BasePopupParameter popupParameter)
    {
        _contentText.text = popupParameter.Content;
        _callbackConfirm = popupParameter.ConfirmCallback;
        _callbackCancel = popupParameter.CancelCallback;
        OpenUI();
    }

    public void ClosePopup(Enums.PopupButtonType type)
    {
        if (type == Enums.PopupButtonType.Confirm) _callbackConfirm?.Invoke();
        else _callbackCancel?.Invoke();

        CloseUI();
    }
}