using System;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class UI_SkillTab : UI_Base
{
    public event Action<SkillEquipType> OnClicked;

    [SerializeField] private SkillEquipType _type;
    [SerializeField] private Color _originalColor;
    [SerializeField] private Color _originalHighlightColor;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _selectedHighlightColor;
    [SerializeField] private Image _highlightImage;
    private Image _toggleImage;
    private Toggle _toggle;

    public override void Init()
    {
        _toggleImage = GetComponent<Image>();
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener((bool isOn) =>
        {
            if (isOn) OnClicked?.Invoke(_type);
            _toggleImage.color = (isOn) ? _selectedColor : _originalColor;
            _highlightImage.color = (isOn) ? _selectedHighlightColor : _originalHighlightColor;
        });
    }

    public void CallOnTabClicked()
    {
        _toggle.isOn = true;
        OnClicked?.Invoke(_type);
        _toggleImage.color = _selectedColor;
        _highlightImage.color = _selectedHighlightColor;
    }
}
