using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Pool<Bullet> _pool;
    private Rigidbody2D _rb;

    public float Damage { get; private set; }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(Damage);
        }

        gameObject.SetActive(false);
        _rb.Sleep();
        _pool.Release(this);
    }

    public void SetPool(Pool<Bullet> pool)
    {
        _pool = pool;
    }

    public void ShootFrom(Gun firedGun)
    {
        Damage = firedGun.AttackDamage;
        transform.SetPositionAndRotation(firedGun.transform.position, Quaternion.identity);
        gameObject.SetActive(true);

        _rb.AddForce(Vector2.up * 500);
    }
}
