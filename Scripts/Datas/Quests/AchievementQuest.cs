using System;

[Serializable]
public class AchievementQuest : Quest
{
    public AchievementQuest() { }

    public AchievementQuest(QuestDataSO dataSO) : base(dataSO)
    {
    }

    public override void CompleteQuest()
    {
    }
}
