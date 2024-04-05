using System;
using UnityEngine;

[Serializable]
public class MonsterAnimationData
{
    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int DieParameterHash { get; private set; }
    public int AppearParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(Strings.Animation.IDLE);
        RunParameterHash = Animator.StringToHash(Strings.Animation.RUN);
        AttackParameterHash = Animator.StringToHash(Strings.Animation.ATTACK);
        DieParameterHash = Animator.StringToHash(Strings.Animation.DIE);
        AppearParameterHash = Animator.StringToHash(Strings.Animation.APPEAR);
    }
}
