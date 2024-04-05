using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillListDataSO", menuName = "Data/SkillListDataSO", order = 0)]
public class SkillListDataSO : ScriptableObject
{
    [SerializeField] private List<SkillDataSO> _fistSkillDataList;
    [SerializeField] private List<SkillDataSO> _bowSkillDataList;
    [SerializeField] private List<SkillDataSO> _passiveSkillDataList;

    public List<SkillDataSO> FistSkillDataList => _fistSkillDataList;
    public List<SkillDataSO> BowSkillDataList => _bowSkillDataList;
    public List<SkillDataSO> PassiveSkillDataList => _passiveSkillDataList;
}
