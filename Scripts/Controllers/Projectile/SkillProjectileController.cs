using UnityEngine;
using static Numbers.Monster;

public class SkillProjectileController : ProjectileController
{
    [SerializeField] private GameObject _hitEffect;

    protected override void DestroyProjectile()
    {
        Collider2D[] hitColliders = OverlapUtil.GetColliderInCircle(transform.position, 1.2f, 1 << MONSTER_LAYER);

        foreach (var hitCollider in hitColliders)
            Attack(hitCollider.gameObject);

        ParticleSystem particle = ResourceManager.Instantiate(_hitEffect, position: transform).GetComponent<ParticleSystem>();
        if (particle) ResourceManager.Destroy(particle.gameObject, particle.main.duration);

        base.DestroyProjectile();
    }
}
