using System;
using System.Collections.Generic;
using static Enums;
using static Strings.Quest;

public class QuestManager : Singleton<QuestManager>
{
    public event Action<QuestTaskType> OnQuestValueUpdate;

    private Dictionary<QuestType, List<Quest>> _questDic;
    private Dictionary<QuestTaskType, List<Quest>> _questTaskTypeDic;

    private QuestDataHandler _dataHandler;
    private int _repeatQuestIdx;

    public void Init()
    {
        _dataHandler = DataManager.Instance.GetDataHandler<QuestDataHandler>();
        _questDic = _dataHandler.LoadQuestDictionary();
        CreateQuestTaskTypeDic();
        _repeatQuestIdx = _dataHandler.LoadRepeatQuestIdx();
        GetCurrentRepeatQuest().OnCompleteRepeatQuest += ChangeCurrentRepeatQuest;
    }

    //public void UpdateQuest(string id, int value)
    //{
    //    if (!_questDic.ContainsKey(id)) return;
    //    _questDic[id].UpdateQuestValue(value);
    //}

    public List<Quest> GetQuestList(QuestType type)
    {
        if (!_questDic.ContainsKey(type)) return null;
        return _questDic[type];
    }

    public RepeatQuest GetCurrentRepeatQuest()
    {
        return _questDic[QuestType.Repeat][_repeatQuestIdx] as RepeatQuest;
    }

    public string GetQuestDescription(QuestTaskType type, int requiredValue)
    {
        string result = "";
        switch (type)
        {
            case QuestTaskType.UpgradeStat:
                result = UPGRADE_STAT_STR; break;
            case QuestTaskType.EnhanceEquipment:
                result = ENHANCE_EQUIPMENT_STR; break;
            case QuestTaskType.SummonEquipment:
                result = SUMMON_EQUIPMENT_STR; break;
            case QuestTaskType.SummonSkill:
                result = SUMMON_SKILL_STR; break;
            case QuestTaskType.KillMonster:
                result = KILL_MONSTER_STR; break;
        }
        result = result.Replace("_", requiredValue.ToString());
        return result;
    }

    public void UpdateQuest(QuestTaskType taskType, int value)
    {
        if (!_questTaskTypeDic.ContainsKey(taskType)) return;

        foreach (Quest quest in _questTaskTypeDic[taskType])
        {
            quest.UpdateQuestValue(value);
        }
        OnQuestValueUpdate?.Invoke(taskType);
        _dataHandler.SaveQuestDictionary();
    }

    private void CreateQuestTaskTypeDic()
    {
        _questTaskTypeDic = new Dictionary<QuestTaskType, List<Quest>>();
        foreach (QuestType type in _questDic.Keys)
        {
            foreach (Quest quest in _questDic[type])
            {
                QuestTaskType key = quest.DataSO.QuestTaskType;
                if (!_questTaskTypeDic.ContainsKey(key))
                    _questTaskTypeDic.Add(key, new List<Quest>());
                _questTaskTypeDic[key].Add(quest);
                quest.OnComplete += () => { _dataHandler.SaveQuestDictionary(); };
            }
        }
    }

    private void ChangeCurrentRepeatQuest()
    {
        _repeatQuestIdx = (_repeatQuestIdx + 1) % _questDic[QuestType.Repeat].Count;
        _dataHandler.SaveRepeatQuestIdx(_repeatQuestIdx);
    }
}
