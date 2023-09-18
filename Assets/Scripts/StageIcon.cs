using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageIcon : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI StageNumberReference;

    [SerializeField]
    private LevelTemplate LevelConfig;

    [SerializeField]
    private Star LeftStarReference;

    [SerializeField]
    private Star MiddleStarReference;

    [SerializeField]
    private Star RightStarReference;

    private Button _button;

    void Awake()
    {
        if (LevelConfig != null)
        {
            StageNumberReference.text = LevelConfig.Id.ToString();

            int score = GetScore();

            Debug.Log(score);

            if (score >= LevelConfig.ScoreLimits[0])
            {
                LeftStarReference.Fill();
            }

            if (score >= LevelConfig.ScoreLimits[1])
            {
                MiddleStarReference.Fill();
            }

            if (score >= LevelConfig.ScoreLimits[2])
            {
                RightStarReference.Fill();
            }

            _button = GetComponent<Button>();
            _button.interactable = score != 0;
            _button.onClick.AddListener(HandleStageSelected);
        }
    }

    void OnDisable()
    {
        _button.onClick.RemoveAllListeners();

        LeftStarReference.Clear();
        MiddleStarReference.Clear();
        LeftStarReference.Clear();
    }

    private void HandleStageSelected()
    {
        LevelManager.Instance.LoadLevelScene(LevelConfig);
    }

    private int GetScore()
    {
        if (LevelConfig != null && PlayerPrefs.HasKey(LevelConfig.Id.ToString()))
        {
            return PlayerPrefs.GetInt(LevelConfig.Id.ToString());
        }

        return 0;
    }
}
