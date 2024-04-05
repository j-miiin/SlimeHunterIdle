public class StateMachine
{
    public PixelCharacter Character;

    public bool IsAttacking { get; set; }
    public float MovementSpeed { get; set; }

    public IState CurState { get; private set; }

    public StateMachine(PixelCharacter character)
    {
        Character = character;
    }

    public void ChangeState(IState newState)
    {
        CurState?.Exit();
        CurState = newState;
        CurState?.Enter();
    }

    public void Update()
    {
        CurState?.Update();
    }

    public void FixedUpdate()
    {
        CurState?.FixedUpdate();
    }
}
