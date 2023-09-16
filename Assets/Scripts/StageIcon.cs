using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageIcon : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI StageNumberReference;

    [SerializeField]
    private LevelTemplate Level;

    private Button _button;

    void Awake()
    {
        if (Level != null)
        {
            StageNumberReference.text = Level.Id.ToString();

            _button = GetComponent<Button>();
            _button.onClick.AddListener(HandleStageSelected);
        }
    }

    void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void HandleStageSelected()
    {
        LevelManager.Instance.LoadLevelScene(Level);
    }
}
