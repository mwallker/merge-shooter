using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    [SerializeField]
    private Image foregroundReference;

    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Fill()
    {
        foregroundReference.gameObject.SetActive(true);
    }

    public void Clear()
    {
        foregroundReference.gameObject.SetActive(false);
    }

    public void Ring()
    {
        _audioSource.Play();
    }

    public bool IsEmpty()
    {
        return !foregroundReference.gameObject.activeSelf;
    }
}
