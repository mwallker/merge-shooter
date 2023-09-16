using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum LevelState
{
    Completed,
    Failed,
    Idle,
}

public class Level : MonoBehaviour
{
    public static Level Instance;

    public LevelState CurrentState { get; private set; } = LevelState.Idle;

    public float CurrentHealth { get; private set; }

    public float CurrentCoins { get; private set; }

    public int AliveMonsters { get; private set; }

    public List<MonsterTemplate> Monsters { get; private set; }

    [SerializeField]
    private TextMeshProUGUI CoinsCounterLabel;

    [SerializeField]
    private TextMeshProUGUI BaseHealthLabel;

    [SerializeField]
    private GameObject DefeatScreen;

    [SerializeField]
    private GameObject WinScreen;

    [SerializeField]
    private Gun gunPrefab;

    [SerializeField]
    private List<GunTierTemplate> gunsTierList = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        CurrentCoins = LevelManager.Instance.SelectedLevel.BaseCoins;
        CurrentHealth = LevelManager.Instance.SelectedLevel.BaseHealth;
        AliveMonsters = LevelManager.Instance.SelectedLevel.Monsters.Count;
        Monsters = LevelManager.Instance.SelectedLevel.Monsters;

        UpdateCoins();
        UpdateBaseHealth();
    }

    void Start()
    {
        Messaging<GunBuildEvent>.Register(OnGunBuild);
        Messaging<GunUpgradeEvent>.Register(OnGunUpgrade);
        Messaging<MonsterDefeatedEvent>.Register(OnMonsterDefeated);
        Messaging<MonsterHitCoreEvent>.Register(OnMonsterHitCore);
    }

    void Stop()
    {
        Messaging<GunBuildEvent>.Unregister(OnGunBuild);
        Messaging<GunUpgradeEvent>.Unregister(OnGunUpgrade);
        Messaging<MonsterDefeatedEvent>.Unregister(OnMonsterDefeated);
        Messaging<MonsterHitCoreEvent>.Unregister(OnMonsterHitCore);
    }

    private GunTierTemplate GetTierById(int tierId)
    {
        if (tierId >= gunsTierList.Count)
        {
            return null;
        }

        return gunsTierList[tierId];
    }

    private void UpdateCoins()
    {
        CoinsCounterLabel.text = CurrentCoins.ToString();
    }

    private void UpdateBaseHealth()
    {
        BaseHealthLabel.text = CurrentHealth.ToString();
    }

    private void OnGunBuild(GunPlatform platform)
    {
        GunTierTemplate tier = GetTierById(0);

        if (tier.Cost <= CurrentCoins)
        {
            Gun gun = Instantiate(gunPrefab, Input.mousePosition, Quaternion.identity);
            gun.Build(platform);
            gun.Upgrade(tier);

            platform.Install(gun);

            CurrentCoins -= tier.Cost;

            UpdateCoins();
        }
    }

    private void OnGunUpgrade(Gun gun)
    {
        GunTierTemplate tier = GetTierById(gun.Tier + 1);

        if (tier != null && tier.Cost <= CurrentCoins)
        {
            gun.Upgrade(tier);

            CurrentCoins -= tier.Cost;

            UpdateCoins();
        }
    }

    private void OnMonsterDefeated(Monster monster)
    {
        CurrentCoins += monster.Reward;
        AliveMonsters--;

        IsCompleted();

        UpdateCoins();
    }

    private void OnMonsterHitCore(Monster monster)
    {
        CurrentHealth -= monster.HitPoints;
        AliveMonsters--;

        IsCompleted();

        UpdateBaseHealth();
    }

    private bool IsCompleted()
    {
        if (CurrentState != LevelState.Idle)
        {
            return true;
        }

        LevelState targetState = CurrentState;

        if (CurrentHealth <= 0)
        {
            targetState = LevelState.Failed;
            DefeatScreen.SetActive(true);
        }
        else if (AliveMonsters <= 0)
        {
            targetState = LevelState.Completed;
            WinScreen.SetActive(true);
        }

        if (CurrentState != targetState)
        {
            CurrentState = targetState;

            Messaging<LevelStateChangedEvent>.Trigger?.Invoke(CurrentState);

            Stop();

            return true;
        }

        return false;
    }
}
