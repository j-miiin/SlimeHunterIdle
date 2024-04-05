using Keiwando.BigInteger;
using System;
using UnityEngine;
using static Enums;

public class Player : PixelCharacter
{
    public event Action OnLevelUpdate;

    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    public PlayerStateMachine StateMachine;
    public PlayerController PlayerController;

    // Player Info
    public string NickName { get; private set; }
    public int Level { get; private set; }
    public int CurExp { get; private set; }
    public int MaxExp { get; private set; }

    public PlayerStat Stat { get; private set; }

    // Equipment
    public Weapon EquippedFist { get; private set; }
    public Weapon EquippedBow { get; private set; }
    public Equipment EquippedClothes { get; private set; }
    public Equipment EquippedShoes { get; private set; }

    // EnhancedAttack
    public EnhancedAttack FistEnhancedAttack { get; private set; }
    public EnhancedAttack BowEnhancedAttack { get; private set; }

    // Skill
    //public Skill EquippedFistSkill { get; private set; }
    //public Skill EquippedBowSkill { get; private set; }
    //public Skill EquippedPassiveSkill { get; private set; }

    public int EnhancedAttackCount { get; private set; } = 7;

    private PlayerDataHandler _playerDataHandler;
    private GameManager _gameManager;

    protected override void Awake()
    {
        base.Awake();
        PlayerController = Controller as PlayerController;
        AnimationData.Initialize();
        StateMachine = new PlayerStateMachine(this);
    }

    public void Init()
    {
        StateMachine.ChangeState(StateMachine.IdleState);
        _playerDataHandler = DataManager.Instance.GetDataHandler<PlayerDataHandler>();
        _gameManager = GameManager.Instance;
        InitPlayerInfo();
        Stat = new PlayerStat();
        Stat.OnMaxHealthUpgrade += (BigInteger maxHealth) => { HealthSystem.ChangeMaxHealth(maxHealth); };
        HealthSystem.Init(Stat.MaxHealth);
        HealthSystem.OnDeath += () => { Invoke("Die", 2f); };
    }

    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    private void Update()
    {
        StateMachine.Update();
    }

    public void EarnExp(int exp)
    {
        CurExp += exp;
        if (CurExp >= MaxExp)
            LevelUp();
        else
            _playerDataHandler.SaveLevelStatus(this, PlayerLevelStatusType.CurExp);
        OnLevelUpdate?.Invoke();
    }

    #region Equip/UnEquip Equipment and Skill
    // Equipment
    public void Equip(Equipment equipment)
    {
        // TODO 스탯 증가 로직 추가
        switch (equipment.DataSO.Type)
        {
            case EquipmentType.Fist:
                UnEquip(EquipmentType.Fist);
                EquippedFist = equipment as Weapon;
                GameObject fistAttackObj = Instantiate(EquippedFist.WeaponDataSO.EnhancedAttack, parent: transform);
                FistEnhancedAttack = fistAttackObj.GetComponent<EnhancedAttack>();
                FistEnhancedAttack.SetDataSO(EquippedFist.WeaponDataSO.EnhancedAttackDataSO);
                EnhancedAttackCount = FistEnhancedAttack.DataSO.RequiredAttackCount;
                break;
            case EquipmentType.Bow:
                UnEquip(EquipmentType.Bow);
                EquippedBow = equipment as Weapon;
                GameObject bowAttackObj = Instantiate(EquippedBow.WeaponDataSO.EnhancedAttack, parent: transform);
                BowEnhancedAttack = bowAttackObj.GetComponent<EnhancedAttack>();
                BowEnhancedAttack.SetDataSO(EquippedBow.WeaponDataSO.EnhancedAttackDataSO);
                EnhancedAttackCount = BowEnhancedAttack.DataSO.RequiredAttackCount;
                break;
            case EquipmentType.Clothes:
                UnEquip(EquipmentType.Clothes);
                EquippedClothes = equipment;
                Stat.ChangeStat(StatType.MaxHealth, EquippedClothes.EquippedEffect);
                break;
            case EquipmentType.Shoes:
                UnEquip(EquipmentType.Shoes);
                EquippedShoes = equipment;
                Stat.ChangeStat(StatType.MaxHealth, EquippedShoes.EquippedEffect);
                break;
        }
        equipment.Equip();
    }

    public void UnEquip(EquipmentType type)
    {
        // TODO 스탯 감소 로직 추가
        switch (type)
        {
            case EquipmentType.Fist:
                if (EquippedFist != null) EquippedFist.UnEquip();
                EquippedFist = null;
                if (FistEnhancedAttack != null) Destroy(FistEnhancedAttack.gameObject);
                FistEnhancedAttack = null;
                break;
            case EquipmentType.Bow:
                if (EquippedBow != null) EquippedBow.UnEquip();
                EquippedBow = null;
                if (BowEnhancedAttack != null) Destroy(BowEnhancedAttack.gameObject);
                BowEnhancedAttack = null;
                break;
            case EquipmentType.Clothes:
                if (EquippedClothes != null)
                {
                    EquippedClothes.UnEquip();
                    Stat.ChangeStat(StatType.MaxHealth, -EquippedClothes.EquippedEffect);
                }
                EquippedClothes = null;
                break;
            case EquipmentType.Shoes:
                if (EquippedShoes != null)
                {
                    EquippedShoes.UnEquip();
                    Stat.ChangeStat(StatType.MaxHealth, -EquippedShoes.EquippedEffect);
                }
                EquippedShoes = null;
                break;
        }
    }

    // Skill
    //public void Equip(Skill skill)
    //{
    //    // TODO 스탯 증가 로직 추가
    //    switch (skill.DataSO.SkillEquipType)
    //    {
    //        case SkillEquipType.Fist:
    //            UnEquip(SkillEquipType.Fist);
    //            EquippedFistSkill = skill;
    //            break;
    //        case SkillEquipType.Bow:
    //            UnEquip(SkillEquipType.Bow);
    //            EquippedBowSkill = skill;
    //            break;
    //        case SkillEquipType.Passive:
    //            UnEquip(SkillEquipType.Passive);
    //            EquippedPassiveSkill = skill;
    //            break;
    //    }
    //    skill.Equip();
    //}

    //public void UnEquip(SkillEquipType type)
    //{
    //    // TODO 스탯 감소 로직 추가
    //    switch (type)
    //    {
    //        case SkillEquipType.Fist:
    //            if (EquippedFistSkill != null) EquippedFistSkill.UnEquip();
    //            EquippedFistSkill = null;
    //            break;
    //        case SkillEquipType.Bow:
    //            if (EquippedBowSkill != null) EquippedBowSkill.UnEquip();
    //            EquippedBowSkill = null;
    //            break;
    //        case SkillEquipType.Passive:
    //            if (EquippedPassiveSkill != null) EquippedPassiveSkill.UnEquip();
    //            EquippedPassiveSkill = null;
    //            break;
    //    }
    //}
    #endregion

    private void InitPlayerInfo()
    {
        PlayerProfileData data = _playerDataHandler.LoadProfile();
        NickName = data.nickName;
        Level = data.level;
        CurExp = data.curExp;
        MaxExp = data.maxExp;
        OnLevelUpdate?.Invoke();
    }

    private void LevelUp()
    {
        Level++;
        MaxExp += Level * Numbers.Player.MAX_EXP_COEFF;
        CurExp = 0;
        _playerDataHandler.SaveProfile(this);
    }

    private void Die()
    {
        HealthSystem.Recover();
        _gameManager.SpawnPlayer();
    }
}
