using UnityEngine;
using static Strings.Prefabs;

public class UI_NotifyRewardPopup : UI_BasePopup
{
    [SerializeField] private Transform _rewardListContainer;

    private ResourceManager _resourceManager;

    public override void ShowPopup(BasePopupParameter popupParameter)
    {
        base.ShowPopup(popupParameter);

        NotifyRewardPopupParameter parameter = popupParameter as NotifyRewardPopupParameter;

        if (!_resourceManager) _resourceManager = ResourceManager.Instance;
        foreach(Reward reward in parameter.RewardList)
        {
            GameObject obj = _resourceManager.Instantiate(UI_REWARD_SLOT_PATH, parent: _rewardListContainer);
            obj.transform.localScale = Vector3.one;
            if (obj && obj.TryGetComponent(out UI_RewardSlot slot))
            {
                slot.Refresh(reward);
            }
        }

        Invoke("CloseUI", 2f);
    }

    public override void CloseUI()
    {
        for (int i = 0; i < _rewardListContainer.childCount; i++)
        {
            _resourceManager.Destroy(_rewardListContainer.GetChild(0).gameObject);
        }
        base.CloseUI();
    }
}
