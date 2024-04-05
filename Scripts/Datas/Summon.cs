using System;
using UnityEngine;
using static Enums;

[Serializable]
public class Summon
{
    public SummonDataSO DataSO { get; private set; }
    public SummonType SummonType { get; private set; }
    public int Level { get; private set; }
    public int MaxLevel { get; private set; }
    public int MaxExp { get; private set;}
    public int CurExp { get; private set; }

    public Summon() { }

    public Summon(SummonDataSO dataSO)
    {
        DataSO = dataSO;
        SummonType = DataSO.Type;
        Level = 1;
        MaxLevel = DataSO.SummonLevelProbList.Count;
        MaxExp = DataSO.DefaultMaxExp;
        CurExp = 0;
    }

    public void SetDataSO(SummonDataSO dataSO)
    {
        DataSO = dataSO;
    }

    public void EarnExp(int exp)
    {
        CurExp += exp;
        if (CurExp >= MaxExp) LevelUp();
    }

    public void LevelUp()
    {
        if (Level == MaxLevel) return;
        Level++;
        CurExp -= MaxExp;
        MaxExp += DataSO.MaxExpCoeff;
    }
}