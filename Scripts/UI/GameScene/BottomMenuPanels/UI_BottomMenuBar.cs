using UnityEngine;
using UnityEngine.UI;

public class UI_BottomMenuBar : UI_Base
{
    [SerializeField] private Toggle _storeButton;
    [SerializeField] private Toggle _growthButton;
    [SerializeField] private Toggle _summonButton;
    [SerializeField] private Toggle _equipmentButton;
    [SerializeField] private Toggle _skillButton;
    [SerializeField] private Toggle _dungeonButton;

    private UIManager _uiManager;
    private UI_GrowthPanel _uiGrowthPanel;
    private UI_EquipmentPanel _uiEquipmentPanel;
    private UI_SummonPanel _uiSummonPanel;
    private UI_SkillPanel _uiSkillPanel;
    private UI_DungeonPanel _uiDungeonPanel;

    private bool _isGrowthPanelInit;
    private bool _isEquipmentPanelInit;
    private bool _isSummonPanelInit;
    private bool _isSkillPanelInit;
    private bool _isDungeonPanelInit;

    private Toggle _curOnToggle;

    public override void Init()
    {
        base.Init();
        _uiManager = UIManager.Instance;

        _growthButton.onValueChanged.AddListener((bool isOn) =>
        {
            if (isOn)
            {
                OpenGrowthPanel();
                ChangeCurOnToggle(_growthButton);
            }
            else _uiGrowthPanel.CloseUI();
        });
        _summonButton.onValueChanged.AddListener((bool isOn) =>
        {
            if (isOn) 
            {
                OpenSummonPanel();
                ChangeCurOnToggle(_summonButton);
            }
            else _uiSummonPanel.CloseUI();
        });
        _equipmentButton.onValueChanged.AddListener((bool isOn) =>
        {
            if (isOn)
            {
                OpenEquipmentPanel(); 
                ChangeCurOnToggle(_equipmentButton);
            }

            else _uiEquipmentPanel.CloseUI();
        });
        _skillButton.onValueChanged.AddListener((bool isOn) =>
        {
            if (isOn) 
            { 
                OpenSkillPanel();
                ChangeCurOnToggle(_skillButton);
            }
            else _uiSkillPanel.CloseUI();
        });
        _dungeonButton.onValueChanged.AddListener((bool isOn) =>
        {
            if (isOn)
            {
                OpenDungeonPanel();
                ChangeCurOnToggle(_dungeonButton);
            }
            else _uiDungeonPanel.CloseUI();
        });
    }

    private void ChangeCurOnToggle(Toggle newToggle)
    {
        if (_curOnToggle != null && _curOnToggle != newToggle) _curOnToggle.isOn = false;
        _curOnToggle = newToggle;
    }

    private void OpenGrowthPanel()
    {
        if (_uiGrowthPanel == null && !_uiManager.TryGetUIComponent(out _uiGrowthPanel))
        {
            DebugUtil.Assert(_uiGrowthPanel != null, "_uiGrowthPanel");
            return;
        }
        _uiGrowthPanel.OpenUI();
        if (!_isGrowthPanelInit)
        {
            _isGrowthPanelInit = true;
            _uiGrowthPanel.OnPanelClosed += () => { _growthButton.isOn = false; };
        }
    }

    private void OpenSummonPanel()
    {
        if (_uiSummonPanel == null && !_uiManager.TryGetUIComponent(out _uiSummonPanel))
        {
            DebugUtil.Assert(_uiSummonPanel != null, "_uiSummonPanel");
            return;
        }
        _uiSummonPanel.OpenUI();
        if (!_isSummonPanelInit)
        {
            _isSummonPanelInit = true;
            _uiSummonPanel.OnPanelClosed += () => { _summonButton.isOn = false; };
        }
    }

    private void OpenEquipmentPanel()
    {
        if (_uiEquipmentPanel == null && !_uiManager.TryGetUIComponent(out _uiEquipmentPanel))
        {
            DebugUtil.Assert(_uiEquipmentPanel != null, "_uiEquipmentPanel");
            return;
        }
        _uiEquipmentPanel.OpenUI();
        if (!_isEquipmentPanelInit)
        {
            _isEquipmentPanelInit = true;
            _uiEquipmentPanel.OnPanelClosed += () => { _equipmentButton.isOn = false; };
        }
    }

    private void OpenSkillPanel()
    {
        if (_uiSkillPanel == null && !_uiManager.TryGetUIComponent(out _uiSkillPanel))
        {
            DebugUtil.Assert(_uiSkillPanel != null, "_uiSkillPanel");
            return;
        }
        _uiSkillPanel.OpenUI();
        if (!_isSkillPanelInit)
        {
            _isSkillPanelInit = true;
            _uiSkillPanel.OnPanelClosed += () => { _skillButton.isOn = false; };
        }
    }

    private void OpenDungeonPanel()
    {
        if (_uiDungeonPanel == null && !_uiManager.TryGetUIComponent(out _uiDungeonPanel))
        {
            DebugUtil.Assert(_uiDungeonPanel != null, "_uiDungeonPanel");
            return;
        }
        _uiDungeonPanel.OpenUI();
        if (!_isDungeonPanelInit)
        {
            _isDungeonPanelInit = true;
            _uiDungeonPanel.OnPanelClosed += () => { _dungeonButton.isOn = false; };
        }
    }
}
