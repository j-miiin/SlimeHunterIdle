public class MonsterIdleState : MonsterBaseState
{
    public MonsterIdleState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(MonsterStateMachine.Monster.AnimationData.IdleParameterHash);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (MonsterController.ClosestTarget && MonsterController.IsTargetAlive()) OnMove();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(MonsterStateMachine.Monster.AnimationData.IdleParameterHash);
    }
}
