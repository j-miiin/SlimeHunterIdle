public class MonsterDieState : MonsterBaseState
{
    public MonsterDieState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(MonsterStateMachine.Monster.AnimationData.DieParameterHash);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!MonsterStateMachine.Monster.HealthSystem.IsDead) OnIdle();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(MonsterStateMachine.Monster.AnimationData.DieParameterHash);
    }
}

