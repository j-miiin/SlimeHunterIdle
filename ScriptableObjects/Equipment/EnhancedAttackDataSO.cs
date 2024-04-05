using UnityEngine;

[CreateAssetMenu(fileName = "EnhancedAttackDataSO", menuName = "Data/SkillAttack/EnhancedAttackDataSO", order = 0)]
public class EnhancedAttackDataSO : ScriptableObject
{
    [SerializeField][TextArea] private string _description;
    [SerializeField] private int _requiredAttackCount;
    [SerializeField] private int _enhancedDamagePercent;
    [SerializeField] private int _duration;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _projectile;

    public string Description => _description;
    public int RequiredAttackCount => _requiredAttackCount;
    public int EnhancedDamagePercent => _enhancedDamagePercent;
    public int Duration => _duration;
    public Sprite Icon => _icon;
    public GameObject Projectile => _projectile;
}
