using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public int MaxGunTier { get; private set; }

    public int Score { get; private set; }

    public static readonly int DefaultGunTier = 1;

    public List<MonsterTemplate> Monsters { get; private set; }

    [SerializeField]
    private TextMeshProUGUI CoinsCounterLabel;

    [SerializeField]
    private TextMeshProUGUI BaseHealthLabel;

    [SerializeField]
    private GameObject DefeatScreen;

    [SerializeField]
    private GameObject PauseScreen;

    [SerializeField]
    private WinScreen WinScreenReference;

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

        if (LevelManager.Instance != null)
        {
            CurrentCoins = LevelManager.Instance.SelectedLevel.BaseCoins;
            CurrentHealth = LevelManager.Instance.SelectedLevel.BaseHealth;
            AliveMonsters = LevelManager.Instance.SelectedLevel.Monsters.Count;
            MaxGunTier = LevelManager.Instance.SelectedLevel.MaxGunTier;

            UpdateCoins();
            UpdateBaseHealth();
        }
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

    void OnDisable()
    {
        Stop();

        Instance = null;
    }

    public GunTierTemplate GetTierById(int tierId)
    {
        for (int i = 0; i < MaxGunTier; i++)
        {
            if (gunsTierList[i].Id == tierId)
            {
                return gunsTierList[i];
            }
        }

        return null;
    }

    private void UpdateCoins()
    {
        CoinsCounterLabel.text = CurrentCoins.ToString();

        Messaging<LevelBalanceChangeEvent>.Trigger?.Invoke(CurrentCoins);
    }

    private void UpdateBaseHealth()
    {
        BaseHealthLabel.text = $"{CurrentHealth} / {LevelManager.Instance.SelectedLevel.BaseHealth}";
    }

    private void OnGunBuild(GunPlatform platform)
    {
        GunTierTemplate tier = GetTierById(DefaultGunTier);

        Gun gun = Instantiate(gunPrefab, Input.mousePosition, Quaternion.identity);
        gun.Build(platform);
        gun.Upgrade(tier);

        platform.Install(gun);

        CurrentCoins -= tier.Cost;

        UpdateCoins();
    }

    private void OnGunUpgrade(Gun gun)
    {
        GunTierTemplate tier = GetTierById(gun.Tier + 1);

        gun.Upgrade(tier);

        CurrentCoins -= tier.Cost;

        UpdateCoins();
    }

    private void OnMonsterDefeated(Monster monster)
    {
        CurrentCoins += monster.Reward;
        Score += monster.Score;
        AliveMonsters--;

        UpdateCoins();
        CheckConditions();
    }

    private void OnMonsterHitCore(Monster monster)
    {
        CurrentHealth -= monster.HitPoints;
        AliveMonsters--;

        UpdateBaseHealth();
        CheckConditions();
    }

    private bool CheckConditions()
    {
        if (CurrentState != LevelState.Idle)
        {
            return true;
        }

        if (IsFailed())
        {
            CurrentState = LevelState.Failed;
            DefeatScreen.SetActive(true);
        }
        else if (IsCompleted())
        {
            CurrentState = LevelState.Completed;
            Score += LevelManager.Instance.SelectedLevel.CompletionReward;

            WinScreenReference.Activate();
        }

        if (CurrentState != LevelState.Idle)
        {
            Messaging<LevelStateChangedEvent>.Trigger?.Invoke(CurrentState);
            SaveScores();
            Stop();

            return true;
        }

        return false;
    }

    private bool IsCompleted()
    {
        return AliveMonsters <= 0;
    }

    private bool IsFailed()
    {
        return CurrentHealth <= 0;
    }

    private void SaveScores()
    {
        if (IsNewRecord() && CurrentState == LevelState.Completed)
        {
            PlayerPrefs.SetInt(LevelManager.Instance.SelectedLevel.Id.ToString(), Score);
            PlayerPrefs.Save();
        }
    }

    private bool IsNewRecord()
    {
        int id = LevelManager.Instance.SelectedLevel.Id;

        if (PlayerPrefs.HasKey(id.ToString()))
        {
            int currentScore = PlayerPrefs.GetInt(id.ToString());

            return Score > currentScore;
        }

        return true;
    }

    public void PauseLevel()
    {
        Time.timeScale = 0f;
        PauseScreen.SetActive(true);
    }

    public void ResumeLevel()
    {
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
    }

    public void RestartLevel()
    {
        ResumeLevel();
        LevelManager.Instance.RestartLevelScene();
    }

    public void Exit()
    {
        LevelManager.Instance.LoadStageSelectionScene();
    }
}
