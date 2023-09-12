using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private float health = 100;

    [SerializeField]
    private float coins = 30;

    [SerializeField]
    private TextMeshProUGUI CoinsCounterLabel;

    [SerializeField]
    private TextMeshProUGUI BaseHealthLabel;

    [SerializeField]
    private Gun gunPrefab;

    [SerializeField]
    private List<GunTier> gunsTierList = new();

    void Awake()
    {
        Messaging<GunBuildEvent>.Register((platform) =>
        {
            GunTier tier = GetTierById(0);

            if (tier.Cost <= coins)
            {
                Gun gun = Instantiate(gunPrefab, Input.mousePosition, Quaternion.identity);
                gun.Build(platform);
                gun.Upgrade(tier);

                coins -= tier.Cost;

                UpdateCoins();

                return gun;
            }

            return null;
        });

        Messaging<MonsterDefeatedEvent>.Register((reward) =>
        {
            coins += reward;

            UpdateCoins();
        });

        Messaging<GunUpgradeEvent>.Register((gun) =>
        {
            GunTier tier = GetTierById(gun.Tier + 1);

            if (tier != null && tier.Cost <= coins)
            {
                gun.Upgrade(tier);

                coins -= tier.Cost;

                UpdateCoins();
            }
        });

        Messaging<MonsterAttacksCoreEvent>.Register((damage) =>
        {
            health -= damage;

            UpdateBaseHealth();
        });

        UpdateCoins();

        UpdateBaseHealth();
    }

    private GunTier GetTierById(int tierId)
    {
        if (tierId >= gunsTierList.Count)
        {
            return null;
        }

        return gunsTierList[tierId];
    }

    private void UpdateCoins()
    {
        CoinsCounterLabel.text = coins.ToString();
    }

    private void UpdateBaseHealth()
    {
        BaseHealthLabel.text = health.ToString();
    }
}
