using UnityEngine;

public class UI_GrowthTraining : UI_Base
{
    [SerializeField] private UI_GrowthTrainingSlot[] _slotList;

    public override void Init()
    {
        for (int i = 0; i < _slotList.Length; i++)
        {
            _slotList[i].OnUpgradeStat += RefreshAllSlots;
            _slotList[i].Init();
        }
    }

    public override void OpenUI()
    {
        base.OpenUI();
        RefreshAllSlots();
    }

    private void RefreshAllSlots()
    {
        for (int i = 0; i < _slotList.Length; i++)
        {
            _slotList[i].RefreshUpgradeButtonState();
        }
    }
}
