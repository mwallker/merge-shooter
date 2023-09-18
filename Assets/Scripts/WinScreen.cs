using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField]
    private Button continueButton;

    [SerializeField]
    private Star leftStarReference;

    [SerializeField]
    private Star middleStarReference;

    [SerializeField]
    private Star rightStarReference;

    [SerializeField]
    private TextMeshProUGUI scoreReference;

    void Awake()
    {
        continueButton.onClick.AddListener(HandleStageSelected);
    }

    void OnEnable()
    {
        scoreReference.text = Level.Instance.Score.ToString();

        StartCoroutine(ScoreCounter());
    }

    private void HandleStageSelected()
    {
        LevelManager.Instance.LoadStageSelectionScene();
    }

    private IEnumerator ScoreCounter()
    {
        float elapsedTime = 0.0f;
        float duration = 2f;

        while (elapsedTime < duration)
        {
            int currentScore = Mathf.RoundToInt(elapsedTime / duration * Level.Instance.Score);
            elapsedTime += Time.deltaTime;
            scoreReference.text = currentScore.ToString();

            if (currentScore == Level.Instance.ScoreLimits[0])
            {
                leftStarReference.Fill();
            }

            if (currentScore == Level.Instance.ScoreLimits[1])
            {
                middleStarReference.Fill();
            }

            if (currentScore == Level.Instance.ScoreLimits[2])
            {
                rightStarReference.Fill();
            }

            yield return null;
        }

        scoreReference.text = Level.Instance.Score.ToString();
    }
}
