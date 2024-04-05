using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;
using static Strings.Stat;
using static Strings.Equipment;
using System;

public class UI_EquipmentPanel : UI_Base
{
    public event Action OnPanelClosed;

    #region SerializeFields
    [SerializeField] private UI_EquipmentSlot _equipmentInfo;
    [Header("강화 공격 정보")]
    [SerializeField] private GameObject _enhancedAttackInfoSlot;
    [SerializeField] private Image _enhancedAttackIconImage;
    [SerializeField] private Text _requiredAttackCountText;
    [SerializeField] private Text _enhancedAttackDescriptionText;
    [Header("장착 효과 정보")]
    [SerializeField] private Text _equippedEffectText;
    [Header("버튼")]
    [SerializeField] private Text _equipButtonText;
    [SerializeField] private Button _compositeButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _allCompositeButton;
    [SerializeField] private Button _recommendedEquipButton;
    [SerializeField] private Button _enhanceEquipButton;
    [Header("Tab 및 Slot")]
    [SerializeField] private UI_EquipmentTab[] _tabList;
    [SerializeField] private UI_EquipmentSlot[] _slotList;
    [SerializeField] private Button _closeButton;
    [Header("ScrollView")]
    [SerializeField] private GridLayoutGroup _itemGridView;
    [SerializeField] private ScrollRect _itemScrollRect;
    #endregion

    private EquipmentManager _equipmentManager;
    private UIManager _uiManager;
    private List<Weapon> _weaponList;
    private List<Suit> _suitList;

    private EquipmentType _curSelectedTab;
    private Equipment _curSelectedEquipment;
    private UI_EnhanceEquipmentPanel _uiEnhanceEquipmentPanel;
    private int _curSelectedIdx = 0;
    private float _gridCellSize;

    public override void Init()
    {
        base.Init();
        _equipmentManager = EquipmentManager.Instance;
        _uiManager = UIManager.Instance;    
        InitTab();
        InitSlotList();
        InitButtons();
        _closeButton.onClick.AddListener(CloseUI);
        _gridCellSize = _itemGridView.cellSize.y;
    }

    public override void OpenUI()
    {
        base.OpenUI();
        _curSelectedTab = EquipmentType.Fist;
        _tabList[(int)EquipmentType.Fist].CallOnTabClicked();
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
            _slotList[i].OnClicked += SetEquipmentInfo;
    }

    private void InitButtons()
    {
        _equipButton.onClick.AddListener(OnClickEquipButton);
        _enhanceEquipButton.onClick.AddListener(OnClickEnhanceEquipButton);
        _compositeButton.onClick.AddListener(OnClickCompositeButton);
        _allCompositeButton.onClick.AddListener(OnClickAllCompositeButton);
        _recommendedEquipButton.onClick.AddListener(OnClickRecommendedEquipButton);
    }

    // 전체 UI Refresh
    private void RefreshAll(EquipmentType type)
    {
        switch (type)
        {
            case EquipmentType.Fist:
            case EquipmentType.Bow:
                RefreshAllWeapon(type);
                break;
            case EquipmentType.Clothes:
            case EquipmentType.Shoes:
                RefreshAllSuit(type);
                break;
        }

        _curSelectedTab = type;
        RefreshSelectedEquipment(type);
    }

    // TODO 현재 장착 중인 장비 idx 반영하기
    private void RefreshAllWeapon(EquipmentType type)
    {
        _weaponList = _equipmentManager.GetWeaponList(type);
        for (int i = 0; i < _slotList.Length; i++)
            _slotList[i].RefreshSlot(_weaponList[i]);
    }

    private void RefreshAllSuit(EquipmentType type)
    {
        _suitList = _equipmentManager.GetSuitList(type);
        for (int i = 0; i < _slotList.Length; i++)
            _slotList[i].RefreshSlot(_suitList[i]);
    }

