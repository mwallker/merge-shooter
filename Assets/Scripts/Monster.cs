using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Animator MonsterAnimator;

    [SerializeField]
    private HealthBar HealthBarReference;

    [SerializeField]
    public SpriteRenderer BodyReference;

    [SerializeField]
    public SpriteRenderer EyesReference;

    [SerializeField]
    public SpriteRenderer LeftHandReference;

    [SerializeField]
    public SpriteRenderer RightHandReference;

    [SerializeField]
    public SpriteRenderer MouthReference;

    public float MaxHitPoints { get; private set; } = 1f;

    public float HitPoints { get; private set; } = 1f;

    public float Speed { get; private set; } = 1f;
    public float Reward { get; private set; } = 1f;
    public float Line { get; private set; } = -1;

    void Awake()
    {
        MonsterAnimator.ResetTrigger("Hit");
    }

    public void TakeDamage(float amount)
    {
        if (gameObject.activeSelf)
        {
            HitPoints -= amount;
            MonsterAnimator.SetTrigger("Hit");
            HealthBarReference.SetHitPoints(HitPoints, MaxHitPoints);

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
        MonsterAnimator.ResetTrigger("Hit");
        gameObject.SetActive(false);

        Messaging<MonsterHitCoreEvent>.Trigger?.Invoke(this);
    }

    public Monster SetAttackLine(int value)
    {
        Line = value;
        transform.position = new Vector2(value * 2 - 5, 18);

        return this;
    }

    public Monster SetParameters(MonsterTemplate parameters)
    {
        Speed = parameters.Speed;
        Reward = parameters.Reward;
        MaxHitPoints = HitPoints = parameters.HitPoints;

        BodyReference.sprite = parameters.Body;
        EyesReference.sprite = parameters.Eyes;
        LeftHandReference.sprite = parameters.LeftHand;
        RightHandReference.sprite = parameters.RightHand;
        MouthReference.sprite = parameters.Mouth;

        HealthBarReference.SetHitPoints(HitPoints, MaxHitPoints);

        return this;
    }
}
