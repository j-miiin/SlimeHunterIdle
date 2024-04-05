public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.IdleParameterHash);
        if (PlayerController.IsTargeting) PlayerController.FindClosestMonster();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (PlayerStateMachine.Player.HealthSystem.IsDead) OnDie();
        if (PlayerController.IsTargeting 
            && (!PlayerController.ClosestTarget || !PlayerController.IsTargetAlive())) PlayerController.FindClosestMonster();
        if (PlayerController.ClosestTarget && PlayerController.IsTargetAlive()) OnMove();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.IdleParameterHash);
    }
}
