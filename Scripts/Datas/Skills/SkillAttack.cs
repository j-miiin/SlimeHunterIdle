using Keiwando.BigInteger;
using UnityEngine;

public abstract class SkillAttack : MonoBehaviour
{
    [SerializeField] protected GameObject Projectile;
    protected ResourceManager ResourceManager;

    public abstract void Execute(BigInteger power, Transform playerPos, Transform targetPos);

    public void Clear()
    {
        PoolManager.Instance.Clear(Projectile.name);
    }
}
