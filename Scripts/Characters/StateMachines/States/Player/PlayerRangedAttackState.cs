using UnityEngine;

public class PlayerRangedAttackState : PlayerBaseState
{
    public PlayerRangedAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Time.time - LastAttackTime >= AttackDelay) // 마지막 공격 시간으로부터 1초가 지났는지 확인
        {
            StartAnimation(PlayerStateMachine.Player.AnimationData.RangedAttackParameterHash);
            LastAttackTime = Time.time; // 마지막 공격 시간 업데이트
        }

        CheckAttackState();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.RangedAttackParameterHash);
    }
}
