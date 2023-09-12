using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Animator monsterAnimator;

    [SerializeField]
    private HealthBar healthBar;


    private float currentHitPoints = 10f;

    private readonly float maxHitPoints = 10f;

    public float Speed { get; private set; } = 2f;
    public float Reward { get; private set; } = 5f;
    public float Line { get; private set; } = -1;

    void Awake()
    {
        monsterAnimator.ResetTrigger("Hit");
        healthBar.SetHitPoints(currentHitPoints, maxHitPoints);
    }

    public void TakeDamage(float amount)
    {
        if (gameObject.activeSelf)
        {
            currentHitPoints -= amount;
            monsterAnimator.SetTrigger("Hit");
            healthBar.SetHitPoints(currentHitPoints, maxHitPoints);

            if (currentHitPoints <= 0)
            {
                currentHitPoints = 0;
                gameObject.SetActive(false);

                Messaging<MonsterDefeatedEvent>.Trigger?.Invoke(Reward);
            }
        }
    }

    public void HitCore()
    {
        monsterAnimator.ResetTrigger("Hit");
        gameObject.SetActive(false);

        Messaging<MonsterAttacksCoreEvent>.Trigger?.Invoke(currentHitPoints);
    }

    public Monster SetAttackLine(int value)
    {
        Line = value;
        transform.position = new Vector2(value * 2 - 5, 18);

        return this;
    }
}
