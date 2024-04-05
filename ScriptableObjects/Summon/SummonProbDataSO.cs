using UnityEngine;

[CreateAssetMenu(fileName = "SummonProbDataSO", menuName = "Data/SummonProbDataSO", order = 0)]
public class SummonProbDataSO : ScriptableObject
{
    [SerializeField] private int _gradeD;
    [SerializeField] private int _gradeC;
    [SerializeField] private int _gradeB;
    [SerializeField] private int _gradeA;
    [SerializeField] private int _gradeS;
    [SerializeField] private int _gradeSS;

    public int GradeD => _gradeD;
    public int GradeC => _gradeC;
    public int GradeB => _gradeB;
    public int GradeA => _gradeA;
    public int GradeS => _gradeS;
    public int GradeSS => _gradeSS;
}
