public class UI_NotifyPopup : UI_BasePopup
{
    public override void ShowPopup(BasePopupParameter popupParameter)
    {
        base.ShowPopup(popupParameter);
        Invoke("CloseUI", 1f);
    }
}
