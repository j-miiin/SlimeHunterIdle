using static Enums;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDataHandler : DataHandler
{
    private List<Weapon> _fistList;
    private List<Weapon> _bowList;
    private List<Suit> _clothesList;
    private List<Suit> _shoesList;

    private EquipmentListDataSO _dataSO;

    public void SaveAllEquipmentList()
    {
        ES3.Save<List<Weapon>>("fistList", _fistList);
        ES3.Save<List<Weapon>>("bowList", _bowList);
        ES3.Save<List<Suit>>("clothesList", _clothesList);
        ES3.Save<List<Suit>>("shoesList", _shoesList);
    }

    public void SaveEquipmentList(EquipmentType type)
    {
        switch (type)
        {
            case EquipmentType.Fist:
                ES3.Save<List<Weapon>>("fistList", _fistList);
                break;
            case EquipmentType.Bow:
                ES3.Save<List<Weapon>>("bowList", _bowList);
                break;
            case EquipmentType.Clothes:
                ES3.Save<List<Suit>>("clothesList", _clothesList);
                break;
            case EquipmentType.Shoes:
                ES3.Save<List<Suit>>("shoesList", _shoesList);
                break;
        }
    }

    public void LoadEquipmentList(out List<Weapon> fistList, out List<Weapon> bowList,
        out List<Suit> clothesList, out List<Suit> shoesList)
    {
        LoadEquipmentListDataSO();
        if (ES3.KeyExists("fistList")) _fistList = ES3.Load<List<Weapon>>("fistList");
        else CreateFistList();

        if (ES3.KeyExists("bowList")) _bowList = ES3.Load<List<Weapon>>("bowList");
        else CreateBowList();

        if (ES3.KeyExists("clothesList")) _clothesList = ES3.Load<List<Suit>>("clothesList");
        else CreateClothesList();

        if (ES3.KeyExists("shoesList")) _shoesList = ES3.Load<List<Suit>>("shoesList");
        else CreateShoesList();

        SetFistDataSO();
        SetBowDataSO();
        SetClothesDataSO();
        SetShoesDataSO();

        SaveAllEquipmentList();

        fistList = _fistList;
        bowList = _bowList;
        clothesList = _clothesList;
        shoesList = _shoesList;
    }

    private void LoadEquipmentListDataSO()
    {
        _dataSO = Resources.Load<EquipmentListDataSO>(Strings.Datas.EQUIPMENT_DATA_PATH);
    }

    #region Create Equipment List
    private void CreateFistList()
    {
        _fistList = new List<Weapon>();
        for (int i = 0; i < _dataSO.FistDataList.Count; i++)
            _fistList.Add(new Weapon(_dataSO.FistDataList[i]));
        _fistList[0].UnLock();
        SaveEquipmentList(EquipmentType.Fist);
    }

    private void CreateBowList()
    {
        _bowList = new List<Weapon>();
        for (int i = 0; i < _dataSO.BowDataList.Count; i++)
            _bowList.Add(new Weapon(_dataSO.BowDataList[i]));
        _bowList[0].UnLock();
        SaveEquipmentList(EquipmentType.Bow);
    }

    private void CreateClothesList()
    {
        _clothesList = new List<Suit>();
        for (int i = 0; i < _dataSO.ClothesDataList.Count; i++)
            _clothesList.Add(new Suit(_dataSO.ClothesDataList[i]));
        _clothesList[0].UnLock();
        SaveEquipmentList(EquipmentType.Clothes);
    }

    private void CreateShoesList()
    {
        _shoesList = new List<Suit>();
        for (int i = 0; i < _dataSO.ShoesDataList.Count; i++)
            _shoesList.Add(new Suit(_dataSO.ShoesDataList[i]));
        _shoesList[0].UnLock();
        SaveEquipmentList(EquipmentType.Shoes);
    }
    #endregion

    #region Set Equipment Data SO
    private void SetFistDataSO()
    {
        for (int i = 0; i < _dataSO.FistDataList.Count; i++)
            _fistList[i].SetDataSO(_dataSO.FistDataList[i] as WeaponDataSO);
    }

    private void SetBowDataSO()
    {
        for (int i = 0; i < _dataSO.BowDataList.Count; i++)
            _bowList[i].SetDataSO(_dataSO.BowDataList[i] as WeaponDataSO);
    }

    private void SetClothesDataSO()
    {
        for (int i = 0; i < _dataSO.ClothesDataList.Count; i++)
            _clothesList[i].SetDataSO(_dataSO.ClothesDataList[i]);
    }

    private void SetShoesDataSO()
    {
        for (int i = 0; i < _dataSO.ShoesDataList.Count; i++)
            _shoesList[i].SetDataSO(_dataSO.ShoesDataList[i]);
    }
    #endregion
}
