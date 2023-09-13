using UnityEngine;

public class GunPlatform : MonoBehaviour
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

    private void OnMouseDown()
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
