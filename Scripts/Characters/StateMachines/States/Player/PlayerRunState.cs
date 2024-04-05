public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!PlayerController.Move()) OnIdle();
    }

    public override void Update()
    {
        base.Update();
        if (PlayerStateMachine.Player.HealthSystem.IsDead) OnDie();
        if (PlayerController.IsAttacking)
        {
            if (PlayerController.IsTargetAlive())
            {
                if (PlayerController.IsCloseAttack) OnCloseAttack();
                else OnRangedAttack();
            } else
            {
                PlayerController.FindClosestMonster();
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        PlayerController.Rotate();
        StopAnimation(PlayerStateMachine.Player.AnimationData.RunParameterHash);
    }
}
