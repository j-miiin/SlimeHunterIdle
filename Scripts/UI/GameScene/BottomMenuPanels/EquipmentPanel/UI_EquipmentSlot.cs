using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EquipmentSlot : UI_Base, IPointerClickHandler
{
    public event Action<Equipment> OnClicked;

    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _gradeImage;
    [SerializeField] private GameObject[] _stars;
    [SerializeField] private Text _enhancementLevelText;
    [SerializeField] private Text _quantityText;
    [SerializeField] private Slider _quantitySlider;
    [SerializeField] private GameObject _lockedImage;
    [SerializeField] private bool _isClickable = true;
    private Toggle _toggle;
    private Outline _outline;
    private Color _outlineColor;

    private ResourceManager _resourceManager;
    private Equipment _equipment;
    private int _requiredCompositeQuantity = Numbers.Equipment.REQUIRED_QUANTITY_FOR_COMPOSITE;
    private bool _isInit;

    public override void Init()
    {
        _isInit = true;
        _resourceManager = ResourceManager.Instance;
    }

    private void Awake()
    {
        if (_isClickable)
        {
            _toggle = GetComponent<Toggle>();
            _outline = GetComponent<Outline>();
            _outlineColor = _outline.effectColor;
            _toggle.onValueChanged.AddListener((bool isOn) =>
            {
                _outlineColor.a = (isOn) ? 1f : 0f;
                _outline.effectColor = _outlineColor;
            });
        }
        _quantitySlider.maxValue = _requiredCompositeQuantity;
    }

    public void RefreshSlot(Equipment equipment)
    {
        if (!_isInit) Init();
        _equipment = equipment;
        EquipmentDataSO dataSO = _equipment.DataSO;
        _iconImage.sprite = dataSO.EquipmentIcon;
        _gradeImage.sprite = _resourceManager.LoadGradeSprite(dataSO.Grade);
        SetActiveStars(dataSO.Level);
        _enhancementLevelText.gameObject.SetActive(!equipment.IsLocked);
        if (!equipment.IsLocked) _enhancementLevelText.text = $"Lv.{equipment.EnhancementLevel}";
        _quantityText.text = $"{equipment.Quantity}/{_requiredCompositeQuantity}";
        _quantitySlider.value = equipment.Quantity;
        _lockedImage.SetActive(equipment.IsLocked);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isClickable && _equipment != null)
        {
            OnClicked?.Invoke(_equipment);
        }
    }

    public void CallOnClicked()
    {
        _toggle.isOn = true;
        OnClicked?.Invoke(_equipment);
    }

    private void SetActiveStars(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _stars[i].SetActive(true);
        }
        
        for (int i = count; i < _stars.Length; i++)
        {
            _stars[i].SetActive(false);
        }
    }
}
