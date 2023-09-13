using UnityEngine;

public class LevelEndScreen : MonoBehaviour
{
    [SerializeField]
    private LevelState TargetState;

    void Start()
    {
        Messaging<LevelStateChangedEvent>.Register(OnLevelStateChanged);

        gameObject.SetActive(false);
    }

    private void OnLevelStateChanged(LevelState currentState)
    {
        if (TargetState == currentState)
        {
            gameObject.SetActive(true);
        }
    }
}
