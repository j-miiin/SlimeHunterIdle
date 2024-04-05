using UnityEngine;

public class MonsterAttackState : MonsterBaseState
{
    protected float AttackDelay;
    protected float LastAttackTime = 0f;

    public MonsterAttackState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(MonsterStateMachine.Monster.AnimationData.AttackParameterHash);
        AttackDelay = MonsterController.AttackDelay;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Time.time - LastAttackTime >= AttackDelay) // 마지막 공격 시간으로부터 1초가 지났는지 확인
        {
            MonsterController.Attack();
            LastAttackTime = Time.time; // 마지막 공격 시간 업데이트
        }

        if (!MonsterController.ClosestTarget || !MonsterController.IsTargetAlive()) OnIdle();

        if (!MonsterController.IsAttacking && MonsterController.IsTargetAlive()) OnMove();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(MonsterStateMachine.Monster.AnimationData.AttackParameterHash);
    }
}
