using System;
using System.Collections.Generic;
using static Enums;

[Serializable]
public class Dungeon
{
    public DungeonDataSO DataSO { get; protected set; }
    public DungeonType Type { get; protected set; }
    public int CurHighestStage { get; protected set; }

    public Dungeon() { }

    public Dungeon(DungeonDataSO dataSO)
    {
        DataSO = dataSO;
        Type = DataSO.Type;
        CurHighestStage = 5;
    }

    public void SetDataSO(DungeonDataSO dataSO)
    {
        DataSO = dataSO;
    }

    public void UpdateHighestStage(int stage)
    {
        if (stage > CurHighestStage)
            CurHighestStage = stage;
    }

    public virtual List<Reward> GetStageRewards(int stage)
    {
        if (stage > DataSO.RewardList.Count)
            return DataSO.RewardList[DataSO.RewardList.Count - 1];
        return DataSO.RewardList[stage - 1];
    }

    public virtual List<Reward> GetHighestStageRewards()
    {
        return GetStageRewards(CurHighestStage);
    }
}
