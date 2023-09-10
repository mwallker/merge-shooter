using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private float health = 10;

    [SerializeField]
    private float coins = 10;

    [SerializeField]
    private TextMeshProUGUI CoinsCounter;

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
        });

        UpdateCoins();
    }

    private void UpdateCoins()
    {
        CoinsCounter.text = coins.ToString();
    }
}
