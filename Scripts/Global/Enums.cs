public class Enums
{
    public enum PopupButtonType
    {
        Confirm,
        Cancel
    }

    public enum CharacterType
    {
        Player,
        Monster
    }

    public enum MonsterType
    {
        Boss,
        Normal,
    }

    public enum StatValueType
    {
        Integer,
        Float,
    }

    public enum StatType
    {
        AtkPower,
        MaxHealth,
        CriticalAtkPower,
        CriticalAtkProb,
    }

    public enum PlayerLevelStatusType
    {
        Level,
        CurExp,
        MaxExp,
    }

    public enum CurrencyType
    {
        Gold,
        Gem,
        EnhancementStone,
    }

    public enum EquipmentType
    {
        Fist,
        Bow,
        Clothes,
        Shoes,
    }

    public enum SkillType
    {
        Passive,
        Active,
        Buff,
    }

    public enum SkillEquipType
    {
        Fist,
        Bow,
        Passive,
    }

    public enum GradeType
    {
        D,
        C,
        B,
        A,
        S,
        SS,
        //SSS,
    }

    public enum StageType
    {
        Easy,
        Normal,
        Hard,
        VeryHard,
    }

    public enum SummonType
    {
        Equipment,
        Skill,
    }

    public enum SummonCountType
    {
        Small,
        Large,
    }

    public enum UIAnimationType
    {
        None,
        Scale,
        Fade,
    }

    public enum QuestType
    {
        Daily,
        Repeat,
        Achievement,
    }

    public enum QuestTaskType
    {
        UpgradeStat,
        SummonEquipment,
        SummonSkill,
        EnhanceEquipment,
        KillMonster,
    }

    public enum RewardType
    {
        Gold,
        Gem,
        Exp,
        Equipment,
        EnhancementStone,
    }

    public enum DungeonType
    {
        Gold,
        Equipment,
        Infinite,
    }
}
