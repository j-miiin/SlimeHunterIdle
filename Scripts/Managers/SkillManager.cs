using System;
using System.Collections.Generic;
using UnityEngine;
using static Enums;
using static Numbers;
using static Numbers.Skill;
using Random = UnityEngine.Random;

public class SkillManager : Singleton<SkillManager>
{
    public event Action<SkillEquipType> OnEquippedSkillUpdate;

    [SerializeField] private List<Skill> _fistSkillList;
    [SerializeField] private List<Skill> _bowSkillList;
    [SerializeField] private List<Skill> _passiveSkillList;

    private readonly Dictionary<int, Skill> _allSkillDic = new Dictionary<int, Skill>();

    private readonly Dictionary<GradeType, List<Skill>> _fistSkillRarityDic = new Dictionary<GradeType, List<Skill>>();
    private readonly Dictionary<GradeType, List<Skill>> _bowSkillRarityDic = new Dictionary<GradeType, List<Skill>>();
    private readonly Dictionary<GradeType, List<Skill>> _passiveSkillRarityDic = new Dictionary<GradeType, List<Skill>>();

    private Dictionary<SkillEquipType, Skill[]> _equippedSkillDic;

    private Player _player;
    private SkillDataHandler _dataHandler;

    private int _maxSkillCount = MAX_SKILL_COUNT;
    private int _curUsableSkillCount;
    private Skill[] _curEquippedSkillList;

    private void Update()
    {
        for (int i = 0; i < _curEquippedSkillList.Length; i++)
        {
            if (_curEquippedSkillList[i] == null) continue;
            _curEquippedSkillList[i].UpdateElapsedCoolTime(Time.deltaTime);
            if ((_curEquippedSkillList[i].ElapsedCoolTime >= _curEquippedSkillList[i].DataSO.CoolTime)
                && (_player.PlayerController.ClosestTarget != null))
            {
                _curEquippedSkillList[i].UpdateElapsedCoolTime(0, true);
                _curEquippedSkillList[i].Execute(_player);
            }
        }
    }

    public void Init()
    {
        _player = GameManager.Instance.Player;
        _dataHandler = DataManager.Instance.GetDataHandler<SkillDataHandler>();
        _dataHandler.LoadSkillList(out _fistSkillList, out _bowSkillList, out _passiveSkillList);
        _equippedSkillDic = new Dictionary<SkillEquipType, Skill[]>
        {
            { SkillEquipType.Fist, new Skill[6] },
            { SkillEquipType.Bow, new Skill[6] },
            { SkillEquipType.Passive, new Skill[6] },
        };
        CreateAllSkillDic();
        CreateRarityDic();
        _curUsableSkillCount = (_player.Level % 200) + 1;
        if (_curUsableSkillCount > _maxSkillCount) _curUsableSkillCount = _maxSkillCount;
        ChangeSkillEquipType(_player.PlayerController.IsCloseAttack ? SkillEquipType.Fist : SkillEquipType.Bow);
        _player.PlayerController.OnAttackTypeChanged += (bool IsCloseAttack) =>
        {
            ChangeSkillEquipType((IsCloseAttack) ? SkillEquipType.Fist : SkillEquipType.Bow);
        };
        SetAllSkills();
    }

    public List<Skill> GetSkillList(SkillEquipType type)
    {
        switch (type)
        {
            case SkillEquipType.Fist:
                return _fistSkillList;
            case SkillEquipType.Bow:
                return _bowSkillList;
            case SkillEquipType.Passive:
                return _passiveSkillList;
        }
        return null;
    }

    public Skill[] GetEquippedSkillList(SkillEquipType type)
    {
        if (!_equippedSkillDic.ContainsKey(type)) return null;
        return _equippedSkillDic[type];
    }

    public bool Equip(Skill skill)
    {
        SkillEquipType key = skill.DataSO.SkillEquipType;
        //if (_equippedSkillDic[key].Length >= _curUsableSkillCount) return false;
        for (int i = 0; i < _equippedSkillDic[key].Length; i++)
        {
            if (_equippedSkillDic[key][i] == null)
            {
                _equippedSkillDic[key][i] = skill;
                skill.Equip(i);
                if (key != SkillEquipType.Passive) CreateSkillAttack(skill);
                if (_player.PlayerController.ClosestTarget != null) skill.Execute(_player);
                OnEquippedSkillUpdate?.Invoke(key);
                _dataHandler.SaveSkillList(key);
                return true;
            }
        }
        return false;
    }

    public void Equip(Skill skill, int idx)
    {
        SkillEquipType key = skill.DataSO.SkillEquipType;
        _equippedSkillDic[key][idx] = skill;
        skill.Equip(idx);
        if (key != SkillEquipType.Passive) CreateSkillAttack(skill);
        if (_player.PlayerController.ClosestTarget != null) skill.Execute(_player);
        OnEquippedSkillUpdate?.Invoke(key);
        _dataHandler.SaveSkillList(key);
    }

    public void UnEquip(Skill skill)
    {
        SkillEquipType key = skill.DataSO.SkillEquipType;
        for (int i = 0; i < _equippedSkillDic[key].Length; i++)
        {
            if (_equippedSkillDic[key][i] == skill)
            {
                _equippedSkillDic[key][i].UnEquip();
                _equippedSkillDic[key][i].Clear();
                _equippedSkillDic[key][i] = null;
                break;
            }
        }
        OnEquippedSkillUpdate?.Invoke(key);
        _dataHandler.SaveSkillList(key);
    }

