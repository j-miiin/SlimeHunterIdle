using Keiwando.BigInteger;
using UnityEngine;

public class CharacterStat
{
    [field: SerializeField] public BigInteger AtkPower { get; protected set; }
    [field: SerializeField] public BigInteger MaxHealth { get; protected set; }

    public CharacterStat() { }

    public CharacterStat(BigInteger atkPower, BigInteger maxHealth)
    {
        AtkPower = atkPower;
        MaxHealth = maxHealth;
    }
}
