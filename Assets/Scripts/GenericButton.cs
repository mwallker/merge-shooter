using UnityEngine;
using UnityEngine.UI;

public class GenericButton : MonoBehaviour
{
    private AudioSource _audioSource;
    private Button _button;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandleStageSelected);
    }

    private void HandleStageSelected()
    {
        _audioSource.Play();
    }
}
