using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ValueLabel;

    private Pool<Coin> poolReference;

    public void SetValue(float value)
    {
        ValueLabel.text = $"+{Mathf.RoundToInt(value)}";
    }

    public void SetPool(Pool<Coin> pool)
    {
        poolReference = pool;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void OnAnimationFinish()
    {
        poolReference.Release(this);
    }
}