    // 장비 정보 및 버튼 세팅
    private void SetEquipmentInfo(Equipment equipment)
    {
        _curSelectedEquipment = equipment;

        bool isInteractable = !equipment.IsLocked;
        _enhanceEquipButton.gameObject.SetActive(isInteractable);
        _compositeButton.gameObject.SetActive(isInteractable);
        _equipButton.gameObject.SetActive(isInteractable);
        _allCompositeButton.gameObject.SetActive(isInteractable);
        _recommendedEquipButton.gameObject.SetActive(isInteractable);

        _equipmentInfo.RefreshSlot(equipment);
        if (equipment.DataSO.Type == EquipmentType.Fist || equipment.DataSO.Type == EquipmentType.Bow)
        {
            _enhancedAttackInfoSlot.SetActive(true);
            SetEnhancedAttackInfo(equipment);
        } else
        {
            _enhancedAttackInfoSlot.SetActive(false);
        }
        SetEquippedEffectText(equipment);

        // 보유한 장비일 경우
        if (isInteractable)
        {
            _enhanceEquipButton.interactable = equipment.IsPossibleToEnhance();
            _compositeButton.interactable = equipment.IsPossibleToComposite();
            _equipButtonText.text = (equipment.IsEquipped) ? UNEQUIP_STR : EQUIP_STR;
            _allCompositeButton.interactable = _equipmentManager.IsAllCompositable();
            _recommendedEquipButton.interactable = _equipmentManager.IsEquipRecommended();
        }
    }

    private void SetEnhancedAttackInfo(Equipment equipment)
    {
        Weapon weapon = equipment as Weapon;
        EnhancedAttackDataSO dataSO = (equipment as Weapon).WeaponDataSO.EnhancedAttackDataSO;
        _enhancedAttackIconImage.sprite = dataSO.Icon;
        _requiredAttackCountText.text = $"{ATTACK_COUNT_STR}{dataSO.RequiredAttackCount}";
        _enhancedAttackDescriptionText.text = dataSO.Description;
    }

    // 장착 효과
    private void SetEquippedEffectText(Equipment equipment)
    {
        switch (equipment.DataSO.Type)
        {
            case EquipmentType.Fist:
            case EquipmentType.Bow:
                _equippedEffectText.text = $"{ATK_POWER_STR} +{equipment.EquippedEffect.ChangeMoney()}";
                break;
            case EquipmentType.Clothes:
            case EquipmentType.Shoes:
                _equippedEffectText.text = $"{HEALTH_STR} +{equipment.EquippedEffect.ChangeMoney()}";
                break;
        }
    }

    private void RefreshSelectedEquipment(EquipmentType type)
    {
        // 현재 장착 중인 장비가 선택되도록 세팅
        // 선택된 장비가 없다면 D1 장비 선택
        _curSelectedEquipment = _equipmentManager.GetCurEquippedEquipment(type);
        if (_curSelectedEquipment == null)
        {
            if (type == EquipmentType.Fist || type == EquipmentType.Bow)
                _curSelectedEquipment = _weaponList[0];
            else
                _curSelectedEquipment = _suitList[0];
        }
        _curSelectedIdx = _curSelectedEquipment.DataSO.ID % 100;

        float value = ((_curSelectedIdx / 4) * _gridCellSize) / (_itemScrollRect.content.rect.height - 1000);
        _itemScrollRect.verticalNormalizedPosition = value;

        _slotList[_curSelectedIdx].CallOnClicked();
    }

    // 장착
    private void OnClickEquipButton()
    {
        if (_curSelectedEquipment.IsEquipped) _equipmentManager.UnEquip(_curSelectedEquipment.DataSO.EquipName);
        else _equipmentManager.Equip(_curSelectedEquipment.DataSO.ID % 200, _curSelectedEquipment.DataSO.Type);
        SetEquipmentInfo(_curSelectedEquipment);
    }

    // 추천 장착
    private void OnClickRecommendedEquipButton()
    {
        _equipmentManager.EquipRecommendedEquipment();
        RefreshAll(_curSelectedTab);
    }

    private void OnClickEnhanceEquipButton()
    {
        if (!_uiEnhanceEquipmentPanel)
        {
            if (!_uiManager.TryGetUIComponent<UI_EnhanceEquipmentPanel>(out _uiEnhanceEquipmentPanel))
                Debug.LogError("Null Exception : UI_EnhanceEquipmentPanel");
        }
        _uiEnhanceEquipmentPanel.OpenUI();
        _uiEnhanceEquipmentPanel.Refresh(_curSelectedEquipment);
        _uiEnhanceEquipmentPanel.OnPanelClosed += RefreshAll;
    }

    // 합성
    private void OnClickCompositeButton()
    {
        _equipmentManager.Composite(_curSelectedEquipment);
        SetEquipmentInfo(_curSelectedEquipment);
    }

    // 일괄 합성
    private void OnClickAllCompositeButton()
    {
        _equipmentManager.AllComposite();
        RefreshAll(_curSelectedTab);
    }
}
