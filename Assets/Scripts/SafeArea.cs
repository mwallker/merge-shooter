using UnityEngine;

public class SafeArea : MonoBehaviour
{
    void Awake()
    {
        RectTransform uiArea = GetComponent<RectTransform>();

        float scaleX = uiArea.sizeDelta.x / Screen.width;
        float scaleY = uiArea.sizeDelta.y / Screen.height;

        float deltaX = Mathf.Floor(Screen.safeArea.x * scaleX);
        float deltaY = Mathf.Floor(Screen.safeArea.y * scaleY);

        float width = Mathf.Floor(Screen.safeArea.width * scaleX);
        float height = Mathf.Floor(Screen.safeArea.height * scaleY);

        uiArea.pivot = new Vector2(0.5f, 0);
        uiArea.sizeDelta = new Vector2(width, height);
        uiArea.anchoredPosition = new Vector2(deltaX, deltaY);

        Debug.Log(Camera.main.aspect);
        Debug.Log(Camera.main.fieldOfView);
        Debug.Log(Camera.main.rect);
        Debug.Log(Camera.main.orthographicSize);
    }
}
