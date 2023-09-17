using System.Collections;
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

    void OnEnable()
    {
        StartCoroutine(ScoreCounter());

        scoreReference.text = Level.Instance.Score.ToString();
    }

    private void HandleStageSelected()
    {
        LevelManager.Instance.RestartLevelScene();
    }

    private IEnumerator ScoreCounter()
    {
        int currentScore = 0;

        while (currentScore <= Level.Instance.Score)
        {
            scoreReference.text = currentScore.ToString();

            if (currentScore == Level.Instance.ScoreLimits[0])
            {
                leftStarReference.gameObject.SetActive(true);
            }
            else if (currentScore == Level.Instance.ScoreLimits[1])
            {
                middleStarReference.gameObject.SetActive(true);
            }
            else if (currentScore == Level.Instance.ScoreLimits[2])
            {
                rightStarReference.gameObject.SetActive(true);
            }

            yield return new WaitForEndOfFrame();

            currentScore++;
        }
    }
}
