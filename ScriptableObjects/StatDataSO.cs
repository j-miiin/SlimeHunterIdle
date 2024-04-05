using System;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "StatDataSO", menuName ="Data/StatDataSO", order = 0)]
public class StatDataSO : ScriptableObject
{
    [SerializeField] private StatType _type;
    [SerializeField] private StatValueType _valueType;
    [SerializeField] private int _defaultMaxExp;
    [SerializeField] private int _maxExpInterval;
    [SerializeField] private int _defaultUpgradeCost;
    [SerializeField] private int _defaultUpgradeCostCoeff;
    [SerializeField] private int _upgradeCostCoeffInterval;

    [Header("Int Stat")]
    [SerializeField] private int _defaultIntStat;
    [SerializeField] private int _defaultUpgradeIntStatCoeff;
    [SerializeField] private int _upgradeIntStatCoeffInterval;

    [Header("Float Stat")]
    [SerializeField] private float _defaultFloatStat;
    [SerializeField] private float _defaultUpgradeFloatStatCoeff;

    public StatType Type => _type;
    public StatValueType ValueType => _valueType;
    public int DefaultMaxExp => _defaultMaxExp;
    public int MaxExpInterval => _maxExpInterval;
    public int DefaultUpgradeCost => _defaultUpgradeCost;
    public int DefaultUpgradeCostCoeff => _defaultUpgradeCostCoeff;
    public int UpgradeCostCoeffInterval => _upgradeCostCoeffInterval;
    public int DefaultIntStat => _defaultIntStat;
    public int DefaultUpgradeIntStatCoeff => _defaultUpgradeIntStatCoeff;
    public int UpgradeIntStatCoeffInterval => _upgradeIntStatCoeffInterval;
    public float DefaultFloatStat => _defaultFloatStat;
    public float DefaultUpgradeFloatStatCoeff => _defaultUpgradeFloatStatCoeff;
}
