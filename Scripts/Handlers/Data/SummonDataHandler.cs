using System.Collections.Generic;
using UnityEngine;
using static Enums;
using static Strings.Datas;

public class SummonDataHandler : DataHandler
{
    private Dictionary<SummonType, Summon> _summonDic;
    private SummonDataSO[] _summonDataSOList;

    // 소환 정보 리스트 저장
    public void SaveSummonDictionary()
    {
        ES3.Save<Dictionary<SummonType, Summon>>("summonDic", _summonDic);
    }

    // 소환 정보 리스트 로드
    public Dictionary<SummonType, Summon> LoadSummonDictionary()
    {
        LoadSummonDataSOList();
        if (ES3.KeyExists("summonDic"))
        {
            _summonDic = ES3.Load<Dictionary<SummonType, Summon>>("summonDic");
            SetSummonDataSO();
        }
        else
        {
            CreateSummonList();
        }
        return _summonDic;
    }

    private void LoadSummonDataSOList()
    {
        _summonDataSOList = Resources.LoadAll<SummonDataSO>(SUMMON_DATA_PATH);
    }

    private void CreateSummonList()
    {
        _summonDic = new Dictionary<SummonType, Summon>(_summonDataSOList.Length);
        for (int i = 0; i < _summonDataSOList.Length; i++)
        {
            SummonType key = _summonDataSOList[i].Type;
            _summonDic.Add(key, new Summon(_summonDataSOList[i]));
        }
        SaveSummonDictionary();
    }

    private void SetSummonDataSO()
    {
        for (int i = 0; i < _summonDataSOList.Length; i++)
        {
            SummonType key = _summonDataSOList[i].Type;
            _summonDic[key].SetDataSO(_summonDataSOList[i]);
        }
    }
}
