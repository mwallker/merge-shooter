using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Animator monsterAnimator;

    [SerializeField]
    private HealthBar healthBar;

    public readonly float MaxHitPoints = 10f;

    public float HitPoints { get; private set; } = 10f;

    public float Speed { get; private set; } = 3f;
    public float Reward { get; private set; } = 5f;
    public float Line { get; private set; } = -1;

    void Awake()
    {
        monsterAnimator.ResetTrigger("Hit");
        healthBar.SetHitPoints(HitPoints, MaxHitPoints);
    }

    public void TakeDamage(float amount)
    {
        if (gameObject.activeSelf)
        {
            HitPoints -= amount;
            monsterAnimator.SetTrigger("Hit");
            healthBar.SetHitPoints(HitPoints, MaxHitPoints);

            if (HitPoints <= 0)
            {
                HitPoints = 0;
                gameObject.SetActive(false);

                Messaging<MonsterDefeatedEvent>.Trigger?.Invoke(this);
            }
        }
    }

    public void HitCore()
    {
        monsterAnimator.ResetTrigger("Hit");
        gameObject.SetActive(false);

        Messaging<MonsterHitCoreEvent>.Trigger?.Invoke(this);
    }

    public Monster SetAttackLine(int value)
    {
        Line = value;
        transform.position = new Vector2(value * 2 - 5, 18);

        return this;
    }
}
