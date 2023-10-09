using UnityEngine;

public class BuildIndicator : MonoBehaviour
{
    [SerializeField]
    private GunPlatform PlatformReference;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();

        Messaging<GunBuildEvent>.Register(HandleGunBuildEvent);
    }

    void OnDisable()
    {
        Messaging<GunBuildEvent>.Unregister(HandleGunBuildEvent);
    }

    private void HandleGunBuildEvent(GunPlatform platform)
    {
        if (platform == PlatformReference)
        {
            _animator.SetTrigger("Outro");
        }
    }

    private void OnAnimationEnd()
    {
        _animator.ResetTrigger("Outro");
        gameObject.SetActive(false);
    }
}
