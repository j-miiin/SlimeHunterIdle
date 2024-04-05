using Keiwando.BigInteger;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] protected float _atkSpeed;
    [SerializeField] protected float _duration;
    protected BigInteger _power;
    [SerializeField] private bool _isDirectlyDestroy;

    protected float CurDuration;
    protected Rigidbody2D Rigidbody;
    protected Vector2 Direction;

    protected ResourceManager ResourceManager;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        CurDuration = 0f;
        Direction = Vector2.zero;
    }

    private void Start()
    {
        ResourceManager = ResourceManager.Instance;
    }

    private void Update()
    {
        CurDuration += Time.deltaTime;

        if (CurDuration > _duration)
        {
            ResourceManager.Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Rigidbody.position += Direction * _atkSpeed * Time.fixedDeltaTime;
    }

    public void SetProjectileDir(Transform target)
    {
        if (target == null) return;
        Direction = (target.position - (Vector3)Rigidbody.position).normalized;
        transform.right = Direction;
    }

    public void SetProjectileDir(Vector2 direction)
    {
        Direction = direction;
        transform.right = Direction;
    }

    public void SetPower(BigInteger power)
    {
        _power = power;
    }

    public float DistanceToTarget(Transform target)
    {
        return Vector3.Distance(Rigidbody.position, target.position);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(Strings.Tags.MONSTER)) return;
        Attack(collision.gameObject);
        if (_isDirectlyDestroy) DestroyProjectile();
    }

    protected virtual void DestroyProjectile()
    {
        ResourceManager.Destroy(gameObject);
    }

    protected void Attack(GameObject obj)
    {
        HealthSystem health = obj.GetComponent<Monster>().HealthSystem;
        if (health.IsDead) return;
        health.ChangeHealth(-_power);
    }

    #region Boune Codes
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (!collision.gameObject.CompareTag(Strings.Tags.WALL)) return;

    //    Debug.Log("벽에 부딫혔다!");
    //    ContactPoint2D contact = collision.GetContact(0);
    //    Bounce(contact.normal);
    //}


    //private void Bounce(Vector2 normal)
    //{
    //    float x = (normal.x < 0) ? -normal.x : normal.x;
    //    float y = (normal.y < 0) ? -normal.y : normal.y;
    //    if (x > y)
    //    {
    //        // X축에 더 큰 충격이 있었음, Y축 방향 유지
    //        _direction = new Vector2(-_direction.x, _direction.y);
    //    }
    //    else
    //    {
    //        // Y축에 더 큰 충격이 있었음, X축 방향 유지
    //        _direction = new Vector2(_direction.x, -_direction.y);
    //    }
    //    transform.right = _direction;
    //}
    #endregion
}
