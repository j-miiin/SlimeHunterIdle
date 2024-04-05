using System.Collections.Generic;
using static UI_BasePopup;

public class NotifyRewardPopupParameter : BasePopupParameter
{
    public List<Reward> RewardList { get; private set; }

    public NotifyRewardPopupParameter(
        List<Reward> rewardList, 
        string content = "", 
        Callback confirmCallback = null, 
        Callback cancelCallback = null
        ) : base(content: content, confirmCallback: confirmCallback, cancelCallback: cancelCallback)
    {
        RewardList = rewardList;
    }
}
