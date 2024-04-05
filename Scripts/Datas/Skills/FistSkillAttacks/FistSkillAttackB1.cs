using Keiwando.BigInteger;
using UnityEngine;

public class FistSkillAttackB1 : SkillAttack
{
    public override void Execute(BigInteger power, Transform playerPos, Transform targetPos)
    {
        if (ResourceManager == null) ResourceManager = ResourceManager.Instance;
        ProjectileController projectile = ResourceManager.Instantiate(Projectile, position: playerPos).GetComponent<ProjectileController>();
        projectile.SetPower(power);
        var direction = (targetPos.position - playerPos.position).normalized;
        if (direction.x > 0.1f)
            projectile.SetProjectileDir(Vector2.right);
        else if (direction.x < -0.1f)
            projectile.SetProjectileDir(Vector2.left);
    }
}
