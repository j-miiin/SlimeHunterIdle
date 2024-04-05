using UnityEngine;

public class AttackColliderAnimationEvent : MonoBehaviour
{
    [SerializeField] private AttackCollider _attackCollider;

    public void Attack()
    {
        _attackCollider.Attack();
    }
}
