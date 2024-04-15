using Keiwando.BigInteger;
using System;
using System.Collections;
using UnityEngine;
using static Numbers.Monster;
using static Enums;
using Random = UnityEngine.Random;

public class PlayerController : PixelCharacterController
{
    public event Action<bool> OnAttackTypeChanged;
    public bool IsCloseAttack { get; set; } = true;
    public bool IsTargeting { get; private set; } = true;

    [SerializeField] private Player _player;
    [SerializeField] private GameObject _closeAttackRange;
    [SerializeField] private GameObject _rangedAttackRange;
    private int _closeAtkCount = 0;
    private int _rangedAtkCount = 0;
    private ResourceManager _resourceManager;

    private void Start()
    {
        _resourceManager = ResourceManager.Instance;
    }

    public override void Attack()
    {
        if (!IsTargetAlive()) FindClosestMonster();

        if (!ClosestTarget) return;

        EnhancedAttack curEnhancedAttack = _player.FistEnhancedAttack;
        if (curEnhancedAttack == null) AttackCollider.Attack(GetAttackPower(EquipmentType.Fist));
        else
        {
            if (_closeAtkCount < curEnhancedAttack.DataSO.RequiredAttackCount)
            {
                _closeAtkCount++;
                AttackCollider.Attack(GetAttackPower(EquipmentType.Fist));
            }
            else
            {
                _closeAtkCount = 0;
                curEnhancedAttack.Execute(GetAttackPower(EquipmentType.Fist), transform, ClosestTarget);
            }
        }
    }

    public void ChangeAttackType()
    {
        IsCloseAttack = !IsCloseAttack;
        _closeAttackRange.SetActive(IsCloseAttack);
        AttackCollider.gameObject.SetActive(IsCloseAttack);
        _rangedAttackRange.SetActive(!IsCloseAttack);
        OnAttackTypeChanged?.Invoke(IsCloseAttack);
    }

    public bool IsClosestMonsterDead()
    {
        if (ClosestTarget != null && ClosestTarget.gameObject.TryGetComponent(out Monster monster))
        {
            return monster.HealthSystem.IsDead;
        }
        return true;
    }

    public void CallOnRangedAttackEvent()
    {
        EnhancedAttack curEnhancedAttack = _player.BowEnhancedAttack;
        if (curEnhancedAttack == null) StartCoroutine(CORangedAttack(GetAttackPower(EquipmentType.Bow)));
        else
        {
            if (_rangedAtkCount < curEnhancedAttack.DataSO.RequiredAttackCount)
            {
                _rangedAtkCount++;
                StartCoroutine(CORangedAttack(GetAttackPower(EquipmentType.Bow)));
            }
            else
            {
                _rangedAtkCount = 0;
                curEnhancedAttack.Execute(GetAttackPower(EquipmentType.Bow), transform, ClosestTarget);
            }
        }
    }

    public IEnumerator CORangedAttack(BigInteger atkPower)
    {
        ProjectileController projectile = _resourceManager.Instantiate(Strings.Prefabs.DEFAULT_PROJECTILE).GetComponent<ProjectileController>();
        projectile.transform.position = transform.position;
        projectile.SetPower(atkPower);
        bool hasReachedTarget = false;

        while (!hasReachedTarget || (ClosestTarget && projectile.DistanceToTarget(ClosestTarget) <= 0.1f))
        {
            if (!IsTargetAlive()) FindClosestMonster();

            if (!ClosestTarget) break;

            projectile.SetProjectileDir(ClosestTarget);

            if (!hasReachedTarget && projectile.DistanceToTarget(ClosestTarget) <= 0.1f)
            {
                hasReachedTarget = true;
            }

            yield return null;
        }
    }

    public void FindClosestMonster()
    {
        if (!IsTargeting) return;

        Collider2D[] hitColliders = OverlapUtil.GetColliderInCircle(transform.position, 10, 1 << MONSTER_LAYER);

        float closestDistance = Mathf.Infinity;
        Transform closestMonster = null;
        foreach (var hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestMonster = hitCollider.transform;
            }
        }

        ClosestTarget = closestMonster;
    }

    public void ChangeTargetingState(bool isTargeting)
    {
        IsTargeting = isTargeting;
        if (!IsTargeting) ClosestTarget = null;
    }

    private BigInteger GetAttackPower(EquipmentType type)
    {
        BigInteger result = _player.Stat.AtkPower;
        switch (type)
        {
            case EquipmentType.Fist:
                BigInteger fistAtkPower = (_player.EquippedFist != null) ? _player.EquippedFist.EquippedEffect : 0;
                result += fistAtkPower;
                break;
            case EquipmentType.Bow:
                BigInteger bowAtkPower = (_player.EquippedBow != null) ? _player.EquippedBow.EquippedEffect : 0;
                result += bowAtkPower;
                break;
        }
        if (IsOccur(_player.Stat.CriticalAtkProb, 10000))
        {
            int criticalPower = (int)(_player.Stat.CriticalAtkPower * 100);
            result *= criticalPower;
            result /= 100;
        }
        return result;
    }

    private bool IsOccur(float prob, int digit = 100)
    {
        int isOccur = Random.Range(0, digit);
        if (isOccur < prob * (digit * 0.01)) return true;
        else return false;
    }
}
