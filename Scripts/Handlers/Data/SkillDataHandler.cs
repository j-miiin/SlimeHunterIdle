using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class SkillDataHandler : DataHandler
{
    private List<Skill> _fistSkillList;
    private List<Skill> _bowSkillList;
    private List<Skill> _passiveSkillList;

    private SkillListDataSO _dataSO;

    public void SaveAllSkillList()
    {
        ES3.Save<List<Skill>>("fistSkillList", _fistSkillList);
        ES3.Save<List<Skill>>("bowSkillList", _bowSkillList);
        ES3.Save<List<Skill>>("passiveSkillList", _passiveSkillList);
    }

    public void SaveSkillList(SkillEquipType type)
    {
        switch (type)
        {
            case SkillEquipType.Fist:
                ES3.Save<List<Skill>>("fistSkillList", _fistSkillList);
                break;
            case SkillEquipType.Bow:
                ES3.Save<List<Skill>>("bowSkillList", _bowSkillList);
                break;
            case SkillEquipType.Passive:
                ES3.Save<List<Skill>>("passiveSkillList", _passiveSkillList);
                break;
        }
    }

    public void LoadSkillList(out List<Skill> swordSkillList, out List<Skill> daggerSkillList,
        out List<Skill> passiveSKillList)
    {
        LoadSkillListDataSO();
        if (ES3.KeyExists("fistSkillList"))
        {
            _fistSkillList = ES3.Load<List<Skill>>("fistSkillList");
            SetFistSkillDataSO();
        }
        else
        {
            CreateFistSkillList();
        }

        if (ES3.KeyExists("bowSkillList"))
        {
            _bowSkillList = ES3.Load<List<Skill>>("bowSkillList");
            SetBowSkillDataSO();
        }
        else
        {
            CreateBowSkillList();
        }

        if (ES3.KeyExists("passiveSkillList"))
        {
            _passiveSkillList = ES3.Load<List<Skill>>("passiveSkillList");
            SetPassiveSkillDataSO();
        }
        else
        {
            CreatePassiveSkillList();
        }

        swordSkillList = _fistSkillList;
        daggerSkillList = _bowSkillList;
        passiveSKillList = _passiveSkillList;
    }

    private void LoadSkillListDataSO()
    {
        _dataSO = Resources.Load<SkillListDataSO>(Strings.Datas.SKILL_DATA_PATH);
    }

    #region Create Skill List
    private void CreateFistSkillList()
    {
        _fistSkillList = new List<Skill>();
        for (int i = 0; i < _dataSO.FistSkillDataList.Count; i++)
            _fistSkillList.Add(new Skill(_dataSO.FistSkillDataList[i]));
        _fistSkillList[0].UnLock();
        SaveSkillList(SkillEquipType.Fist);
    }

    private void CreateBowSkillList()
    {
        _bowSkillList = new List<Skill>();
        for (int i = 0; i < _dataSO.BowSkillDataList.Count; i++)
            _bowSkillList.Add(new Skill(_dataSO.BowSkillDataList[i]));
        _bowSkillList[0].UnLock();
        SaveSkillList(SkillEquipType.Bow);
    }

    private void CreatePassiveSkillList()
    {
        _passiveSkillList = new List<Skill>();
        for (int i = 0; i < _dataSO.PassiveSkillDataList.Count; i++)
            _passiveSkillList.Add(new Skill(_dataSO.PassiveSkillDataList[i]));
        _passiveSkillList[0].UnLock();
        SaveSkillList(SkillEquipType.Passive);
    }
    #endregion

    #region Set Skill Data SO
    private void SetFistSkillDataSO()
    {
        for (int i = 0; i < _dataSO.FistSkillDataList.Count; i++)
        {
            _fistSkillList[i].SetDataSO(_dataSO.FistSkillDataList[i]);
        }
    }

    private void SetBowSkillDataSO()
    {
        for (int i = 0; i < _dataSO.BowSkillDataList.Count; i++)
        {
            _bowSkillList[i].SetDataSO(_dataSO.BowSkillDataList[i]);
        }
    }

    private void SetPassiveSkillDataSO()
    {
        for (int i = 0; i < _dataSO.PassiveSkillDataList.Count; i++)
        {
            _passiveSkillList[i].SetDataSO(_dataSO.PassiveSkillDataList[i]);
        }
    }
    #endregion
}
