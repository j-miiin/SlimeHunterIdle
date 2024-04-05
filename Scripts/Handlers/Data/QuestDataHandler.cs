using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class QuestDataHandler : DataHandler
{
    private Dictionary<QuestType, List<Quest>> _questDic;
    private QuestListDataSO _questListData;

    public void SaveQuestDictionary()
    {
        ES3.Save<Dictionary<QuestType, List<Quest>>>("questDic", _questDic);
    }

    public void SaveRepeatQuestIdx(int idx)
    {
        ES3.Save<int>("repeatQuestIdx", idx);
    }

    public Dictionary<QuestType, List<Quest>> LoadQuestDictionary()
    {
        LoadQuestDataSOList();
        if (ES3.KeyExists("questDic"))
        {
            _questDic = ES3.Load<Dictionary<QuestType, List<Quest>>>("questDic");
            SetQuestDataSO();
        }
        else
        {
            CreateQuestDictionary();
        }
        return _questDic;
    }

    public int LoadRepeatQuestIdx()
    {
        int result = 0;
        if (ES3.KeyExists("repeatQuestIdx"))
            result = ES3.Load<int>("repeatQuestIdx");
        return result;
    }

    private void LoadQuestDataSOList()
    {
        _questListData = Resources.Load<QuestListDataSO>(Strings.Datas.QUEST_DATA_PATH);
    }

    private void CreateQuestDictionary()
    {
        _questDic = new Dictionary<QuestType, List<Quest>>
        {
            { QuestType.Daily, new List<Quest>() },
            { QuestType.Repeat, new List<Quest>() },
            { QuestType.Achievement, new List<Quest>() }
        };
        for (int i = 0; i < _questListData.DailyQuestDataList.Count; i++)
            _questDic[QuestType.Daily].Add(new DailyQuest(_questListData.DailyQuestDataList[i]));

        for (int i = 0; i < _questListData.RepeatQuestDataList.Count; i++)
            _questDic[QuestType.Repeat].Add(new RepeatQuest(_questListData.RepeatQuestDataList[i]));

        for (int i = 0; i < _questListData.AchievementQuestDataList.Count; i++)
            _questDic[QuestType.Achievement].Add(new AchievementQuest(_questListData.AchievementQuestDataList[i]));
        SaveQuestDictionary();
    }

    private void SetQuestDataSO()
    {
        for (int i = 0; i < _questListData.DailyQuestDataList.Count; i++)
            _questDic[QuestType.Daily][i].SetDataSO(_questListData.DailyQuestDataList[i]);

        for (int i = 0; i < _questListData.RepeatQuestDataList.Count; i++)
            _questDic[QuestType.Repeat][i].SetDataSO(_questListData.RepeatQuestDataList[i]);

        for (int i = 0; i < _questListData.AchievementQuestDataList.Count; i++)
            _questDic[QuestType.Achievement][i].SetDataSO(_questListData.AchievementQuestDataList[i]);
    }
}
