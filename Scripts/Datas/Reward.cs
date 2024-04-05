using Keiwando.BigInteger;
using System;
using UnityEngine;
using static Enums;

[Serializable]
public class Reward 
{
    [field: SerializeField] public RewardType Type { get; private set; }
    [field: SerializeField] public string ValueStr { get; private set; }
    [field: SerializeField] public float Prob { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    public BigInteger Value 
    { 
        get
        {
            if (_value == null) _value = new BigInteger(ValueStr);
            return _value;
        }
    }

    private BigInteger _value;
}
