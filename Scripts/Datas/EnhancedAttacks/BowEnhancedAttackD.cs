using Keiwando.BigInteger;
using UnityEngine;

public class BowEnhancedAttackD : EnhancedAttack
{
    public override void Execute(BigInteger power, Transform playerPos, Transform targetPos)
    {
        for (int i = 0; i < DataSO.Duration; i++)
        {
            ProjectileController projectile =  ResourceManager.Instantiate(DataSO.Projectile, position: playerPos).GetComponent<ProjectileController>();
            projectile.SetPower(power + power * (DataSO.EnhancedDamagePercent / 100));
            projectile.SetProjectileDir(Quaternion.Euler(0, 0, -45f + i * 15f) * (targetPos.position - playerPos.position).normalized);
        }
    }
}
