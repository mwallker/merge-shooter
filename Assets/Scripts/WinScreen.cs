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

            if (currentScore == LevelManager.Instance.SelectedLevel.MinScore)
            {
                leftStarReference.Fill();
            }

            if (currentScore == LevelManager.Instance.SelectedLevel.AverageScore)
            {
                middleStarReference.Fill();
            }

            if (currentScore == LevelManager.Instance.SelectedLevel.MaxScore)
            {
                rightStarReference.Fill();
            }

            yield return null;
        }

        scoreReference.text = Level.Instance.Score.ToString();
    }
}
