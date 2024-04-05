using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private PixelCharacterController _controller;
    [SerializeField] private int _enemyCount = 0;

    private void Awake()
    {
        _controller = transform.parent.GetComponent<PixelCharacterController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_controller.TargetTag))
        {
            _enemyCount++;
            _controller.IsAttacking = (_enemyCount == 0) ? false : true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_controller.TargetTag))
        {
            _enemyCount--;
            _controller.IsAttacking = (_enemyCount == 0) ? false : true;
        }
    }
}
