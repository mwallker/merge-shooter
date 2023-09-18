using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_00", menuName = "Asset/Level")]
public class LevelConfig : ScriptableObject
{
    public int Id = 0;

    public int PreviousId = -1;

    public float BaseHealth = 100f;

    public float BaseCoins = 10f;

    public float MonsterSpawnRate = 1f;

    public int MaxGunTier = 1;

    public List<MonsterTemplate> Monsters = new();

    public int[] ScoreLimits = new int[3] { 0, 1, 2 };
}
