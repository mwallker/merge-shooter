using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private Bullet bulletPrefab;

    private ObjectPool<Bullet> bulletsPool;

    void Start()
    {
        bulletsPool = new ObjectPool<Bullet>(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet, true, 100, 200);

        Messaging<GunShootEvent>.Register((gun) =>
        {
            bulletsPool.Get(out var bullet);

            if (bullet != null)
            {
                bullet.ShootFrom(gun);
            }
        });
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);

        bullet.SetPool(bulletsPool);

        return bullet;
    }

    private void GetBullet(Bullet bullet)
    {
        bullet.transform.SetPositionAndRotation(Vector2.zero, Quaternion.identity);
        bullet.gameObject.SetActive(true);

        if (bullet.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.AddForce(Vector2.up * 500);
        }
    }

    private void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);

        if (bullet.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.Sleep();
        }
    }

    private void DestroyBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}
