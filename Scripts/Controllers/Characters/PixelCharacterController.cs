using Keiwando.BigInteger;
using System;
using System.Collections;
using UnityEngine;

public class PixelCharacterController : MonoBehaviour
{
    public string TargetTag { get; private set; }

    [SerializeField] protected Enums.CharacterType TargetCharacterType;
    [SerializeField] protected AttackCollider AttackCollider;
    [SerializeField] private Transform _nonRotateObject;

    [Header("SerializeField For Check")]
    [SerializeField] public Transform ClosestTarget;
    [SerializeField] public float AttackDelay = 0.5f;
    [SerializeField] private float _moveSpeed = 4f;

    public bool IsFollowing { get; set; }
    public bool IsAttacking { get; set; } = false;

    private Rigidbody2D _rigidbody;
    private float _knockbackDuration;
    private Vector2 _knockback = Vector2.zero;

    private void Awake()
    {
        TargetTag = TargetCharacterType.ToStringEx();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public bool Move()
    {
        if (!ClosestTarget) return false;

        Vector3 position = _rigidbody.position;
        var direction = (ClosestTarget.position - position).normalized;
        var newPosition = position + direction * (_moveSpeed * Time.fixedDeltaTime);

        _rigidbody.MovePosition(newPosition);

        Rotate(direction.x);

        return true;
    }

    public virtual void Attack()
    {
        AttackCollider.Attack();
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        _knockbackDuration = duration;
        _knockback = -(other.position - transform.position).normalized * power;
        StartCoroutine(COApplyKnockback());
    }

    public bool IsTargetAlive()
    {
        if (!ClosestTarget) return false;
        if (ClosestTarget.TryGetComponent(out HealthSystem health) && !health.IsDead) return true;
        return false;
    }

    public float DistanceToTarget()
    {
        if (ClosestTarget == null) return 0f;
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }

    public Vector2 DirectionToTarget()
    {
        if (ClosestTarget == null) return Vector2.zero;
        // Target을 바라보는 방향
        return (ClosestTarget.position - transform.position).normalized;
    }

    public void Rotate()
    {
        if (!ClosestTarget) return;

        var direction = (ClosestTarget.position - transform.position).normalized;
        Rotate(direction.x);
    }

    private void Rotate(float directionX)
    {
        if (!ClosestTarget) return;

        int flag = 1;
        if (directionX > 0.1f) flag = 1;
        else if (directionX < -0.1f) flag = -1;
        
        transform.localScale = new Vector3(flag, 1, 1);
        if (_nonRotateObject)
            _nonRotateObject.localScale = new Vector3(flag, 1, 1);
    }

    private IEnumerator COApplyKnockback()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _knockbackDuration)
        {
            _rigidbody.velocity = _knockback;
            elapsedTime += Time.fixedDeltaTime;
            yield return Time.fixedDeltaTime;
        }
    }
}
