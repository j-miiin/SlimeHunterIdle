using System;
using System.Collections.Generic;
using static Enums;

public class StatManager : Singleton<StatManager>
{
    public event Action<StatType> OnStatUpgrade;

    public readonly string[] StatGradeStrings = new string[] { "D", "C", "B", "A", "S", "SS", "SSS" };

    private Dictionary<StatType, Stat> _statDic;

    private CurrencyManager _currencyManager;
    private StatDataHandler _dataHandler;

    private bool _isInit;

    public void Init()
    {
        _isInit = true;
        _currencyManager = CurrencyManager.Instance;
        _dataHandler = DataManager.Instance.GetDataHandler<StatDataHandler>();
        _statDic = _dataHandler.LoadStatDictionary();
    }

    public void UpgradeStat(StatType type)
    {
        if (!_statDic.ContainsKey(type)) return;
        if (!_isInit) Init();
        _currencyManager.SubtractCurrency(CurrencyType.Gold, _statDic[type].CurUpgradeCost);
        _statDic[type].UpgradeStat();
        _dataHandler.SaveStatDictionary();
        OnStatUpgrade?.Invoke(type);
    }

    public Stat GetStat(StatType type)
    {
        if (!_isInit) Init();
        if (!_statDic.ContainsKey(type)) return null;
        return _statDic[type];
    }

    public Dictionary<StatType, Stat> GetStatDictionary()
    {
        if (!_isInit) Init();
        return _statDic;
    }
}
