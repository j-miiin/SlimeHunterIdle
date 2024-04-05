public class MonsterStateMachine : StateMachine
{
    public Monster Monster { get; }

    public MonsterIdleState IdleState { get; }
    public MonsterRunState RunState { get; }
    public MonsterAttackState AttackState { get; }
    public MonsterDieState DieState { get; }
    public MonsterAppearState AppearState { get; }

    public MonsterStateMachine(Monster monster) : base(monster)
    {
        Monster = monster;

        IdleState = new MonsterIdleState(this);
        RunState = new MonsterRunState(this);
        AttackState = new MonsterAttackState(this);
        DieState = new MonsterDieState(this);
        AppearState = new MonsterAppearState(this);
    }
}
