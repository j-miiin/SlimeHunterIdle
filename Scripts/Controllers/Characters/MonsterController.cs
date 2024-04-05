using Keiwando.BigInteger;
using UnityEngine;

public class MonsterController : PixelCharacterController
{
    public BigInteger AttackPower { get; protected set; }

    private GameManager _gameManager;

    public void Init(BigInteger attackPower)
    {
        AttackPower = attackPower;
        AttackCollider.SetPower(AttackPower);
    }

    public void TargetPlayer()
    {
        if (_gameManager == null) _gameManager = GameManager.Instance;
        ClosestTarget = _gameManager.Player.gameObject.transform;
    }
}
