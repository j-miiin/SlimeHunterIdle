using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestListDataSO", menuName = "Data/Quest/QuestListDataSO", order = 0)]
public class QuestListDataSO : ScriptableObject
{
    [SerializeField] private List<QuestDataSO> _dailyQuestDataList;
    [SerializeField] private List<QuestDataSO> _repeatQuestDataList;
    [SerializeField] private List<QuestDataSO> _achievementQuestDataList;

    public List<QuestDataSO> DailyQuestDataList => _dailyQuestDataList;
    public List<QuestDataSO> RepeatQuestDataList => _repeatQuestDataList;
    public List<QuestDataSO> AchievementQuestDataList => _achievementQuestDataList;
}
