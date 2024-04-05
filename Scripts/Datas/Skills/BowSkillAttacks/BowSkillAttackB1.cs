using Keiwando.BigInteger;
using UnityEngine;

public class BowSkillAttackB1 : SkillAttack
{
    public override void Execute(BigInteger power, Transform playerPos, Transform targetPos)
    {
        if (ResourceManager == null) ResourceManager = ResourceManager.Instance;
        SkillProjectileController projectile = ResourceManager.Instantiate(Projectile, position: playerPos).GetComponent<SkillProjectileController>();
        projectile.SetPower(power);
        projectile.SetProjectileDir(targetPos);
    }
}
