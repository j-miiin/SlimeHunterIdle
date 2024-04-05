using UnityEngine;
using UnityEngine.UI;

public class UI_Health : UI_Base
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private PixelCharacterController _characterController;

    public override void Init()
    {
        _healthSystem.OnHeal += UpdateHealth;
        _healthSystem.OnDamage += UpdateHealth;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        string valueStr = ((_healthSystem.CurrentHealth * 100) / _healthSystem.MaxHealth).ToString();
        _healthSlider.value = int.Parse(valueStr);
    }
}
