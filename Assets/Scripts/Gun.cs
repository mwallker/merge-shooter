using System.Collections;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Animator gunAnimator;

    public readonly float AttackDamage = 1f;

    public readonly float AttackSpeed = 1f;

    public readonly float AttackDistance = 14f;

    public static readonly float Cost = 10;

    private readonly float SHOOT_TIME = 0.1f;

    private readonly float AIMING_TIME = 0.25f;

    public bool IsShooting { get; private set; } = false;

    public GunPlatform Platform { get; private set; }

    private Monster target;

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

    public void BuildAt(GunPlatform platform)
    {
        Platform = platform;
        transform.SetPositionAndRotation(platform.transform.position, platform.transform.rotation);
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
