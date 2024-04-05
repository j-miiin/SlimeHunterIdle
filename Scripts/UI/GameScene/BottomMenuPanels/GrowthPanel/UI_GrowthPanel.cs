using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_GrowthPanel : UI_Base
{
    public event Action OnPanelClosed;

    [SerializeField] private UI_GrowthTraining _uiGrowthTraining;
    [SerializeField] private Toggle _uiTrainingButton;
    [SerializeField] private Toggle _uiAttributeButton;
    [SerializeField] private Toggle _uiAwakeningButton;
    [SerializeField] private Toggle _uiRelicsButton;
    [SerializeField] private Toggle _uiLockedButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _backgroundPanel;

    public void Awake()
    {
        _uiTrainingButton.onValueChanged.AddListener((bool isOn) =>
        {
            if (isOn) _uiGrowthTraining.OpenUI();
            else _uiGrowthTraining.CloseUI();
        });
        _closeButton.onClick.AddListener(CloseUI);
        _backgroundPanel.onClick.AddListener(CloseUI);
    }

    public override void CloseUI()
    {
        base.CloseUI();
        OnPanelClosed?.Invoke();
    }
}
