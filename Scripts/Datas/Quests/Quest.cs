using System;

[Serializable]
public abstract class Quest 
{
    public event Action OnQuestAchieve;
    public Action OnComplete;

    public QuestDataSO DataSO { get; protected set; }
    public int CurQuestValue { get; protected set; }
    public bool IsCompleted { get; protected set; }

    protected RewardManager RewardManager;

    public Quest() { }

    public Quest(QuestDataSO dataSO)
    {
        DataSO = dataSO;
        CurQuestValue = 0;
        IsCompleted = false;
    }

    public void SetDataSO(QuestDataSO dataSO)
    {
        DataSO = dataSO;
    }

    public void UpdateQuestValue(int value)
    {
        CurQuestValue += value;
        if (CurQuestValue >= DataSO.RequiredQuestValue && !IsCompleted)
        {
            IsCompleted = true;
            OnQuestAchieve?.Invoke();
        }
    }

    public abstract void CompleteQuest();
}
