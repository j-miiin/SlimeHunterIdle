using Keiwando.BigInteger;
using System;

[Serializable]
public class Equipment 
{
    public EquipmentDataSO DataSO { get; protected set; }
    public int Quantity { get; protected set; }
    public bool IsLocked { get; protected set; }
    public bool IsEquipped { get; protected set; }
    public int EnhancementLevel { get; protected set; }
    public BigInteger EquippedEffect { get; protected set; }
    public BigInteger RequiredEnhanceStone { get; protected set; }

    #region Caching
    public BigInteger DefaultEquippedEffect { get; protected set; }
    public BigInteger UpgradeEquippedEffectCoeff { get; protected set; }
    public BigInteger DefaultRequiredEnhanceStone { get; protected set; }
    public BigInteger UpgradeEnhanceStoneCoeff { get; protected set; }
    #endregion

    private string _requiredEnhanceStoneStr;
    private string _equippedEffectStr;

    private int _requiredCompositeQuantity = Numbers.Equipment.REQUIRED_QUANTITY_FOR_COMPOSITE;

    public Equipment() { }

    public Equipment(EquipmentDataSO dataSO)
    {
        DataSO = dataSO;
        Quantity = 0;
        IsLocked = true;
        IsEquipped = false;
        EnhancementLevel = 1;
        _equippedEffectStr = DataSO.DefaultEquippedEffect;
        _requiredEnhanceStoneStr = DataSO.DefaultRequiredEnhanceStone;
        EquippedEffect = new BigInteger(_equippedEffectStr);
        RequiredEnhanceStone = new BigInteger(_requiredEnhanceStoneStr);
        SetStringToBigInteger();
    }

    public virtual void SetDataSO(EquipmentDataSO dataSO)
    {
        DataSO = dataSO;
        SetStringToBigInteger();
        EquippedEffect = new BigInteger(_equippedEffectStr);
        RequiredEnhanceStone = new BigInteger(_requiredEnhanceStoneStr);
    }

    public void UnLock()
    {
        IsLocked = false;
    }

    public void Equip()
    {
        IsEquipped = true;
    }

    public void UnEquip()
    {
        IsEquipped = false;
    }

    public void ChangeQuantity(int quantity)
    {
        if (IsLocked) UnLock();
        Quantity += quantity;
    }

    // 장비 강화
    public void Enhance()
    {
        EnhancementLevel++;
        EquippedEffect += UpgradeEquippedEffectCoeff;
        RequiredEnhanceStone += UpgradeEnhanceStoneCoeff;
        _equippedEffectStr = EquippedEffect.ToString();
        _requiredEnhanceStoneStr = RequiredEnhanceStone.ToString();
    }

    public BigInteger GetNextEquippedEffect()
    {
        return EquippedEffect + UpgradeEquippedEffectCoeff;
    }

    public bool IsPossibleToEnhance()
    {
        return EnhancementLevel < DataSO.MaxLevel;
    }

    // 장비 합성
    public void Composite(int compositeCount)
    {
        if (IsLocked) UnLock();
        Quantity += compositeCount;
    }

    public bool IsPossibleToComposite()
    {
        return Quantity >= _requiredCompositeQuantity;
    }

    private void SetStringToBigInteger()
    {
        DefaultEquippedEffect = new BigInteger(DataSO.DefaultEquippedEffect);
        UpgradeEquippedEffectCoeff = new BigInteger(DataSO.UpgradeEquippedEffectCoeff);
        DefaultRequiredEnhanceStone = new BigInteger(DataSO.DefaultRequiredEnhanceStone);
        UpgradeEnhanceStoneCoeff = new BigInteger(DataSO.UpgradeEnhanceStoneCoeff);
    }
}
