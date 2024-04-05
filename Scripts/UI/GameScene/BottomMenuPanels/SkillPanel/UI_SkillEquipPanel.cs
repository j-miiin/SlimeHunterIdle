using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillEquipPanel : UI_Base
{
    public event Action<int> OnSlotSelected;

    [SerializeField] private UI_SkillSlot[] _slotList;

    public void RefreshPanel(Skill[] skillList, Action<int> slotSelectAction)
    {
        OnSlotSelected = null;
        OnSlotSelected += slotSelectAction;
        for (int i = 0; i < _slotList.Length; i++)
        {
            _slotList[i].RefreshSlot(skillList[i]);
            Button button = _slotList[i].gameObject.GetComponent<Button>();
            if (button)
            {
                int idx = i;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    Debug.Log(idx);
                    OnSlotSelected?.Invoke(idx);
                });
            }
        }
    }
}
