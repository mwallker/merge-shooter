using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    [SerializeField]
    private Image foregroundReference;

    public void Fill()
    {
        foregroundReference.gameObject.SetActive(true);
    }

    public void Clear()
    {
        foregroundReference.gameObject.SetActive(false);
    }
}
