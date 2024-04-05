using UnityEngine;

public class PlayerCloseAttackState : PlayerBaseState
{
    public PlayerCloseAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.CloseAttackParameterHash);
        LastAttackTime = Time.time; 
    }

    public override void Update()
    {
        base.Update();

        if (Time.time - LastAttackTime >= AttackDelay) // 마지막 공격 시간으로부터 1초가 지났는지 확인
        {
            PlayerController.Attack();
            LastAttackTime = Time.time; // 마지막 공격 시간 업데이트
        }

        CheckAttackState();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.CloseAttackParameterHash);
    }
}
