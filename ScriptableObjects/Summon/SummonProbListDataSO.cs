using System.Collections.Generic;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "SummonProbListDataSO", menuName = "Data/SummonProbListDataSO", order = 0)]
public class SummonProbListDataSO : ScriptableObject
{
    [SerializeField] private SummonType _type;
    [SerializeField] private List<SummonProbDataSO> _summonProbList = new List<SummonProbDataSO>(10);

    public SummonType Type => _type;
    public List<SummonProbDataSO> SummonLevelProbList => _summonProbList;
}
