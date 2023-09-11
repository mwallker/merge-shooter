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

    void Awake()
    {
        Messaging<GunBuildEvent>.Register((platform) =>
        {
            if (Gun.Cost <= coins)
            {
                Gun gun = Instantiate(gunPrefab, Input.mousePosition, Quaternion.identity);
                gun.BuildAt(platform);

                coins -= Gun.Cost;

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

        Messaging<MonsterAttacksCoreEvent>.Register((damage) =>
        {
            health -= damage;

            UpdateBaseHealth();
        });

        UpdateCoins();

        UpdateBaseHealth();
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
