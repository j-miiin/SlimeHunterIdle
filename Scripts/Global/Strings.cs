public class Strings 
{
    public class Prefabs
    {
        public const string PREFABS_DATA_PATH = "Prefabs/";
        public const string UI_EVENTSYSTEM = "Prefabs/UI/EventSystem";
        public const string UI_PREFAB_PATH = "Prefabs/UI/";
        public const string UI_POPUP_PREFAB_PATH = "Prefabs/UI/Popup/";

        public const string PLAYER = "Characters/Player";
        public const string MONSTER_FILE_PATH = "Prefabs/Characters/Monsters";
        public const string BOSS_MONSTER_FILE_PATH = "Prefabs/Characters/BossMonsters/BossGreenSlime";

        public const string WAVE_SYSTEM = "GameSystems/WaveSystem";
        public const string GOLD_DUNGEON_WAVE_SYSTEM = "GameSystems/GoldDungeonWaveSystem";

        public const string PROJECTILE_DAGGER = "Weapons/ProjectileDagger";

        public const string UI_SUMMON_RESULT_SLOT_PATH = "UI/Slots/UI_SummonResultSlot";
        public const string UI_REWARD_SLOT_PATH = "UI/Slots/UI_RewardSlot";

        public const string DEFAULT_PROJECTILE = "Weapons/DefaultProjectile";
    }

    public class Sprites
    {
        public const string SPRITES_DATA_PATH = "Sprites/";
        public const string GRADE_D_IMAGE = "GradeIconImages/grade_icon_d";
        public const string GRADE_C_IMAGE = "GradeIconImages/grade_icon_c";
        public const string GRADE_B_IMAGE = "GradeIconImages/grade_icon_b";
        public const string GRADE_A_IMAGE = "GradeIconImages/grade_icon_a";
        public const string GRADE_S_IMAGE = "GradeIconImages/grade_icon_s";
        public const string GRADE_SS_IMAGE = "GradeIconImages/grade_icon_ss";
    }

    public class Tags
    {
        public const string PLAYER = "Player";
        public const string MONSTER = "Monster";
        public const string RANGED_ATTACK = "RangedAttack";
        public const string WALL = "Wall";
    }

    public class Animation
    {
        public const string IDLE = "Idle";
        public const string RUN = "Run";
        public const string ATTACK = "Attack";
        public const string CLOSE_ATTACK = "CloseAttack";
        public const string RANGED_ATTACK = "RangedAttack";
        public const string DIE = "Die";
        public const string APPEAR = "Appear";
    }

    public class Datas
    {
        public const string STAT_DATA_PATH = "ScriptableObjects/StatDatas";
        public const string EQUIPMENT_DATA_PATH = "ScriptableObjects/EquipmentDatas/EquipmentListData";
        public const string SUMMON_DATA_PATH = "ScriptableObjects/SummonDatas";
        public const string SKILL_DATA_PATH = "ScriptableObjects/SkillDatas/SkillListData";
        public const string QUEST_DATA_PATH = "ScriptableObjects/QuestDatas/QuestListData";
        public const string DUNGEON_DATA_PATH = "ScriptableObjects/DungeonDatas/DungeonDicData";
    }

    public class SortingLayer
    {
        public const string UI_CANVAS_LAYER_NAME = "UI_Canvas";
    }

    public class Stat
    {
        public const string ATK_POWER_STR = "공격력";
        public const string HEALTH_STR = "체력";
    }

    public class Equipment
    {
        public const string EQUIP_STR = "장착";
        public const string UNEQUIP_STR = "장착 해제";
        public const string ATTACK_COUNT_STR = "필요 공격 횟수 : ";
    }

    public class Quest
    {
        public const string UPGRADE_STAT_STR = "스탯 업그레이드 _회 달성";
        public const string ENHANCE_EQUIPMENT_STR = "장비 강화 _회 달성";
        public const string SUMMON_EQUIPMENT_STR = "장비 소환 _회 달성";
        public const string SUMMON_SKILL_STR = "스킬 소환 _회 달성";
        public const string KILL_MONSTER_STR = "몬스터 _마리 처치";
    }

    public class Dungeon
    {
        public const string GOLD_DUNGEON_STAGE_STR = "단계";
        public const string GOLD_DUNGEON_MONSTER_COUNT_STR = "마리 처치";
    }

    public class Popup
    {
        public const string NOTIFY_EXIT_POPUP_CONTENT = "한 번 더 누르면 종료합니다";
        public const string NOTIFY_REWARD_POPUP_CONTENT = "보상 획득";
    }
}
