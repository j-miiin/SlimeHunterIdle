using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "DungeonDataSO", menuName = "Data/Dungeon/DungeonDataSO", order = 0)]
public class DungeonDataSO : SerializedScriptableObject
{
    [SerializeField] private DungeonType _type;
    [SerializeField] private string _dungeonName;
    [SerializeField][TextArea] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _time;
    [SerializeField] private List<List<Reward>> _rewardList;

    public DungeonType Type => _type;
    public string DungeonName => _dungeonName;
    public string Description => _description;
    public Sprite Icon => _icon;
    public float Time => _time;
    public List<List<Reward>> RewardList => _rewardList;
}
