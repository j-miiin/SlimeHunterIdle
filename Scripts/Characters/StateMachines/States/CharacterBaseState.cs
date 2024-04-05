public class CharacterBaseState : IState
{
    protected StateMachine StateMachine;

    public CharacterBaseState(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Update()
    {
    }

    // 애니메이션을 키거나 끔
    protected void StartAnimation(int animationHash)
    {
        StateMachine.Character.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        StateMachine.Character.Animator.SetBool(animationHash, false);
    }
}
