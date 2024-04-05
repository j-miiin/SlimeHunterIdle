using System;

[Serializable]
public class RepeatQuest : Quest
{
    public event Action OnCompleteRepeatQuest;

    public RepeatQuest() { }

    public RepeatQuest(QuestDataSO dataSO) : base(dataSO)
    {
    }

    public override void CompleteQuest()
    {
        IsCompleted = false;
        CurQuestValue = 0;
        if (RewardManager == null) RewardManager = RewardManager.Instance;
        RewardManager.GiveReward(DataSO.RewardType, DataSO.RewardValue);
        OnComplete?.Invoke();
        OnCompleteRepeatQuest?.Invoke();
    }
}
