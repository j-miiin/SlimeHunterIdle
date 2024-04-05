public class PlayerBaseState : CharacterBaseState
{
    protected PlayerStateMachine PlayerStateMachine;
    protected PlayerController PlayerController;
    protected float AttackDelay;
    protected float LastAttackTime = 0f;

    public PlayerBaseState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        PlayerStateMachine = stateMachine;
        PlayerController = PlayerStateMachine.Player.PlayerController;
        AttackDelay = PlayerController.AttackDelay;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }

    protected virtual void OnIdle()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.IdleState);
    }

    protected virtual void OnMove()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.RunState);
    }

    protected virtual void OnCloseAttack()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.CloseAttackState);
    }

    protected virtual void OnRangedAttack()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.RangedAttackState);
    }

    protected virtual void OnDie()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.DieState);
    }

    protected void CheckAttackState()
    {
        if (PlayerStateMachine.Player.HealthSystem.IsDead) OnDie();
        if (!PlayerController.ClosestTarget || !PlayerController.IsAttacking)
        {
            OnIdle();
        }
    }
}
