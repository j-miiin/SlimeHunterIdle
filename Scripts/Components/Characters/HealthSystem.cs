using Keiwando.BigInteger;
using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action OnHeal;
    public event Action OnDamage;
    public event Action<BigInteger> OnDamageWithValue;
    public event Action OnInvincibilityEnd;
    public Action OnDeath;

    [SerializeField] private UI_Health _uiHealth;

    [field: SerializeField] public bool IsDead { get; private set; }
    public BigInteger MaxHealth { get; private set; }
    public BigInteger CurrentHealth { get; private set; }
    private float _healthChangeDelay = 0.5f;
    private float _timeSinceLastChange = float.MaxValue;

    private void Update()
    {
        if (_timeSinceLastChange < _healthChangeDelay)
        {
            _timeSinceLastChange += Time.deltaTime;
            if (_timeSinceLastChange >= _healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
            }
        }
    }

    public void Init(BigInteger maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
        IsDead = false;
        _timeSinceLastChange = float.MaxValue;
        OnInvincibilityEnd?.Invoke();
        OnHeal = null;
        OnDamage = null;
        OnInvincibilityEnd = null;
        OnDeath = null;
        _uiHealth.gameObject.SetActive(true);
        _uiHealth.Init();
    }

    public void ChangeHealth(BigInteger value)
    {
        if (value == 0 || _timeSinceLastChange < _healthChangeDelay) return;

        _timeSinceLastChange = 0f;
        CurrentHealth += value;
        CurrentHealth = (CurrentHealth > MaxHealth) ? MaxHealth : CurrentHealth;
        CurrentHealth = (CurrentHealth < 0) ? 0 : CurrentHealth;

        if (value > 0) OnHeal?.Invoke();
        else
        {
            OnDamage?.Invoke();
            OnDamageWithValue?.Invoke(-value);
        }

        if (CurrentHealth <= 0 && !IsDead)
        {
            IsDead = true;
            CallOnDeath();
        }
    }

    public void ChangeMaxHealth(BigInteger maxHealth)
    {
        MaxHealth = maxHealth;
        Debug.Log("현재 최대 체력 : " + MaxHealth);
    }

    public void Recover()
    {
        CurrentHealth = MaxHealth;
        _uiHealth.gameObject.SetActive(true);
        OnHeal?.Invoke();
        IsDead = false;
    }

    public void CallOnDeath()
    {
        CurrentHealth = 0;
        IsDead = true;
        _uiHealth.gameObject.SetActive(false);
        OnDeath?.Invoke();
    }
}
