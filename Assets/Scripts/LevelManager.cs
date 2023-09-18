using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private TextMeshProUGUI loadingScreen;

    public LevelTemplate SelectedLevel { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // public async void LoadScene(string name)
    // {
    //     var scene = SceneManager.LoadSceneAsync(name);
    //     scene.allowSceneActivation = false;

    //     _loadingScreen.gameObject.SetActive(true);

    //     do
    //     {
    //         await Task.Delay(100);
    //         Debug.Log(scene.progress);
    //     } while (scene.progress < 0.9f);
    // }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadStageSelectionScene()
    {
        SceneManager.LoadScene("StageSelection");
    }

    public void LoadLevelScene(LevelTemplate level)
    {
        if (level != null)
        {
            SelectedLevel = level;

            SceneManager.LoadScene("Level");
        }
    }

    public void RestartLevelScene()
    {
        SceneManager.LoadScene("Level");
    }

    public int GetScore()
    {
        if (PlayerPrefs.HasKey(SelectedLevel.Id.ToString()))
        {
            // var scores = PlayerPrefs.GetString("scores");
            // var data = JsonUtility.FromJson<LevelData>(scores);

            // Debug.Log(data.scores);
            // Debug.Log(data);

            // return data.scores.GetValueOrDefault(SelectedLevel.Id, 0);

            return PlayerPrefs.GetInt(SelectedLevel.Id.ToString());
        }

        return 0;
    }
}
