using System.Collections.Generic;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "SummonDataSO", menuName = "Data/SummonDataSO", order = 0)]
public class SummonDataSO : ScriptableObject
{
    [SerializeField] private SummonType _type;
    [SerializeField] private int _cost1;
    [SerializeField] private int _cost2;
    [SerializeField] private int _count1;
    [SerializeField] private int _count2;
    [SerializeField] private int _defaultMaxExp;
    [SerializeField] private int _maxExpCoeff;
    [SerializeField] private List<SummonProbDataSO> _summonProbList = new List<SummonProbDataSO>(10);

    public SummonType Type => _type;
    public int Cost1 => _cost1;
    public int Cost2 => _cost2;
    public int Count1 => _count1;
    public int Count2 => _count2;
    public int DefaultMaxExp => _defaultMaxExp;
    public int MaxExpCoeff => _maxExpCoeff;
    public List<SummonProbDataSO> SummonLevelProbList => _summonProbList;
}
