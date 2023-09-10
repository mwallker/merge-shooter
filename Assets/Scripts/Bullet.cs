using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private ObjectPool<Bullet> pool;

    private Gun gun;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var target) && gun != null)
        {
            target.TakeDamage(gun.AttackDamage);
        }

        pool.Release(this);
    }

    public void SetPool(ObjectPool<Bullet> pool)
    {
        this.pool = pool;
    }

    public void ShootFrom(Gun firedGun)
    {
        gun = firedGun;
        transform.SetPositionAndRotation(gun.transform.position, Quaternion.identity);
    }
}
