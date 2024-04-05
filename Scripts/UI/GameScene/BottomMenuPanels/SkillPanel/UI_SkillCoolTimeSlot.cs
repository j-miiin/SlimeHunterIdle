using UnityEngine;
using UnityEngine.UI;

public class UI_SkillCoolTimeSlot : UI_Base
{
    [SerializeField] private Image _skillIconImage;
    [SerializeField] private Image _coolTimeImage;
    [SerializeField] private Image _emptyImage;
    private Button _skillButton;

    private UI_SkillPanel _uiSkillPanel;
    private Skill _skill;

    private void Awake()
    {
        _skillButton = GetComponent<Button>();
        _skillButton.onClick.AddListener(() =>
        {
            if (!_uiSkillPanel) _uiSkillPanel = UIManager.Instance.GetUIComponent<UI_SkillPanel>();
            _uiSkillPanel.OpenUI();
        });
    }

    public void RefreshSlot(Skill skill)
    {
        _skill = skill;
        bool isEmpty = (_skill == null);
        _skillIconImage.gameObject.SetActive(!isEmpty);
        _coolTimeImage.gameObject.SetActive(!isEmpty);
        _emptyImage.gameObject.SetActive(isEmpty);
        if (!isEmpty)
            _skill.SetCoolTimeUpdateEventAction(UpdateCoolTime);
    }

    private void UpdateCoolTime()
    { 
        if (_coolTimeImage.gameObject.activeSelf)
            _coolTimeImage.fillAmount = 1 - (_skill.ElapsedCoolTime / _skill.DataSO.CoolTime);
    }
}
