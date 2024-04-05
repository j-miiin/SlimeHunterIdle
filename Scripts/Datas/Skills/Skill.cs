using System;
using UnityEngine;

[Serializable]
public class Skill
{
    public event Action OnCoolTimeUpdate;
    public SkillDataSO DataSO { get; private set; }
    public int ID { get; private set; }
    public int Level { get; private set; }
    public int Quantity { get; private set; }
    public bool IsLocked { get; private set; }
    public int EquippedIdx { get; private set; }
    public int Effect { get; private set; }
    public int RequiredLevelUpQuantity { get; private set; }
    public float ElapsedCoolTime { get; private set; }
    public SkillAttack SkillAttack { get; set; }

    public Skill() { }

    public Skill(SkillDataSO dataSO)
    {
        DataSO = dataSO;
        ID = DataSO.ID;
        Level = 1;
        Quantity = 0;
        IsLocked = true;
        EquippedIdx = -1;
        Effect = DataSO.DefaultEffect;
        RequiredLevelUpQuantity = Level * DataSO.RequiredLevelUpQuantityCoeff;
    }

    public void SetDataSO(SkillDataSO dataSO)
    {
        DataSO = dataSO;
    }

    public void UnLock()
    {
        IsLocked = false;
    }

    public void Equip(int idx)
    {
        EquippedIdx = idx;
    }

    public void UnEquip()
    {
        EquippedIdx = -1;
        ElapsedCoolTime = 0;
    }

    public bool IsPossibleLevelUp()
    {
        return Quantity >= RequiredLevelUpQuantity;
    }

    public void ChangeQuantity(int quantity)
    {
        if (IsLocked) IsLocked = false;
        Quantity += quantity;
    }

    public void LevelUp()
    {
        Level += (Quantity / RequiredLevelUpQuantity);
        Quantity %= RequiredLevelUpQuantity;
        Effect += DataSO.UpgradeEffectCoeff;
        RequiredLevelUpQuantity = Level * DataSO.RequiredLevelUpQuantityCoeff;
    }

    public void SetCoolTimeUpdateEventAction(Action action)
    {
        OnCoolTimeUpdate = null;
        OnCoolTimeUpdate += action;
    }

    public void UpdateElapsedCoolTime(float time, bool isReset = false)
    {
        if (isReset) ElapsedCoolTime = 0f;
        else ElapsedCoolTime += time;
        OnCoolTimeUpdate?.Invoke();
    }

    public void Execute(Player player)
    {
        if (!SkillAttack) return;
        SkillAttack.Execute(player.Stat.AtkPower * Effect, player.transform, player.PlayerController.ClosestTarget);
    }

    public void Clear()
    {
        SkillAttack.Clear();
        GameObject.Destroy(SkillAttack.gameObject);
        SkillAttack = null;
    }
}
