using System.Collections.Generic;
using UnityEngine;
using static Enums;
using static Strings.Equipment;
using UnityEngine.UI;
using System;

public class UI_SkillPanel : UI_Base
{
    public event Action OnPanelClosed;

    [SerializeField] private UI_SkillSlot _skillBaseInfo;
    [SerializeField] private UI_SkillDetailInfo _skillDetailInfo;
    [SerializeField] private Text _equipButtonText;
    [SerializeField] private Button _optionButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _allLevelUpButton;
    [SerializeField] private UI_SkillTab[] _tabList;
    [SerializeField] private UI_SkillSlot[] _slotList;
    [SerializeField] private UI_SkillSlot[] _equippedSlotList;
    [SerializeField] private Button _closeButton;

    private SkillManager _skillManager;
    private UIManager _uiManager;
    private UI_SkillEquipPanel _uiSkillEquipPanel;
    private List<Skill> _skillList;
    private Skill[] _equippedSkillList;

    private SkillEquipType _curSelectedTab;
    private Skill _curSelectedSkill;
    private int _curSelectedIdx = 0;

    public override void Init()
    {
        base.Init();
        _skillManager = SkillManager.Instance;
        _uiManager = UIManager.Instance;
        InitTab();
        InitSlotList();
        InitButtons();
        _closeButton.onClick.AddListener(CloseUI);
    }

    public override void OpenUI()
    {
        base.OpenUI();
        _curSelectedTab = SkillEquipType.Fist;
        _tabList[(int)SkillEquipType.Fist].CallOnTabClicked();
    }

    public override void CloseUI()
    {
        base.CloseUI();
        OnPanelClosed?.Invoke();
    }

    private void InitTab()
    {
        for (int i = 0; i < _tabList.Length; i++)
        {
            _tabList[i].OnClicked += RefreshAll;
            _tabList[i].Init();
        }
    }

    private void InitSlotList()
    {
        for (int i = 0; i < _slotList.Length; i++)
            _slotList[i].OnClicked += SetSkillInfo;
    }

    private void InitButtons()
    {
        //_optionButton.onClick.AddListener();
        _equipButton.onClick.AddListener(OnClickEquipButton);
        _allLevelUpButton.onClick.AddListener(OnClickAllLevelUpButton);
    }

    // 전체 UI Refresh
    private void RefreshAll(SkillEquipType type)
    {
        _skillList = _skillManager.GetSkillList(type);
        for (int i = 0; i < _slotList.Length; i++)
            _slotList[i].RefreshSlot(_skillList[i]);

        _curSelectedTab = type;
        RefreshEquippedSkill(type);
        RefreshSelectedSkill(type);
    }

    private void RefreshEquippedSkill(SkillEquipType type)
    {
        _equippedSkillList = _skillManager.GetEquippedSkillList(type);
        for (int i = 0; i < _equippedSlotList.Length; i++)
            _equippedSlotList[i].RefreshSlot(_equippedSkillList[i]);
    }

    // 장비 정보 및 버튼 세팅
    private void SetSkillInfo(Skill skill)
    {
        _curSelectedSkill = skill;

        bool isInteractable = !skill.IsLocked;
        _optionButton.gameObject.SetActive(isInteractable);
        _equipButton.gameObject.SetActive(isInteractable);
        _allLevelUpButton.gameObject.SetActive(isInteractable);

        _skillBaseInfo.RefreshSlot(skill);
        _skillDetailInfo.RefreshSkillInfo(skill);

        // 보유한 장비일 경우
        if (isInteractable)
        {
            //_optionButton.interactable = ;
            _equipButtonText.text = (skill.EquippedIdx != -1) ? UNEQUIP_STR : EQUIP_STR;
            _allLevelUpButton.interactable = _skillManager.IsPossibleAllLevelUp();
        }
    }

    private void RefreshSelectedSkill(SkillEquipType type)
    {
        _curSelectedSkill = _skillList[0];
        _curSelectedIdx = _curSelectedSkill.DataSO.ID % 100;
        _slotList[_curSelectedIdx].CallOnClicked();
    }

    // 장착
    private void OnClickEquipButton()
    {
        if (_curSelectedSkill.EquippedIdx != -1) _skillManager.UnEquip(_curSelectedSkill);
        else
        {
            bool isEquipped = _skillManager.Equip(_curSelectedSkill);
            if (!isEquipped)
            {
                OpenSkillEquipPanel();
                return;
            }
        }
        SetSkillInfo(_curSelectedSkill);
        RefreshEquippedSkill(_curSelectedTab);
    }

    private void OpenSkillEquipPanel()
    {
        if (!_uiSkillEquipPanel)
        {
            if (!_uiManager.TryGetUIComponent<UI_SkillEquipPanel>(out _uiSkillEquipPanel))
            {
                Debug.LogError("Null Exception : UI_CookBook");
            }
        }
        _uiSkillEquipPanel.OpenUI();
        _uiSkillEquipPanel.RefreshPanel(_equippedSkillList, (int idx) =>
        {
            _skillManager.Equip(_curSelectedSkill, idx);
            _uiSkillEquipPanel.CloseUI();
            RefreshEquippedSkill(_curSelectedTab);
            RefreshSelectedSkill(_curSelectedTab);
        });
    }

    // 일괄 레벨업
    private void OnClickAllLevelUpButton()
    {
        _skillManager.AllLevelUp();
        RefreshAll(_curSelectedTab);
    }
}
