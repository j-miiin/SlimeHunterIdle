using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class StatDataHandler : DataHandler
{
    private Dictionary<StatType, Stat> _statDic;

    public void SaveStatDictionary()
    {
        ES3.Save<Dictionary<StatType, Stat>>("statDic", _statDic);
    }

    public Dictionary<StatType, Stat> LoadStatDictionary()
    {
        if (ES3.KeyExists("statDic"))
        {
            _statDic = ES3.Load<Dictionary<StatType, Stat>>("statDic");
        } else
        {
            CreateStatDictionary();
        }
        LoadStatDataSODictionary();
        SaveStatDictionary();
        return _statDic;
    }

    public void LoadStatDataSODictionary()
    {
        StatDataSO[] dataSOList = Resources.LoadAll<StatDataSO>(Strings.Datas.STAT_DATA_PATH);
        for (int i = 0; i < dataSOList.Length; i++)
        {
            StatType key = dataSOList[i].Type;
            if (_statDic.ContainsKey(key))
                _statDic[key].SetStatDataSO(dataSOList[i]);
            else
                _statDic.Add(key, new Stat(dataSOList[i]));
        }
    }

    public void CreateStatDictionary()
    {
        _statDic = new Dictionary<StatType, Stat>();
        StatDataSO[] dataSOList = Resources.LoadAll<StatDataSO>(Strings.Datas.STAT_DATA_PATH);
        for (int i = 0; i < dataSOList.Length; i++)
        {
            StatType key = dataSOList[i].Type;
            if (_statDic.ContainsKey(key))
            {
#if UNITY_EDITOR

                Debug.LogError("Stat already exist");
#endif
                continue;
            }
            _statDic.Add(key, new Stat(dataSOList[i]));
        }
    }
}
