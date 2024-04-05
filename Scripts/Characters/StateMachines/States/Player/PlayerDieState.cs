public class PlayerDieState : PlayerBaseState
{
    public PlayerDieState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.DieParameterHash);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!PlayerStateMachine.Player.HealthSystem.IsDead) OnIdle();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.DieParameterHash);
    }
}
