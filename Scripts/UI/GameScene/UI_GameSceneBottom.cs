using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class UI_GameSceneBottom : UI_Base
{
    [SerializeField] private UI_SkillCoolTimeSlot[] _slotList;
    [SerializeField] private Button _weaponSwitchButton;

    private SkillManager _skillManager;
    private Player _player;
    private Skill[] _curEquippedSkillList;

    public override void Init()
    {
        base.Init();
        _skillManager = SkillManager.Instance;
        _player = GameManager.Instance.Player;
        _skillManager.OnEquippedSkillUpdate += RefreshSlotList;
        _weaponSwitchButton.onClick.AddListener(SwitchWeapon);
        RefreshSlotList((_player.PlayerController.IsCloseAttack) ? SkillEquipType.Fist : SkillEquipType.Bow);
    }

    public override void Clear()
    {
        base.Clear();
        _skillManager.OnEquippedSkillUpdate -= RefreshSlotList;
    }

    private void RefreshSlotList(SkillEquipType type)
    {
        _curEquippedSkillList = _skillManager.GetEquippedSkillList(type);
        for (int i = 0; i < _slotList.Length; i++)
        {
            _slotList[i].RefreshSlot(_curEquippedSkillList[i]);
        }
    }

    private void SwitchWeapon()
    {
        _player.PlayerController.ChangeAttackType();
    }
}
