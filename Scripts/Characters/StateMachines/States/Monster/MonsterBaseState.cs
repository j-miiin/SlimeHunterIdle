public class MonsterBaseState : CharacterBaseState
{
    protected MonsterStateMachine MonsterStateMachine;
    protected MonsterController MonsterController;

    public MonsterBaseState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
        MonsterStateMachine = stateMachine;
        MonsterController = MonsterStateMachine.Monster.MonsterController;
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
        MonsterStateMachine.ChangeState(MonsterStateMachine.IdleState);
    }

    protected virtual void OnMove()
    {
        MonsterStateMachine.ChangeState(MonsterStateMachine.RunState);
    }

    protected virtual void OnAttack()
    {
        MonsterStateMachine.ChangeState(MonsterStateMachine.AttackState);
    }

    protected virtual void OnDie()
    {
        MonsterStateMachine.ChangeState(MonsterStateMachine.DieState);
    }
}
