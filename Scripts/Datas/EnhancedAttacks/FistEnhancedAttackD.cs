using Keiwando.BigInteger;
using System.Collections;
using UnityEngine;

public class FistEnhancedAttackD : EnhancedAttack
{
    public override void Execute(BigInteger power, Transform playerPos, Transform targetPos)
    {
        StartCoroutine(Attack(power, playerPos));
    }

    private IEnumerator Attack(BigInteger power, Transform playerPos)
    {
        WaitForSeconds interval = new WaitForSeconds(1.2f);
        int count = 0;

        while (count < DataSO.Duration)
        {
            GameObject obj = ResourceManager.Instantiate(DataSO.Projectile, parent: transform, position: playerPos);
            obj.transform.localPosition = new Vector3(0.26f, 0.15f, 0);
            obj.transform.localScale = Vector3.one;
            AttackCollider attack = obj.GetComponentInChildren<AttackCollider>();
            attack.SetPower(power + power * (DataSO.EnhancedDamagePercent / 100));
            count++;
            yield return interval;
            ResourceManager.Destroy(obj);
        }
    }
}
