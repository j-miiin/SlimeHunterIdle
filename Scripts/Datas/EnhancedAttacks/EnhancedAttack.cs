using Keiwando.BigInteger;
using UnityEngine;

public abstract class EnhancedAttack : MonoBehaviour 
{
    public EnhancedAttackDataSO DataSO { get; private set; }
    protected ResourceManager ResourceManager;

    private void Start()
    {
        ResourceManager = ResourceManager.Instance;
    }

    public void SetDataSO(EnhancedAttackDataSO dataSO)
    {
        DataSO = dataSO;
    }

    public abstract void Execute(BigInteger power, Transform playerPos, Transform targetPos);
}
