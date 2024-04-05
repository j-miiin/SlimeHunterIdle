using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class UI_SkillDetailInfo : UI_Base
{
    [SerializeField] private Text _skillNameText;
    [SerializeField] private Text _skillLevelText;
    [SerializeField] private Text _coolTimeText;
    [SerializeField] private Text _skillTypeText;
    [SerializeField] private Text _descriptionText;

    private Dictionary<SkillType, string> _skillTypeToStrDic = new Dictionary<SkillType, string>()
    {
        { SkillType.Active, "액티브" },
        { SkillType.Buff, "버프" },
        { SkillType.Passive, "패시브" },
    };

    public void RefreshSkillInfo(Skill skill)
    {
        SkillDataSO dataSO = skill.DataSO;
        _skillNameText.text = $"{dataSO.SkillName}";
        _skillLevelText.text = $"Lv.{skill.Level}";
        _coolTimeText.text = (dataSO.CoolTime == 0) ? "없음" : $"{dataSO.CoolTime}";
        _skillTypeText.text = $"{_skillTypeToStrDic[dataSO.SkillType]}";
        _descriptionText.text = $"{dataSO.Description}";
    }
}
