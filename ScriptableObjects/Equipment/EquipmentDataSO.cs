using Keiwando.BigInteger;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "EquipmentDataSO", menuName = "Data/Equipment/EquipmentDataSO", order = 0)]
public class EquipmentDataSO : ScriptableObject
{
    [SerializeField] protected string _equipName;
    [SerializeField] protected int _id;
    [SerializeField] protected EquipmentType _type;
    [SerializeField] protected GradeType _grade;
    [SerializeField] protected int _level;
    [SerializeField] protected int _maxLevel;
    [Header("장착 효과")][SerializeField] protected string _defaultEquippedEffect;          // 기본 장착 효과
    [Header("장착 효과 덧셈 계수")][SerializeField] protected string _upgradeEquippedEffectCoeff;     // 장비 강화 시 장착 효과 증가 계수
    [Header("장비 강화석")][SerializeField] protected string _defaultRequiredEnhanceStone;    // 기본 필요 강화석
    [Header("장비 강화석 덧셈 계수")][SerializeField] protected string _upgradeEnhanceStoneCoeff;       // 장비 강화석 증가 계수
    [SerializeField] protected Sprite _equipmentIcon;

    public string EquipName => _equipName;
    public int ID => _id;
    public EquipmentType Type => _type;
    public GradeType Grade => _grade;
    public int Level => _level;
    public int MaxLevel => _maxLevel;
    public string DefaultEquippedEffect => _defaultEquippedEffect;
    public string UpgradeEquippedEffectCoeff => _upgradeEquippedEffectCoeff;
    public string DefaultRequiredEnhanceStone => _defaultRequiredEnhanceStone;
    public string UpgradeEnhanceStoneCoeff => _upgradeEnhanceStoneCoeff;
    public Sprite EquipmentIcon => _equipmentIcon;
}
