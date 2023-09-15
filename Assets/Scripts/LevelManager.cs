using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private TextMeshProUGUI _loadingScreen;

    void Start()
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

    public async void LoadScene(string name)
    {
        var scene = SceneManager.LoadSceneAsync(name);
        scene.allowSceneActivation = false;

        _loadingScreen.gameObject.SetActive(true);

        do
        {
            await Task.Delay(100);
            Debug.Log(scene.progress);
        } while (scene.progress < 0.9f);
    }
}
