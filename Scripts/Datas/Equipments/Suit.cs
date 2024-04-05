using System;

[Serializable]
public class Suit : Equipment
{
    public Suit() { }

    public Suit(EquipmentDataSO dataSO) : base(dataSO)
    {
    }
}
