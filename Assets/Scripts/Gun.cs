using System.Collections;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Animator gunAnimator;

    public float AttackDamage { get; private set; }

    public float AttackSpeed { get; private set; }

    public float AttackDistance { get; private set; }

    public int Tier { get; private set; }

    private readonly float SHOOT_TIME = 0.1f;

    private readonly float AIMING_TIME = 0.25f;

    public bool IsShooting { get; private set; } = false;

    public GunPlatform Platform { get; private set; }

    private Monster target;

    [SerializeField]
    private GameObject BaseSpriteReference;

    [SerializeField]
    private GameObject BarrelSpriteReference;

    void Awake()
    {
        gunAnimator.speed = AttackSpeed;
    }

    void Start()
    {
        StartCoroutine(Attack());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Build(GunPlatform platform)
    {
        Platform = platform;
        transform.SetPositionAndRotation(platform.transform.position, platform.transform.rotation);
    }

    public void Upgrade(GunTier tier)
    {
        AttackDamage = tier.Damage;
        AttackDistance = tier.Distance;
        AttackSpeed = tier.Speed;
        Tier = tier.Id;

        if (BaseSpriteReference.TryGetComponent<SpriteRenderer>(out var baseSprite))
        {
            baseSprite.sprite = tier.Base;
        }

        if (BarrelSpriteReference.TryGetComponent<SpriteRenderer>(out var barrelSprite))
        {
            barrelSprite.sprite = tier.Barrel;
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            target = FindObjectsOfType<Monster>()
               ?.Where(IsMonsterInRange)
               ?.OrderBy(monster => monster.transform.position.y)
               ?.FirstOrDefault();

            if (target != null && !IsShooting)
            {
                IsShooting = true;
                gunAnimator.SetBool("IsShooting", true);

                Messaging<GunShootEvent>.Trigger?.Invoke(this);

                yield return new WaitForSeconds(SHOOT_TIME / AttackSpeed);

                IsShooting = false;
                gunAnimator.SetBool("IsShooting", false);
            }

            yield return new WaitForSeconds(AIMING_TIME / AttackSpeed);
        }
    }

    private bool IsMonsterInRange(Monster monster)
    {
        if (monster.Line != Platform.Line || !monster.gameObject.activeSelf)
        {
            return false;
        }

        float distance = monster.transform.position.y - transform.position.y;

        return distance > 0 && distance <= AttackDistance;
    }
}
