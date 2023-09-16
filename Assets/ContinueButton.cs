using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandleStageSelected);
    }

    private void HandleStageSelected()
    {
        LevelManager.Instance.LoadStageSelectionScene();
    }
}
