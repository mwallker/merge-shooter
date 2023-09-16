using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private Monster monsterPrefab;

    [SerializeField]
    private float spawnInterval = 1.5f;

    private readonly List<Monster> _spawnedMonsters = new();

    void Start()
    {
        StartCoroutine(SpawnMonsterWithInterval());

        Messaging<LevelStateChangedEvent>.Register((state) => StopAllCoroutines());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnMonsterWithInterval()
    {
        Debug.Log(Level.Instance.Monsters.Count);

        for (int i = 0; i < Level.Instance.Monsters.Count; i++)
        {
            var parameters = Level.Instance.Monsters[i];
            var monster = Instantiate(monsterPrefab).SetAttackLine(Random.Range(0, Grid.LinesCount)).SetParameters(parameters);

            Debug.Log(monster);

            _spawnedMonsters.Add(monster);

            yield return new WaitForSeconds(spawnInterval);
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
