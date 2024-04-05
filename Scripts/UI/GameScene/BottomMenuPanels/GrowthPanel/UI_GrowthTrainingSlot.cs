using System;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class UI_GrowthTrainingSlot : UI_Base
{
    public event Action OnUpgradeStat;

    [SerializeField] private StatType _type;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _effectValueText;
    [SerializeField] private Text _gradeValueText;
    [SerializeField] private Text _curGradeText;
    [SerializeField] private Text _nextGradeText;
    [SerializeField] private Text _upgradeCostText;
    [SerializeField] private Slider _gradeValueSlider;
    [SerializeField] private Button _upgradeButton;

    private Stat _stat;
    private StatManager _statManager;
    private CurrencyManager _currencyManager;

    public override void Init()
    {
        _statManager = StatManager.Instance;
        _currencyManager = CurrencyManager.Instance;
        _stat = _statManager.GetStat(_type);
        _upgradeButton.onClick.AddListener(() => {
            if (_stat.CurUpgradeCost > _currencyManager.GetCurrencyAmount(CurrencyType.Gold)) return;
            _statManager.UpgradeStat(_type);
            SetStatSlotUI();
            OnUpgradeStat?.Invoke();
        });
        SetStatSlotUI();
    }

    public void SetStatSlotUI()
    {
        _levelText.text = $"Lv.{_stat.Level + 1}";
        switch (_type)
        {
            case StatType.AtkPower:
            case StatType.MaxHealth:
                _effectValueText.text = $"+{_stat.CurIntStatValue.ChangeMoney()}";
                break;
            case StatType.CriticalAtkPower:
            case StatType.CriticalAtkProb:
                _effectValueText.text = $"+{_stat.CurFloatStatValue.ToString("N2")}%";
                break;
        }
        _gradeValueText.text = $"{_stat.CurExp}/{_stat.MaxExp}";
        _curGradeText.text = $"{_statManager.StatGradeStrings[_stat.Level]}";
        if (_stat.Level >= _statManager.StatGradeStrings.Length) _nextGradeText.gameObject.SetActive(false);
        else _nextGradeText.text = $"{_statManager.StatGradeStrings[_stat.Level + 1]}";
        _upgradeCostText.text = $"{_stat.CurUpgradeCost.ChangeMoney()}";

        _gradeValueSlider.maxValue = _stat.MaxExp;
        _gradeValueSlider.value = _stat.CurExp;
    }

    public void RefreshUpgradeButtonState()
    {
        if (_stat.CurUpgradeCost > _currencyManager.GetCurrencyAmount(CurrencyType.Gold))
        {
            _upgradeCostText.color = Color.red;
            _upgradeButton.interactable = false;
        }
        else
        {
            _upgradeCostText.color = Color.white;
            _upgradeButton.interactable = true;
        }
    }
}
