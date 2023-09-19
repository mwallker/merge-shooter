using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private Monster monsterPrefab;

    private readonly List<Monster> _spawnedMonsters = new();

    void Start()
    {
        // Messaging<LevelStateChangedEvent>.Register((state) => StopAllCoroutines());

        StartCoroutine(SpawnMonsterWithInterval());
    }

    private IEnumerator SpawnMonsterWithInterval()
    {
        for (int i = 0; i < LevelManager.Instance.SelectedLevel.Monsters.Count; i++)
        {
            var parameters = LevelManager.Instance.SelectedLevel.Monsters[i];
            var monster = Instantiate(monsterPrefab).SetAttackLine(Random.Range(0, PlatformGrid.LinesCount)).SetParameters(parameters);

            _spawnedMonsters.Add(monster);

            yield return new WaitForSeconds(LevelManager.Instance.SelectedLevel.MonsterSpawnRate);
        }
    }

    void FixedUpdate()
    {
        if (Level.Instance.CurrentState != LevelState.Idle)
        {
            return;
        }

        foreach (var monster in _spawnedMonsters)
        {
            if (monster.gameObject.activeSelf)
            {
                monster.transform.Translate(monster.Speed * Time.deltaTime * Vector2.down);
            }
        }
    }
}
