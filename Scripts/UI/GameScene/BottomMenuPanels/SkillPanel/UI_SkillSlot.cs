using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillSlot : UI_Base, IPointerClickHandler
{
    public event Action<Skill> OnClicked;

    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _gradeImage;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _quantityText;
    [SerializeField] private Slider _quantitySlider;
    [SerializeField] private GameObject _lockedImage;
    [SerializeField] private bool _isClickable = true;
    private Toggle _toggle;
    private Outline _outline;
    private Color _outlineColor;

    private ResourceManager _resourceManager;
    private Skill _skill;
    private int _requiredLevelUpQuantity;
    private bool _isInit;

    private void Awake()
    {
        if (_isClickable)
        {
            _toggle = GetComponent<Toggle>();
            _outline = GetComponent<Outline>();
            if (_outline != null) _outlineColor = _outline.effectColor;
            _toggle.onValueChanged.AddListener((bool isOn) =>
            {
                if (_outline != null)
                {
                    _outlineColor.a = (isOn) ? 1f : 0f;
                    _outline.effectColor = _outlineColor;
                }
            });
        }
    }

    public void RefreshSlot(Skill skill)
    {
        if (skill == null)
        {
            _lockedImage.SetActive(true);
            return;
        }
        if (!_isInit) Init();
        _skill = skill;
        SkillDataSO dataSO = _skill.DataSO;
        _iconImage.sprite = dataSO.SkillIcon;
        _gradeImage.sprite = _resourceManager.LoadGradeSprite(dataSO.Grade);
        _requiredLevelUpQuantity = skill.RequiredLevelUpQuantity;
        _quantitySlider.maxValue = _requiredLevelUpQuantity;
        _levelText.gameObject.SetActive(!skill.IsLocked);
        if (!skill.IsLocked) _levelText.text = $"Lv.{skill.Level}";
        _quantityText.text = $"{skill.Quantity}/{_requiredLevelUpQuantity}";
        _quantitySlider.value = skill.Quantity;
        _lockedImage.SetActive(skill.IsLocked);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isClickable && _skill != null)
        {
            OnClicked?.Invoke(_skill);
        }
    }

    public void CallOnClicked()
    {
        _toggle.isOn = true;
        OnClicked?.Invoke(_skill);
    }

    private void Init()
    {
        _isInit = true;
        _resourceManager = ResourceManager.Instance;
    }
}
