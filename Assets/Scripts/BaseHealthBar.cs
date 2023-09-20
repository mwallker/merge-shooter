using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBar : MonoBehaviour
{
  [SerializeField]
  private Image healthBarForeGround;

  public void SetHitPoints(float currentHitPoints, float maxHitPoints)
  {
    healthBarForeGround.fillAmount = Mathf.Clamp(currentHitPoints / maxHitPoints, 0, 1f);
  }
}
