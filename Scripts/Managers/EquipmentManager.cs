using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class EquipmentManager : Singleton<EquipmentManager>
{
    [SerializeField] private List<Weapon> _fistList;
    [SerializeField] private List<Weapon> _bowList;
    [SerializeField] private List<Suit> _clothesList;
    [SerializeField] private List<Suit> _shoesList;

    private readonly Dictionary<string, Equipment> _allEquipmentDic = new Dictionary<string, Equipment>();

    private readonly Dictionary<GradeType, List<Weapon>> _fistRarityDic = new Dictionary<GradeType, List<Weapon>>();
    private readonly Dictionary<GradeType, List<Weapon>> _bowRarityDic = new Dictionary<GradeType, List<Weapon>>();
    private readonly Dictionary<GradeType, List<Suit>> _clothesRarityDic = new Dictionary<GradeType, List<Suit>>();
    private readonly Dictionary<GradeType, List<Suit>> _shoesRarityDic = new Dictionary<GradeType, List<Suit>>();

    private CurrencyManager _currencyManager;
    private Player _player;
    private EquipmentDataHandler _dataHandler;

    private Equipment _recommendedFist;
    private Equipment _recommendedBow;
    private Equipment _recommendedClothes;
    private Equipment _recommendedShoes;

    private int _requiredCompositeQuantity = Numbers.Equipment.REQUIRED_QUANTITY_FOR_COMPOSITE;
    private int _maxLevel = Numbers.Equipment.LAST_EQUIPMENT_LEVEL;

    private bool _isInit;

    private void Start()
    {
        if (!_isInit) Init();
    }

    private void Init()
    {
        _isInit = true;
        _currencyManager = CurrencyManager.Instance;
        _player = GameManager.Instance.Player;
        _dataHandler = DataManager.Instance.GetDataHandler<EquipmentDataHandler>();
        _dataHandler.LoadEquipmentList(out _fistList, out _bowList, out _clothesList, out _shoesList);
        CreateAllEquipmentDic();
        CreateRarityDic();
        SetRecommendedEquipment();
        SetAllEquipments();
    }

    public Equipment GetCurEquippedEquipment(EquipmentType type)
    {
        if (!_isInit) Init();
        switch (type)
        {
            case EquipmentType.Fist:
                return _player.EquippedFist;
            case EquipmentType.Bow:
                return _player.EquippedBow;
            case EquipmentType.Clothes:
                return _player.EquippedClothes;
            case EquipmentType.Shoes:
                return _player.EquippedShoes;
        }
        return null;
    }

    public List<Weapon> GetWeaponList(EquipmentType type)
    {
        if (!_isInit) Init();
        switch (type)
        {
            case EquipmentType.Fist:
                return _fistList;
            case EquipmentType.Bow:
                return _bowList;
        }
        return null;
    }

    public List<Suit> GetSuitList(EquipmentType type)
    {
        if (!_isInit) Init();
        switch (type)
        {
            case EquipmentType.Clothes:
                return _clothesList;
            case EquipmentType.Shoes:
                return _shoesList;
        }
        return null;
    }

    public void Equip(int id, EquipmentType type)
    {
        //if (!_allEquipmentDic.ContainsKey(equipName)) return;
        //_player.Equip(_allEquipmentDic[equipName]);
        //_dataHandler.SaveEquipmentList(_allEquipmentDic[equipName].DataSO.Type);
        switch (type)
        {
            case EquipmentType.Fist:
                _player.Equip(_fistList[id]); break;
            case EquipmentType.Bow:
                _player.Equip(_bowList[id]); break;
            case EquipmentType.Clothes:
                _player.Equip(_clothesList[id]); break;
            case EquipmentType.Shoes:
                _player.Equip(_shoesList[id]); break;
        }
        _dataHandler.SaveEquipmentList(type);
    }

    public void UnEquip(string equipName)
    {
        if (!_allEquipmentDic.ContainsKey(equipName)) return;
        _player.UnEquip(_allEquipmentDic[equipName].DataSO.Type);
        _dataHandler.SaveEquipmentList(_allEquipmentDic[equipName].DataSO.Type);
    }

    // 장비 강화
    public void Enhance(Equipment equipment)
    {
        _currencyManager.SubtractCurrency(CurrencyType.EnhancementStone, equipment.RequiredEnhanceStone);
        equipment.Enhance();
        _dataHandler.SaveEquipmentList(equipment.DataSO.Type);
    }

    // 장비 합성
    public void Composite(Equipment equipment)
    {
        if (equipment.Quantity < _requiredCompositeQuantity) return;
        if (equipment.DataSO.Grade == GradeType.SS
            && equipment.DataSO.Level == _maxLevel) return;

        int compositeCount = equipment.Quantity / 4;
        equipment.Composite(-compositeCount * _requiredCompositeQuantity);
        GetNextEquipment(equipment).Composite(compositeCount);
        _dataHandler.SaveEquipmentList(equipment.DataSO.Type);
        SetRecommendedEquipment();
    }

    // 장비 일괄 합성
    public void AllComposite()
    {
        foreach(Equipment equipment in _allEquipmentDic.Values)
        {
            Composite(equipment);
        }
        _dataHandler.SaveAllEquipmentList();
        SetRecommendedEquipment();
    }

    public bool IsAllCompositable()
    {
        foreach (Equipment equipment in _allEquipmentDic.Values)
        {
            if (equipment.DataSO.Grade == GradeType.SS && equipment.DataSO.Level == _maxLevel) continue;
            if (equipment.Quantity >= _requiredCompositeQuantity) return true;
        }
        return false;
    }

    // 추천 장비 장착
    public void EquipRecommendedEquipment()
    {
        if (_recommendedFist == null || _recommendedBow == null
            || _recommendedClothes == null || _recommendedShoes == null)
            SetRecommendedEquipment();

        if (_player.EquippedFist != _recommendedFist) _player.Equip(_recommendedFist);
        if (_player.EquippedBow != _recommendedBow) _player.Equip(_recommendedBow);
        if (_player.EquippedClothes != _recommendedClothes) _player.Equip(_recommendedClothes);
        if (_player.EquippedShoes != _recommendedShoes) _player.Equip(_recommendedShoes);

        _dataHandler.SaveAllEquipmentList();
    }

    // 추천 장비 장착 여부 확인
    public bool IsEquipRecommended()
    {
        if (_recommendedFist == null || _recommendedBow == null
            || _recommendedClothes == null || _recommendedShoes == null)
            SetRecommendedEquipment();

        if (_player.EquippedFist != _recommendedFist) return true;
        if (_player.EquippedBow != _recommendedBow) return true;
        if (_player.EquippedClothes != _recommendedClothes) return true;
        if (_player.EquippedShoes != _recommendedShoes) return true;

        return false;
    }

    public List<Equipment> SummonEquipments(int count, SummonProbDataSO prob)
    {
        if (!_isInit) Init();
        List<Equipment> resultEquipmentList = new List<Equipment>(count);
        for (int i = 0; i < count; i++)
        {
            int typeRndNum = Random.Range(0, System.Enum.GetValues(typeof(EquipmentType)).Length);
            int rndNum = Random.Range(0, 1000);
            Equipment randomEquipment;
            if (rndNum < prob.GradeD)
                randomEquipment = GetRandomEquipment((EquipmentType)typeRndNum, GradeType.D);
            else if (rndNum < prob.GradeC)
                randomEquipment = GetRandomEquipment((EquipmentType)typeRndNum, GradeType.C);
            else if (rndNum < prob.GradeB)
                randomEquipment = GetRandomEquipment((EquipmentType)typeRndNum, GradeType.B);
            else if (rndNum < prob.GradeA)
                randomEquipment = GetRandomEquipment((EquipmentType)typeRndNum, GradeType.A);
            else if (rndNum < prob.GradeS)
                randomEquipment = GetRandomEquipment((EquipmentType)typeRndNum, GradeType.S);
            else /*if (rndNum < prob.GradeSS)*/
                randomEquipment = GetRandomEquipment((EquipmentType)typeRndNum, GradeType.SS);

            randomEquipment.ChangeQuantity(1);
            resultEquipmentList.Add(randomEquipment);
        }

        _dataHandler.SaveAllEquipmentList();
        return resultEquipmentList;
    }

    #region Create Equipment Dictionary
    // 전체 장비 Dictionary 생성
    private void CreateAllEquipmentDic()
    {
        for (int i = 0; i < _fistList.Count; i++)
            _allEquipmentDic.Add(_fistList[i].DataSO.EquipName, _fistList[i]);
        for (int i = 0; i < _bowList.Count; i++)
            _allEquipmentDic.Add(_bowList[i].DataSO.EquipName, _bowList[i]);
        for (int i = 0; i < _clothesList.Count; i++)
            _allEquipmentDic.Add(_clothesList[i].DataSO.EquipName, _clothesList[i]);
        for (int i = 0; i < _shoesList.Count; i++)
            _allEquipmentDic.Add(_shoesList[i].DataSO.EquipName, _shoesList[i]);
    }

    // 장비 등급별 Dictionary 생성
    private void CreateRarityDic()
    {
        for (int i = 0; i < _fistList.Count; i++)
        {
            GradeType key = _fistList[i].DataSO.Grade;
            if (!_fistRarityDic.ContainsKey(key))
                _fistRarityDic.Add(key, new List<Weapon>());
            _fistRarityDic[key].Add(_fistList[i]);
        }

        for (int i = 0; i < _bowList.Count; i++)
        {
            GradeType key = _bowList[i].DataSO.Grade;
            if (!_bowRarityDic.ContainsKey(key))
                _bowRarityDic.Add(key, new List<Weapon>());
            _bowRarityDic[key].Add(_bowList[i]);
        }

        for (int i = 0; i < _clothesList.Count; i++)
        {
            GradeType key = _clothesList[i].DataSO.Grade;
            if (!_clothesRarityDic.ContainsKey(key))
                _clothesRarityDic.Add(key, new List<Suit>());
            _clothesRarityDic[key].Add(_clothesList[i]);
        }

        for (int i = 0; i < _shoesList.Count; i++)
        {
            GradeType key = _shoesList[i].DataSO.Grade;
            if (!_shoesRarityDic.ContainsKey(key))
                _shoesRarityDic.Add(key, new List<Suit>());
            _shoesRarityDic[key].Add(_shoesList[i]);
        }
    }
    #endregion

    // IsEquipped == true인 장비 장착
    private void SetAllEquipments()
    {
        for (int i = 0; i < _fistList.Count; i++)
        {
            if (_fistList[i].IsEquipped) _player.Equip(_fistList[i]);
        }

        for (int i = 0; i < _bowList.Count; i++)
        {
            if (_bowList[i].IsEquipped) _player.Equip(_bowList[i]);
        }

        for (int i = 0; i < _clothesList.Count; i++)
        {
            if (_clothesList[i].IsEquipped) _player.Equip(_clothesList[i]);
        }

        for (int i = 0; i < _shoesList.Count; i++)
        {
            if (_shoesList[i].IsEquipped) _player.Equip(_shoesList[i]);
        }
    }

    // 다음 단계 장비를 구하는 메서드
    private Equipment GetNextEquipment(Equipment equipment)
    {
        GradeType _targetRarity;
        int _targetLevel;

        EquipmentDataSO dataSO = equipment.DataSO;
        if (dataSO.Level < _maxLevel)
        {
            _targetRarity = dataSO.Grade;
            _targetLevel = dataSO.Level + 1;
        } else
        {
            _targetRarity = (GradeType)((int)dataSO.Grade + 1);
            _targetLevel = 1;
        }

        switch (dataSO.Type)
        {
            case EquipmentType.Fist:
                return _fistRarityDic[_targetRarity][_targetLevel - 1];
            case EquipmentType.Bow:
                return _bowRarityDic[_targetRarity][_targetLevel - 1];
            case EquipmentType.Clothes:
                return _clothesRarityDic[_targetRarity][_targetLevel - 1];
            case EquipmentType.Shoes:
                return _shoesRarityDic[_targetRarity][_targetLevel - 1];
            default:
                return null;
        }
    }

    // 추천 장비 설정
    private void SetRecommendedEquipment()
    {
        if (_recommendedFist == null) _recommendedFist = _fistList[0];
        for (int i = 0; i < _fistList.Count; i++)
        {
            if (!_fistList[i].IsLocked && _fistList[i].EquippedEffect > _recommendedFist.EquippedEffect)
                _recommendedFist = _fistList[i];
        }

        if (_recommendedBow == null) _recommendedBow = _bowList[0];
        for (int i = 0; i < _bowList.Count; i++)
        {
            if (!_bowList[i].IsLocked && _bowList[i].EquippedEffect > _recommendedBow.EquippedEffect)
                _recommendedBow = _bowList[i];
        }

        if (_recommendedClothes == null) _recommendedClothes = _clothesList[0];
        for (int i = 0; i < _clothesList.Count; i++)
        {
            if (!_clothesList[i].IsLocked && _clothesList[i].EquippedEffect > _recommendedClothes.EquippedEffect)
                _recommendedClothes = _clothesList[i];
        }

        if (_recommendedShoes == null) _recommendedShoes = _shoesList[0];
        for (int i = 0; i < _shoesList.Count; i++)
        {
            if (!_shoesList[i].IsLocked && _shoesList[i].EquippedEffect > _recommendedShoes.EquippedEffect)
                _recommendedShoes = _shoesList[i];
        }
    }

    // 특정 등급의 랜덤 장비 반환
    private Equipment GetRandomEquipment(EquipmentType equipType, GradeType gradeType)
    {
        int rndIdx = 0;
        switch (equipType)
        {
            case EquipmentType.Fist:
                rndIdx = Random.Range(0, _fistRarityDic[gradeType].Count);
                return _fistRarityDic[gradeType][rndIdx];
            case EquipmentType.Bow:
                rndIdx = Random.Range(0, _bowRarityDic[gradeType].Count);
                return _bowRarityDic[gradeType][rndIdx];
            case EquipmentType.Clothes:
                rndIdx = Random.Range(0, _clothesRarityDic[gradeType].Count);
                return _clothesRarityDic[gradeType][rndIdx];
            case EquipmentType.Shoes:
                rndIdx = Random.Range(0, _shoesRarityDic[gradeType].Count);
                return _shoesRarityDic[gradeType][rndIdx];
        }
        return null;
    }
}
