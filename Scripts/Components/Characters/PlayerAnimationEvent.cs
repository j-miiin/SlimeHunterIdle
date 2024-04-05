using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerController _controller;

    public void RangedAttack()
    {
        _controller.CallOnRangedAttackEvent();
    }

    public void StopRangedAttackAnimationEvent()
    {
        _player.Animator.SetBool(_player.AnimationData.RangedAttackParameterHash, false);
    }
}
