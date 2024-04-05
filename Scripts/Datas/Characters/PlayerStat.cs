using Keiwando.BigInteger;
using System;
using UnityEngine;
using static Enums;

public class PlayerStat : CharacterStat
{
    public event Action<BigInteger> OnMaxHealthUpgrade;

    [field: SerializeField] public float CriticalAtkPower { get; private set; }
    [field: SerializeField] public float CriticalAtkProb { get; private set; }

    private StatManager _statManager;

    public PlayerStat()
    {
        _statManager = StatManager.Instance;
        _statManager.OnStatUpgrade += CallOnStatUpgrade;

        AtkPower = _statManager.GetStat(StatType.AtkPower).CurIntStatValue;
        MaxHealth = _statManager.GetStat(StatType.MaxHealth).CurIntStatValue;
        CriticalAtkPower = _statManager.GetStat(StatType.CriticalAtkPower).CurFloatStatValue;
        CriticalAtkProb = _statManager.GetStat(StatType.CriticalAtkProb).CurFloatStatValue;
    }

    public void ChangeStat(StatType type, BigInteger value)
    {
        switch (type)
        {
            case StatType.AtkPower:
                AtkPower += value; break;
            case StatType.MaxHealth:
                MaxHealth += value;
                OnMaxHealthUpgrade?.Invoke(MaxHealth);
                break;
            default:
                break;
        }
    }

    public void ChangeStat(StatType type, float value)
    {
        switch (type)
        {
            case StatType.CriticalAtkPower:
                CriticalAtkPower += value; break;
            case StatType.CriticalAtkProb:
                CriticalAtkProb += value; break;
            default:
                break;
        }
    }

    private void CallOnStatUpgrade(StatType type)
    {
        switch (type)
        {
            case StatType.AtkPower:
            case StatType.MaxHealth:
                ChangeStat(type, _statManager.GetStat(type).CurUpgradeIntStatCoeff); break;
            case StatType.CriticalAtkPower:
            case StatType.CriticalAtkProb:
                ChangeStat(type, _statManager.GetStat(type).CurUpgradeFloatStatCoeff); break;
            default:
                break;
        }
    }
}
