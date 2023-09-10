using UnityEngine;

public class Wall : MonoBehaviour
{
    Animator wallAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        wallAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var target))
        {
            target.AttackCore();
        }

        wallAnimator.SetTrigger("Hit");
    }
}
