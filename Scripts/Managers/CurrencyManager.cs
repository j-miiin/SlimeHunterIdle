using Keiwando.BigInteger;
using System;
using System.Collections.Generic;
using static Enums;

public class CurrencyManager : Singleton<CurrencyManager>
{
    public event Action<CurrencyType> OnCurrencyUpdate;

    private Dictionary<CurrencyType, BigInteger> _currencyDic;
    
    private CurrencyDataHandler _dataHandler;

    public void Init()
    {
        _dataHandler = DataManager.Instance.GetDataHandler<CurrencyDataHandler>();
        _currencyDic = _dataHandler.LoadCurrencies();
        foreach (KeyValuePair<CurrencyType, BigInteger> currency in _currencyDic)
        {
            OnCurrencyUpdate?.Invoke(currency.Key);
        }
    }

    // 특정 재화를 증가시키는 메서드
    public void AddCurrency(CurrencyType currencyType, BigInteger value)
    {
        if (!_currencyDic.ContainsKey(currencyType)) return;

        _currencyDic[currencyType] += value;
        OnCurrencyUpdate?.Invoke(currencyType);
        _dataHandler.SaveCurrencies();
    }

    // 특정 재화를 감소시키는 메서드
    public bool SubtractCurrency(CurrencyType currencyType, BigInteger value)
    {
        if (!_currencyDic.ContainsKey(currencyType)
            || _currencyDic[currencyType] - value < 0) return false;

        _currencyDic[currencyType] -= value;
        OnCurrencyUpdate?.Invoke(currencyType);
        _dataHandler.SaveCurrencies();

        return true;
    }

    // 특정 재화의 현재 양을 반환하는 메서드
    public BigInteger GetCurrencyAmount(CurrencyType currencyType)
    {
        if (!_currencyDic.ContainsKey(currencyType)) return -1;
        return _currencyDic[currencyType];
    }
}
