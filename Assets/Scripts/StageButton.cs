using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI StageNumberReference;

    [SerializeField]
    private Star LeftStarReference;

    [SerializeField]
    private Star MiddleStarReference;

    [SerializeField]
    private Star RightStarReference;

    private Button _button;

    private LevelConfig _levelConfig;

    void OnDisable()
    {
        _button.onClick.RemoveAllListeners();

        LeftStarReference.Clear();
        MiddleStarReference.Clear();
        LeftStarReference.Clear();
    }

    private void HandleStageSelected()
    {
        LevelManager.Instance.LoadLevelScene(_levelConfig);
    }

    private int GetScoreByLevelId(string id)
    {
        if (_levelConfig != null && PlayerPrefs.HasKey(id))
        {
            return PlayerPrefs.GetInt(id);
        }

        return 0;
    }

    private bool IsEnabled()
    {
        if (_levelConfig.PreviousId == -1)
        {
            return true;
        }

        return GetScoreByLevelId(_levelConfig.PreviousId.ToString()) > 0;
    }

    public void SetLevelConfig(LevelConfig config)
    {
        if (config != null)
        {
            _levelConfig = config;
            StageNumberReference.text = _levelConfig.Id.ToString();

            int score = GetScoreByLevelId(StageNumberReference.text);

            if (score > 0)
            {
                LeftStarReference.Fill();
            }

            if (score >= _levelConfig.AverageScore)
            {
                MiddleStarReference.Fill();
            }

            if (score == _levelConfig.MaxScore)
            {
                RightStarReference.Fill();
            }

            _button = GetComponent<Button>();
            _button.interactable = IsEnabled();
            _button.onClick.AddListener(HandleStageSelected);
        }
    }
}
