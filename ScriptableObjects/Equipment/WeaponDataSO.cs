using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataSO", menuName = "Data/Equipment/WeaponDataSO", order = 0)]
public class WeaponDataSO : EquipmentDataSO
{
    [SerializeField] private EnhancedAttackDataSO _enhancedAttackDataSO;
    [SerializeField] private GameObject _enhancedAttack;

    public EnhancedAttackDataSO EnhancedAttackDataSO => _enhancedAttackDataSO;
    public GameObject EnhancedAttack => _enhancedAttack;
}
