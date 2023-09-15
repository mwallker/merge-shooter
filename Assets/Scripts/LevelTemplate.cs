using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_00", menuName = "Asset/Level")]
public class LevelTemplate : ScriptableObject
{
    public int Id = 0;

    public float BaseHealth = 100f;

    public float BaseCoins = 10f;

    public float MonsterSpawnRate = 1f;

    public int GunMaxTier = 1;

    public List<MonsterTemplate> Monsters = new();
}
