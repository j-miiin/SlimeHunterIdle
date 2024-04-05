using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentListDataSO", menuName = "Data/Equipment/EquipmentListDataSO", order = 0)]
public class EquipmentListDataSO : ScriptableObject
{
    [SerializeField] private List<WeaponDataSO> _fistDataList;
    [SerializeField] private List<WeaponDataSO> _bowDataList;
    [SerializeField] private List<EquipmentDataSO> _clothesDataList;
    [SerializeField] private List<EquipmentDataSO> _shoesDataList;

    public List<WeaponDataSO> FistDataList => _fistDataList;
    public List<WeaponDataSO> BowDataList => _bowDataList;
    public List<EquipmentDataSO> ClothesDataList => _clothesDataList;
    public List<EquipmentDataSO> ShoesDataList => _shoesDataList;
}
