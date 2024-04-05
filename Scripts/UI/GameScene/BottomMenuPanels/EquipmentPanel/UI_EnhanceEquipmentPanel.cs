using Keiwando.BigInteger;
using UnityEngine;
using UnityEngine.UI;
using static Enums;
using static Strings.Stat;
using static Colors;
using System;

public class UI_EnhanceEquipmentPanel : UI_Base
{
    public event Action<EquipmentType> OnPanelClosed;

    [SerializeField] private Text _currencyText;
    [SerializeField] private UI_EquipmentSlot _equipmentSlot;
    [SerializeField] private Text _currentEffectText;
    [SerializeField] private Text _nextEffectText;
    [SerializeField] private Text _costText;
    [SerializeField] private Button _enhanceButton;
    [SerializeField] private Button _closeButton;

    private EquipmentManager _equipmentManager;
    private CurrencyManager _currencyManager;
    private Equipment _equipment;

    public override void Init()
    {
        base.Init();
        _equipmentManager = EquipmentManager.Instance;
        _currencyManager = CurrencyManager.Instance;
        _enhanceButton.onClick.AddListener(() =>
        {
            if (_equipment == null) return;
            if (_currencyManager
            && (_currencyManager.GetCurrencyAmount(CurrencyType.EnhancementStone) < _equipment.RequiredEnhanceStone)) return;
             _equipmentManager.Enhance(_equipment);
            if (!_equipment.IsPossibleToEnhance()) CloseUI();
            Refresh(_equipment);
        });
        _closeButton.onClick.AddListener(CloseUI);
    }

    public override void CloseUI()
    {
        base.CloseUI();
        OnPanelClosed?.Invoke(_equipment.DataSO.Type);
    }

    public void Refresh(Equipment equipment)
    {
        _equipment = equipment;
        BigInteger currency = _currencyManager.GetCurrencyAmount(CurrencyType.EnhancementStone);
        BigInteger cost = _equipment.RequiredEnhanceStone;
        _currencyText.text = currency.ChangeMoney();
        _equipmentSlot.RefreshSlot(_equipment);
        string statStr = ATK_POWER_STR;
        if (_equipment.DataSO.Type == EquipmentType.Clothes || _equipment.DataSO.Type == EquipmentType.Shoes)
            statStr = HEALTH_STR;

        _currentEffectText.text = $"{statStr} +{_equipment.EquippedEffect.ChangeMoney()}";
        _nextEffectText.text = $"{statStr} +{_equipment.GetNextEquippedEffect().ChangeMoney()}";
        _costText.text = cost.ChangeMoney();
        bool isPossibleToEnhance = (currency >= cost);
        _costText.color = (isPossibleToEnhance) ? TextBrown : TextRed;
        _enhanceButton.interactable = isPossibleToEnhance;
    }
}
