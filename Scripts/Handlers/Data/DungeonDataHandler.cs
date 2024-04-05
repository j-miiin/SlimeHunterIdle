using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class DungeonDataHandler : DataHandler
{
    private Dictionary<DungeonType, Dungeon> _dungeonDic;
    private Dictionary<DungeonType, DungeonDataSO> _dataSODic;

    public void SaveDungeons()
    {
        ES3.Save<Dictionary<DungeonType, Dungeon>>("dungeonDic", _dungeonDic);
    }

    public Dictionary<DungeonType, Dungeon> LoadDungeons()
    {
        LoadDungeonDataSO();
        if (ES3.KeyExists("dungeonDic"))
        {
            _dungeonDic = ES3.Load<Dictionary<DungeonType, Dungeon>>("dungeonDic");
            SetDungeonDataSOs();
        }
        else
        {
            CreateDungeonDatas();
            SaveDungeons();
        }
        return _dungeonDic;
    }

    private void LoadDungeonDataSO()
    {
        _dataSODic = Resources.Load<DungeonDicDataSO>(Strings.Datas.DUNGEON_DATA_PATH).DungeonDic;
    }

    private void CreateDungeonDatas()
    {
        _dungeonDic = new Dictionary<DungeonType, Dungeon>()
            {
                {DungeonType.Gold, new GoldDungeon(_dataSODic[DungeonType.Gold]) },
                {DungeonType.Equipment, new EquipmentDungeon(_dataSODic[DungeonType.Equipment]) },
                {DungeonType.Infinite, new InfiniteDungeon(_dataSODic[DungeonType.Infinite]) },
            };
    }

    private void SetDungeonDataSOs()
    {
        foreach (Dungeon dungeon in _dungeonDic.Values)
        {
            dungeon.SetDataSO(_dataSODic[dungeon.Type]);
        }
    }
}
