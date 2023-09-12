using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private ObjectPool<Bullet> pool;

    public float Damage { get; private set; }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(Damage);
        }

        pool.Release(this);
    }

    public void SetPool(ObjectPool<Bullet> pool)
    {
        this.pool = pool;
    }

    public void ShootFrom(Gun firedGun)
    {
        Damage = firedGun.AttackDamage;
        transform.SetPositionAndRotation(firedGun.transform.position, Quaternion.identity);
    }
}
