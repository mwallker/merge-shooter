using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private TextMeshProUGUI loadingScreen;

    [SerializeField]
    private List<LevelConfig> levels = new();

    public LevelConfig SelectedLevel { get; private set; }

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

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadStageSelectionScene()
    {
        SceneManager.LoadScene("StageSelection");
    }

    public void LoadLevelScene(LevelConfig level)
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

    public List<LevelConfig> GetList()
    {
        return levels;
    }
}
