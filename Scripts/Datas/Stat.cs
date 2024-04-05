using Keiwando.BigInteger;
using System;
using static Enums;

[Serializable]
public class Stat
{
    public StatDataSO DataSO { get; private set; }
    public StatType Type { get; protected set; }
    public int Level { get; protected set; }
    public int CurExp { get; protected set; }
    public int MaxExp { get; protected set; }
    public BigInteger CurUpgradeCost { get; protected set; }
    public BigInteger CurUpgradeCostCoeff { get; protected set; }
    public BigInteger CurIntStatValue { get; protected set; }
    public BigInteger CurUpgradeIntStatCoeff { get; protected set; }
    public float CurFloatStatValue { get; protected set; }
    public float CurUpgradeFloatStatCoeff { get; protected set; }

    private string _curUpgradeCostStr;
    private string _curUpgradeCostCoeffStr;
    private string _curIntStatValueStr;
    private string _curUpgradeIntStatCoeffStr;

    public Stat() { }

    public Stat(StatDataSO data)
    {
        DataSO = data;
        Type = DataSO.Type;
        Level = 0;
        CurExp = 0;
        MaxExp = DataSO.DefaultMaxExp;
        CurUpgradeCost = DataSO.DefaultUpgradeCost;
        CurUpgradeCostCoeff = DataSO.DefaultUpgradeCostCoeff;

        if (DataSO.ValueType == StatValueType.Integer)
        {
            CurIntStatValue = DataSO.DefaultIntStat;
            CurUpgradeIntStatCoeff = DataSO.DefaultUpgradeIntStatCoeff;
            _curIntStatValueStr = CurIntStatValue.ToString();
            _curUpgradeIntStatCoeffStr = CurUpgradeIntStatCoeff.ToString();
        }
        else
        {
            CurFloatStatValue = DataSO.DefaultFloatStat;
            CurUpgradeFloatStatCoeff = DataSO.DefaultUpgradeFloatStatCoeff;
        }

        _curUpgradeCostStr = CurUpgradeCost.ToString();
        _curUpgradeCostCoeffStr = CurUpgradeCostCoeff.ToString();
    }

    public void SetStatDataSO(StatDataSO data)
    {
        DataSO = data;
        CurUpgradeCost = new BigInteger(_curUpgradeCostStr);
        CurUpgradeCostCoeff = new BigInteger(_curUpgradeCostCoeffStr);
        if (DataSO.ValueType == StatValueType.Integer)
        {
            CurIntStatValue = new BigInteger(_curIntStatValueStr);
            CurUpgradeIntStatCoeff = new BigInteger(_curUpgradeIntStatCoeffStr);
        }
    }

    public void UpgradeStat()
    {
        CurExp++;
        CurUpgradeCost += CurUpgradeCostCoeff;
        if (DataSO.ValueType == StatValueType.Integer)
        {
            CurIntStatValue += CurUpgradeIntStatCoeff;
            _curIntStatValueStr = CurIntStatValue.ToString();
        }
        else
            CurFloatStatValue += CurUpgradeFloatStatCoeff;
        if (CurExp >= MaxExp) LevelUpStat();

        _curUpgradeCostStr = CurUpgradeCost.ToString();
    }

    public void LevelUpStat()
    {
        Level++;
        CurExp = 0;

        MaxExp += DataSO.MaxExpInterval;
        CurUpgradeCostCoeff *= DataSO.UpgradeCostCoeffInterval;
        if (DataSO.ValueType == StatValueType.Integer)
        {
            CurUpgradeIntStatCoeff *= DataSO.UpgradeIntStatCoeffInterval;
            _curUpgradeIntStatCoeffStr = CurUpgradeIntStatCoeff.ToString();
        }
        _curUpgradeCostCoeffStr = CurUpgradeCostCoeff.ToString();
    }
}
