using System.Collections;
using System.Threading.Tasks;
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

    void OnDisable()
    {
        scoreReference.text = "0";

        StopAllCoroutines();
    }

    public async void Activate()
    {
        await Task.Delay(2000);

        gameObject.SetActive(true);
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

            UpdateStars(currentScore);

            yield return null;
        }

        UpdateStars(Level.Instance.Score);
    }

    private void UpdateStars(int score)
    {
        if (leftStarReference.IsEmpty() && score >= LevelManager.Instance.SelectedLevel.MinScore)
        {
            leftStarReference.Fill();
            leftStarReference.Ring();
        }

        if (middleStarReference.IsEmpty() && score >= LevelManager.Instance.SelectedLevel.AverageScore)
        {
            middleStarReference.Fill();
            middleStarReference.Ring();
        }

        if (rightStarReference.IsEmpty() && score >= LevelManager.Instance.SelectedLevel.MaxScore)
        {
            rightStarReference.Fill();
            rightStarReference.Ring();
        }

        scoreReference.text = score.ToString();
    }
}
