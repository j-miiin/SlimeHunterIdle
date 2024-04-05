public class MonsterRunState : MonsterBaseState
{
    public MonsterRunState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(MonsterStateMachine.Monster.AnimationData.RunParameterHash);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!MonsterController.Move()) OnIdle();
    }

    public override void Update()
    {
        base.Update();

        if (MonsterController.IsTargetAlive())
        {
            if (MonsterController.IsAttacking) OnAttack();
        } else
        {
            OnIdle();
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(MonsterStateMachine.Monster.AnimationData.RunParameterHash);
    }
}
