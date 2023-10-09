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

    public GunTierTemplate GetNextTier()
    {
        int tierId = InstalledGun == null ? Level.DefaultGunTier : InstalledGun.Tier + 1;

        return Level.Instance.GetTierById(tierId);
    }

    public bool HasNextTier()
    {
        var nextTier = GetNextTier();

        if (nextTier == null)
        {
            return false;
        }

        return nextTier.Cost <= Level.Instance.CurrentCoins;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (HasNextTier())
        {
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
}
