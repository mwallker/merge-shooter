using UnityEngine;

public class StagesScreen : MonoBehaviour
{
    [SerializeField]
    private StageButton stageButton;

    void Start()
    {
        foreach (var levelConfig in LevelManager.Instance.GetList())
        {
            Instantiate(stageButton, transform).SetLevelConfig(levelConfig);
        }
    }
}
