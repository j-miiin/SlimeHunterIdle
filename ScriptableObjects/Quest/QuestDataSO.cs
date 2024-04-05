using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "QuestDataSO", menuName = "Data/Quest/QuestDataSO", order = 0)]
public class QuestDataSO : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private QuestType _questType;
    [SerializeField] private QuestTaskType _questTaskType;
    [SerializeField] private int _requiredQuestValue;
    [SerializeField] private RewardType _rewardType;
    [SerializeField] private int _rewardValue;

    public string ID => _id;
    public QuestType QuestType => _questType;
    public QuestTaskType QuestTaskType => _questTaskType;
    public int RequiredQuestValue => _requiredQuestValue;
    public RewardType RewardType => _rewardType;
    public int RewardValue => _rewardValue;
}