    public void ChangeSkillEquipType(SkillEquipType type)
    {
        if (!_equippedSkillDic.ContainsKey(type)) return;
        _curEquippedSkillList = _equippedSkillDic[type];
        for (int i = 0; i < _curEquippedSkillList.Length; i++)
        {
            if (_curEquippedSkillList[i] != null) _curEquippedSkillList[i].UpdateElapsedCoolTime(0, true);
        }
        OnEquippedSkillUpdate?.Invoke(type);
    }

    // 스킬 일괄 레벨업
    public void AllLevelUp()
    {
        foreach (Skill skill in _allSkillDic.Values)
        {
            if (skill.IsPossibleLevelUp()) skill.LevelUp();
        }
        _dataHandler.SaveAllSkillList();
    }

    public bool IsPossibleAllLevelUp()
    {
        foreach (Skill skill in _allSkillDic.Values)
        {
            if (skill.IsPossibleLevelUp()) return true;
        }
        return false;
    }

    public List<Skill> SummonSkills(int count, SummonProbDataSO prob)
    {
        List<Skill> resultSkillList = new List<Skill>(count);
        for (int i = 0; i < count; i++)
        {
            int typeRndNum = Random.Range(0, Enum.GetValues(typeof(SkillEquipType)).Length);
            int rndNum = Random.Range(0, 1000);
            Skill randomSkill;
            if (rndNum < prob.GradeD)
                randomSkill = GetRandomSkill((SkillEquipType)typeRndNum, GradeType.D);
            else if (rndNum < prob.GradeC)
                randomSkill = GetRandomSkill((SkillEquipType)typeRndNum, GradeType.C);
            else if (rndNum < prob.GradeB)
                randomSkill = GetRandomSkill((SkillEquipType)typeRndNum, GradeType.B);
            else if (rndNum < prob.GradeA)
                randomSkill = GetRandomSkill((SkillEquipType)typeRndNum, GradeType.A);
            else if (rndNum < prob.GradeS)
                randomSkill = GetRandomSkill((SkillEquipType)typeRndNum, GradeType.S);
            else /*if (rndNum < prob.GradeSS)*/
                randomSkill = GetRandomSkill((SkillEquipType)typeRndNum, GradeType.SS);

            randomSkill.ChangeQuantity(1);
            resultSkillList.Add(randomSkill);
        }

        _dataHandler.SaveAllSkillList();
        return resultSkillList;
    }

    #region Create Skill Dictionary
    // 전체 스킬 Dictionary 생성
    private void CreateAllSkillDic()
    {
        for (int i = 0; i < _fistSkillList.Count; i++)
            _allSkillDic.Add(_fistSkillList[i].DataSO.ID, _fistSkillList[i]);
        for (int i = 0; i < _bowSkillList.Count; i++)
            _allSkillDic.Add(_bowSkillList[i].DataSO.ID, _bowSkillList[i]);
        for (int i = 0; i < _passiveSkillList.Count; i++)
            _allSkillDic.Add(_passiveSkillList[i].DataSO.ID, _passiveSkillList[i]);
    }

    // 스킬 등급별 Dictionary 생성
    private void CreateRarityDic()
    {
        for (int i = 0; i < _fistSkillList.Count; i++)
        {
            GradeType key = _fistSkillList[i].DataSO.Grade;
            if (!_fistSkillRarityDic.ContainsKey(key))
                _fistSkillRarityDic.Add(key, new List<Skill>());
            _fistSkillRarityDic[key].Add(_fistSkillList[i]);
        }

        for (int i = 0; i < _bowSkillList.Count; i++)
        {
            GradeType key = _bowSkillList[i].DataSO.Grade;
            if (!_bowSkillRarityDic.ContainsKey(key))
                _bowSkillRarityDic.Add(key, new List<Skill>());
            _bowSkillRarityDic[key].Add(_bowSkillList[i]);
        }

        for (int i = 0; i < _passiveSkillList.Count; i++)
        {
            GradeType key = _passiveSkillList[i].DataSO.Grade;
            if (!_passiveSkillRarityDic.ContainsKey(key))
                _passiveSkillRarityDic.Add(key, new List<Skill>());
            _passiveSkillRarityDic[key].Add(_passiveSkillList[i]);
        }
    }
    #endregion

    private void SetAllSkills()
    {
        for (int i = 0; i < _fistSkillList.Count; i++)
        {
            if (_fistSkillList[i].EquippedIdx != -1) Equip(_fistSkillList[i]);
        }

        for (int i = 0; i < _bowSkillList.Count; i++)
        {
            if (_bowSkillList[i].EquippedIdx != -1) Equip(_bowSkillList[i]);
        }

        for (int i = 0; i < _passiveSkillList.Count; i++)
        {
            if (_passiveSkillList[i].EquippedIdx != -1) Equip(_passiveSkillList[i]);
        }
    }

    private void CreateSkillAttack(Skill skill)
    {
        GameObject skillAttackObj = Instantiate(skill.DataSO.SkillAttack, parent: transform);
        skill.SkillAttack = skillAttackObj.GetComponent<SkillAttack>();
    }

    // 특정 등급의 랜덤 스킬 반환
    private Skill GetRandomSkill(SkillEquipType equipType, GradeType gradeType)
    {
        int rndIdx = 0;
        switch (equipType)
        {
            case SkillEquipType.Fist:
                rndIdx = Random.Range(0, _fistSkillRarityDic[gradeType].Count);
                return _fistSkillRarityDic[gradeType][rndIdx];
            case SkillEquipType.Bow:
                rndIdx = Random.Range(0, _bowSkillRarityDic[gradeType].Count);
                return _bowSkillRarityDic[gradeType][rndIdx];
            case SkillEquipType.Passive:
                rndIdx = Random.Range(0, _passiveSkillRarityDic[gradeType].Count);
                return _passiveSkillRarityDic[gradeType][rndIdx];
        }
        return null;
    }
}
