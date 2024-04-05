using System;

[Serializable]
public class Stage 
{
    public Enums.StageType Type { get; private set; } 
    public int Level { get; private set; }
    public int TypeLevel { get; private set; }
    public int MainLevel { get; private set; }
    public int SubLevel { get; private set; }
    public int MonsterLevel { get; private set; }

    public Stage() { }

    public Stage(int level)
    {
        Level = level;
        CaculateLevelInfo();
    }

    public void LevelUp()
    {
        Level++;
        CaculateLevelInfo();
    }

    private void CaculateLevelInfo()
    {
        TypeLevel = Level / 100;
        MainLevel = (Level - TypeLevel * 100) / 20 + 1;
        SubLevel = (Level - TypeLevel * 100) % 20;

        Type = (Enums.StageType)(TypeLevel);
        MonsterLevel = MainLevel;
    }
}
