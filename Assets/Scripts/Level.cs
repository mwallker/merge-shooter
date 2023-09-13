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

    public int DefeatedMonsters { get; private set; }

    [SerializeField]
    private LevelTemplate Config;

    [SerializeField]
    private TextMeshProUGUI CoinsCounterLabel;

    [SerializeField]
    private TextMeshProUGUI BaseHealthLabel;

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

        CurrentCoins = Config.BaseCoins;
        CurrentHealth = Config.BaseHealth;

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

    private void TransitionToFiniteState(LevelState targetPhase)
    {
        if (CurrentState != targetPhase && CurrentState == LevelState.Idle)
        {
            CurrentState = targetPhase;

            Messaging<LevelStateChangedEvent>.Trigger?.Invoke(CurrentState);

            Stop();
        }
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
        DefeatedMonsters++;

        if (DefeatedMonsters >= Config.MonstersAmount)
        {
            TransitionToFiniteState(LevelState.Completed);
        }

        UpdateCoins();
    }

    private void OnMonsterHitCore(Monster monster)
    {
        CurrentHealth -= monster.HitPoints;
        DefeatedMonsters++;

        if (CurrentHealth <= 0)
        {
            TransitionToFiniteState(LevelState.Failed);
        }

        UpdateBaseHealth();
    }
}
