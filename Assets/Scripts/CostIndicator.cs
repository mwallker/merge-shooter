using TMPro;
using UnityEngine;

public class CostIndicator : MonoBehaviour
{
    [SerializeField]
    private GameObject CostReference;

    [SerializeField]
    private TextMeshProUGUI CostLabelReference;

    [SerializeField]
    private GunPlatform PlatformReference;

    private Animator _animator;

    private bool _previousState;

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _previousState = true;

        CostLabelReference.text = Level.Instance.GetTierById(Level.DefaultGunTier).Cost.ToString();

        Messaging<LevelBalanceChangeEvent>.Register(HandleBalanceChangeEvent);
    }

    public void OnDisable()
    {
        _animator.ResetTrigger("Outro");
        _previousState = false;

        Messaging<LevelBalanceChangeEvent>.Unregister(HandleBalanceChangeEvent);
    }

    private void HandleBalanceChangeEvent(float balance)
    {
        bool nextStateExist = PlatformReference.HasNextTier();

        if (nextStateExist)
        {
            CostLabelReference.text = PlatformReference.GetNextTier().Cost.ToString();
        }

        if (_previousState != nextStateExist)
        {
            _previousState = nextStateExist;

            if (nextStateExist)
            {
                _animator.ResetTrigger("Outro");
                _animator.SetTrigger("Intro");
            }
            else
            {
                _animator.SetTrigger("Outro");
            }
        }
    }

    private void OnAnimationEnd()
    {
        _previousState = false;
    }
}
