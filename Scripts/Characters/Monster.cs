using Keiwando.BigInteger;
using System;
using UnityEngine;

public class Monster : PixelCharacter
{
    public event Action<Monster> OnMonsterDeath;

    public MonsterController MonsterController { get; private set; }

    [field: SerializeField] public MonsterAnimationData AnimationData { get; private set; }
    [SerializeField] private bool _isBoss;
    [SerializeField] private GameObject _damageParticle;

    private MonsterStateMachine _stateMachine;
    private CharacterStat _stat;
    private DeathEffect _deathEffect;
    private UI_DamageText _uiDamageText;

    protected override void Awake()
    {
        base.Awake();
        MonsterController = Controller as MonsterController;
        AnimationData.Initialize();
        _stateMachine = new MonsterStateMachine(this);
        _deathEffect = GetComponent<DeathEffect>(); 
    }

    public void Init(CharacterStat stat)
    {
        _stat = stat;

        MonsterController.Init(_stat.AtkPower);

        HealthSystem.Init(_stat.MaxHealth);
        HealthSystem.OnDamage += () => { SetDamagedState(true); };
        HealthSystem.OnDamageWithValue += ShowDamageText;
        HealthSystem.OnInvincibilityEnd += () => { SetDamagedState(false); };
        HealthSystem.OnDeath += () => { _stateMachine.ChangeState(_stateMachine.DieState); };

        if (_isBoss) _stateMachine.ChangeState(_stateMachine.AppearState);
        else
        {
            MonsterController.TargetPlayer();
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

        _deathEffect.Init();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void SetDamagedState(bool isDamaged)
    {
        _damageParticle.SetActive(isDamaged);
    }

    private void ShowDamageText(BigInteger damage)
    {
        if (!_uiDamageText)
        {
            if (!UIManager.Instance.TryGetUIComponent<UI_DamageText>(out _uiDamageText))
                Debug.LogError("Null Exception : UI_DamageText");
        }
        _uiDamageText.ShowDamage(damage, transform.position);
    }
}
