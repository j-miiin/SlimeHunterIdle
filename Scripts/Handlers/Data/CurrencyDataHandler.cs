using Keiwando.BigInteger;
using System.Collections.Generic;
using static Enums;
using static Numbers.Currency;

public class CurrencyDataHandler : DataHandler
{
    private Dictionary<CurrencyType, BigInteger> _currencyDic;

    public void SaveCurrencies()
    {
        ES3.Save<Dictionary<CurrencyType, string>>("currencyDic", ConvertCurrencyBigIntegerToString(_currencyDic));
    }

    public Dictionary<CurrencyType, BigInteger> LoadCurrencies()
    {
        if (ES3.KeyExists("currencyDic"))
        {
            _currencyDic = ConvertCurrencyStringToBigInteger(ES3.Load<Dictionary<CurrencyType, string>>("currencyDic"));
        }
        else
        {
            _currencyDic = new Dictionary<CurrencyType, BigInteger>()
            {
                { CurrencyType.Gold, INITIAL_GOLD },
                { CurrencyType.Gem, INITIAL_GEM },
                { CurrencyType.EnhancementStone, INITIAL_ENHANCEMENT_STONE }
            };
        }
        SaveCurrencies();
        return _currencyDic;
    }

    private Dictionary<CurrencyType, string> ConvertCurrencyBigIntegerToString(Dictionary<CurrencyType, BigInteger> dic)
    {
        Dictionary<CurrencyType, string> result = new Dictionary<CurrencyType, string>();
        foreach(KeyValuePair<CurrencyType, BigInteger> pair in dic)
        {
            if (result.ContainsKey(pair.Key)) continue;
            result.Add(pair.Key, pair.Value.ToString());
        }
        return result;
    }

    private Dictionary<CurrencyType, BigInteger> ConvertCurrencyStringToBigInteger(Dictionary<CurrencyType, string> dic)
    {
        Dictionary<CurrencyType, BigInteger> result = new Dictionary<CurrencyType, BigInteger>();
        foreach (KeyValuePair<CurrencyType, string> pair in dic)
        {
            if (result.ContainsKey(pair.Key)) continue;
            result.Add(pair.Key, new BigInteger(pair.Value));
        }
        return result;
    }
}
