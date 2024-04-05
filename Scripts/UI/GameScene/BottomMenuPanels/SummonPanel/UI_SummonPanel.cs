using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SummonPanel : UI_Base
{
    public event Action OnPanelClosed;

    [SerializeField] private UI_SummonSlot[] _slotList;
    [SerializeField] private Button _closeButton;

    private UIManager _uiManager;
    private SummonSystem _summonSystem;
    private UI_SummonResult _uiSummonResult;

    public override void Init()
    {
        base.Init();
        _uiManager = UIManager.Instance;
        _summonSystem = GameManager.Instance.SummonSystem;
        _summonSystem.OnWeaponSummonResult += OpenSummonResultUI;
        _summonSystem.OnSkillSummonResult += OpenSummonResultUI;
        for (int i = 0; i < _slotList.Length; i++)
        {
            _slotList[i].Init();
        }
        _closeButton.onClick.AddListener(CloseUI);
    }

    public override void CloseUI()
    {
        base.CloseUI();
        OnPanelClosed?.Invoke();
    }

    private void OpenSummonResultUI(List<Equipment> resultList)
    {
        if (_uiSummonResult == null && !_uiManager.TryGetUIComponent(out _uiSummonResult))
        {
            DebugUtil.Assert(_uiSummonResult != null, "_uiSummonResult");
            return;
        }
        _uiSummonResult.OpenUI();
        _uiSummonResult.RefreshSummonResultUI(resultList);
    }

    private void OpenSummonResultUI(List<Skill> skillList)
    {
        if (_uiSummonResult == null && !_uiManager.TryGetUIComponent(out _uiSummonResult))
        {
            DebugUtil.Assert(_uiSummonResult != null, "_uiSummonResult");
            return;
        }
        _uiSummonResult.OpenUI();
        _uiSummonResult.RefreshSummonResultUI(skillList);
    }
}
