public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public PlayerIdleState IdleState { get; }
    public PlayerRunState RunState { get; }
    public PlayerCloseAttackState CloseAttackState { get; }
    public PlayerRangedAttackState RangedAttackState { get; }
    public PlayerDieState DieState { get; }

    public int ComboIndex { get; set; }

    public PlayerStateMachine(Player player) : base(player)
    {
        Player = player;

        IdleState = new PlayerIdleState(this);
        RunState = new PlayerRunState(this);
        CloseAttackState = new PlayerCloseAttackState(this);
        RangedAttackState = new PlayerRangedAttackState(this);
        DieState = new PlayerDieState(this);
    }
}
