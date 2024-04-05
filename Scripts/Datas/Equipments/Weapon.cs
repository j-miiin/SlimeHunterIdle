using System;
using UnityEngine;

[Serializable]
public class Weapon : Equipment
{
    public WeaponDataSO WeaponDataSO { get; private set; } 

    public Weapon() { }

    public Weapon(WeaponDataSO dataSO) : base(dataSO)
    {
        WeaponDataSO = dataSO;
    }

    public override void SetDataSO(EquipmentDataSO dataSO)
    {
        base.SetDataSO(dataSO);
        WeaponDataSO = dataSO as WeaponDataSO;
    }
}
