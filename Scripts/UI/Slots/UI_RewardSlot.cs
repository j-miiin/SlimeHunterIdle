using UnityEngine;
using UnityEngine.UI;

public class UI_RewardSlot : UI_Base
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Text _quantityText;
    [SerializeField] private Text _probText;

    public void Refresh(Reward reward)
    {
        _iconImage.sprite = reward.Icon;
        _quantityText.text = $"x{reward.Value.ChangeMoney()}";
        _probText.gameObject.SetActive((reward.Prob < 100));
        if (reward.Prob < 100) _probText.text = $"{reward.Prob}%";
    }
}
