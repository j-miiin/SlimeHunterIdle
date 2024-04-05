using System;

[Serializable]
public class DailyQuest : Quest
{
    public DailyQuest() { }

    public DailyQuest(QuestDataSO dataSO) : base(dataSO)
    {
    }

    public override void CompleteQuest()
    {
    }
}
