using System;
using System.Collections.Generic;
using static Enums;

public class SummonSystem 
{
    public event Action<SummonType> OnSummon;
    public event Action<List<Equipment>> OnWeaponSummonResult;
    public event Action<List<Skill>> OnSkillSummonResult;

    private Dictionary<SummonType, Summon> _summonDic;

    private EquipmentManager _equipmentManager;
    private SkillManager _skillManager;
    private CurrencyManager _currencyManager;
    private SummonDataHandler _dataHandler;

    public SummonSystem()
    {
        InitSummonSystem();
    }

    public void InitSummonSystem()
    {
        _equipmentManager = EquipmentManager.Instance;
        _skillManager = SkillManager.Instance;
        _currencyManager = CurrencyManager.Instance;
        _dataHandler = DataManager.Instance.GetDataHandler<SummonDataHandler>();
        _summonDic = _dataHandler.LoadSummonDictionary();
    }

    public Summon GetSummonInfo(SummonType type)
    {
        return _summonDic[type];
    }

    public void SummonWithType(SummonType summonType, SummonCountType countType)
    {
        switch (summonType)
        {
            case SummonType.Equipment:
                SummonWeapon(countType);
                break;
            case SummonType.Skill:
                SummonSkill(countType);
                break;
        }
    }

    public void SummonWeapon(SummonCountType countType)
    {
        SummonType summonType = SummonType.Equipment;
        SummonDataSO dataSO = _summonDic[summonType].DataSO;
        List<SummonProbDataSO> dataList = dataSO.SummonLevelProbList;
        int levelIdx = _summonDic[summonType].Level - 1;
        SummonProbDataSO probSO = dataList[levelIdx];

        int count = (countType == SummonCountType.Small) ? dataSO.Count1 : dataSO.Count2;
        int cost = (countType == SummonCountType.Small) ? dataSO.Cost1 : dataSO.Cost2;

        List<Equipment> resultList = _equipmentManager.SummonEquipments(count, probSO);
        _summonDic[summonType].EarnExp(count);
        _dataHandler.SaveSummonDictionary();
        _currencyManager.SubtractCurrency(CurrencyType.Gem, cost);
        OnSummon?.Invoke(summonType);
        OnWeaponSummonResult?.Invoke(resultList);
    }

    public void SummonSkill(SummonCountType countType)
    {
        SummonType summonType = SummonType.Skill;
        SummonDataSO dataSO = _summonDic[summonType].DataSO;
        List<SummonProbDataSO> dataList = dataSO.SummonLevelProbList;
        int levelIdx = _summonDic[summonType].Level - 1;
        SummonProbDataSO probSO = dataList[levelIdx];

        int count = (countType == SummonCountType.Small) ? dataSO.Count1 : dataSO.Count2;
        int cost = (countType == SummonCountType.Small) ? dataSO.Cost1 : dataSO.Cost2;

        List<Skill> resultList = _skillManager.SummonSkills(count, probSO);
        _summonDic[summonType].EarnExp(count);
        _dataHandler.SaveSummonDictionary();
        _currencyManager.SubtractCurrency(CurrencyType.Gem, cost);
        OnSummon?.Invoke(summonType);
        OnSkillSummonResult?.Invoke(resultList);
    }
}
