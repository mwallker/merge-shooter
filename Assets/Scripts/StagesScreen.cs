using UnityEngine;

public class StagesScreen : MonoBehaviour
{
    [SerializeField]
    private StageButton stageButton;

    void Start()
    {
        foreach (var levelConfig in LevelManager.Instance.GetList())
        {
            var levelInstance = Instantiate(stageButton, transform);
            levelInstance.SetLevelConfig(levelConfig);
        }
    }
}
