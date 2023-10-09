using UnityEngine;

public class SafeArea : MonoBehaviour
{
    void Awake()
    {
        RectTransform uiArea = GetComponent<RectTransform>();

        float width = 0f;
        float height = - (Screen.height - Screen.safeArea.y - Screen.safeArea.height);

        uiArea.offsetMin = new Vector2(width, Screen.safeArea.y);
        uiArea.offsetMax = new Vector2(width, height);

        float minimalWidth = 6.5f;

        if (Camera.main.aspect * Camera.main.orthographicSize < minimalWidth) {
            Camera.main.orthographicSize = minimalWidth / Camera.main.aspect;
        }
    }
}
