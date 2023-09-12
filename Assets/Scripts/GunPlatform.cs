using UnityEngine;

public class GunPlatform : MonoBehaviour
{
    private Gun installedGun;

    public int Line { get; private set; }

    public void AssignToLine(int line)
    {
        Line = line;
    }

    private void OnMouseDown()
    {
        // Install gun at platform
        if (installedGun == null)
        {
            installedGun = Messaging<GunBuildEvent>.Trigger?.Invoke(this);
        }
        else
        {
            Messaging<GunUpgradeEvent>.Trigger?.Invoke(installedGun);
        }
    }
}
