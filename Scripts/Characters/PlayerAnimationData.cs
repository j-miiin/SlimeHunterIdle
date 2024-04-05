using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData 
{
    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int CloseAttackParameterHash { get; private set; }
    public int RangedAttackParameterHash { get; private set; }
    public int DieParameterHash { get; private set; }

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(Strings.Animation.IDLE);
        RunParameterHash = Animator.StringToHash(Strings.Animation.RUN);
        CloseAttackParameterHash = Animator.StringToHash(Strings.Animation.CLOSE_ATTACK);
        RangedAttackParameterHash = Animator.StringToHash(Strings.Animation.RANGED_ATTACK);
        DieParameterHash = Animator.StringToHash(Strings.Animation.DIE);
    }
}
