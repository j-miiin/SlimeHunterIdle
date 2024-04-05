using System;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class UI_DungeonPanel : UI_Base
{
    public event Action OnPanelClosed;

    [SerializeField] private UI_DungeonSlot[] _slotList;
    [SerializeField] private Button _closeButton;

    private DungeonManager _dungeonManager;
    private UIManager _uiManager;
    private UI_GoldDungeonPanel _uiGoldDungeonPanel;
    private UI_InfiniteDungeonPanel _uiInfiniteDungeonPanel;

    public override void Init()
    {
        base.Init();
        _dungeonManager = DungeonManager.Instance;
        _uiManager = UIManager.Instance;
        for (int i = 0; i < _slotList.Length; i++)
        {
            _slotList[i].OnClickEnterButton += OpenDungeonEnterPanel;
            _slotList[i].RefreshSlot(_dungeonManager.GetDungeon(_slotList[i].Type));
        }
        _closeButton.onClick.AddListener(CloseUI);
    }

    public override void CloseUI()
    {
        base.CloseUI();
        if (_uiGoldDungeonPanel) _uiGoldDungeonPanel.CloseUI();
        if (_uiInfiniteDungeonPanel) _uiInfiniteDungeonPanel.CloseUI();
        OnPanelClosed?.Invoke();
    }

    private void OpenDungeonEnterPanel(DungeonType type)
    {
        switch (type)
        {
            case DungeonType.Gold:
                if (!_uiGoldDungeonPanel) _uiGoldDungeonPanel = _uiManager.GetUIComponent<UI_GoldDungeonPanel>();
                _uiGoldDungeonPanel.OpenUI();
                break;
            case DungeonType.Infinite:
                if (!_uiInfiniteDungeonPanel) _uiInfiniteDungeonPanel = _uiManager.GetUIComponent<UI_InfiniteDungeonPanel>();
                _uiInfiniteDungeonPanel.OpenUI();
                break;
        }
    }
}
