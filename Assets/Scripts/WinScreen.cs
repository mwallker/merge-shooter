using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField]
    private Button continueButton;

    [SerializeField]
    private Image leftStarReference;

    [SerializeField]
    private Image middleStarReference;

    [SerializeField]
    private Image rightStarReference;

    [SerializeField]
    private TextMeshProUGUI scoreReference;

    void Awake()
    {
        continueButton.onClick.AddListener(HandleStageSelected);
    }

    private void HandleStageSelected()
    {
        LevelManager.Instance.RestartLevelScene();
    }
}
