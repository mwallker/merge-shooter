using UnityEngine;
using UnityEngine.EventSystems;

public class GunPlatform : MonoBehaviour, IPointerDownHandler
{
    public Gun InstalledGun { get; private set; }

    public int Line { get; set; }

    public void Install(Gun gun)
    {
        if (InstalledGun == null)
        {
            InstalledGun = gun;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Install gun at platform
        if (InstalledGun == null)
        {
            Messaging<GunBuildEvent>.Trigger?.Invoke(this);
        }
        else
        {
            Messaging<GunUpgradeEvent>.Trigger?.Invoke(InstalledGun);
        }
    }
}
