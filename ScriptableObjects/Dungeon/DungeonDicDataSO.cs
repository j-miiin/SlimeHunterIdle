using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "DungeonDicDataSO", menuName = "Data/Dungeon/DungeonDicDataSO", order = 0)]
public class DungeonDicDataSO : SerializedScriptableObject
{
    [SerializeField] private Dictionary<DungeonType, DungeonDataSO> _dungeonDic;

    public Dictionary<DungeonType, DungeonDataSO> DungeonDic => _dungeonDic;
}
