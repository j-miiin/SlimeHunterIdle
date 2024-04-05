using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "SkillDataSO", menuName = "Data/SkillDataSO", order = 0)]
public class SkillDataSO : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _skillName;
    [TextArea][SerializeField] private string _description;
    [SerializeField] private SkillEquipType _skillEquipType;
    [SerializeField] private SkillType _skillType;
    [SerializeField] private GradeType _grade;
    [Header("스킬 쿨타임")][SerializeField] private float _coolTime;
    [Header("스킬 기본 효과")][SerializeField] private int _defaultEffect;          
    [Header("스킬 레벨업 시 효과 증가 계수")][SerializeField] private int _upgradeEffectCoeff;
    [Header("스킬 레벨업에 필요한 스킬 개수 증가 계수")][SerializeField] private int _requiredLevelUpQuantityCoeff;
    [SerializeField] private Sprite _skillIcon;
    [SerializeField] private GameObject _skillAttack;

    public int ID => _id;
    public string SkillName => _skillName;
    public string Description => _description;
    public SkillEquipType SkillEquipType => _skillEquipType;
    public SkillType SkillType => _skillType;
    public GradeType Grade => _grade;
    public float CoolTime => _coolTime;
    public int DefaultEffect => _defaultEffect;
    public int UpgradeEffectCoeff => _upgradeEffectCoeff;
    public int RequiredLevelUpQuantityCoeff => _requiredLevelUpQuantityCoeff;
    public Sprite SkillIcon => _skillIcon;
    public GameObject SkillAttack => _skillAttack;
}
