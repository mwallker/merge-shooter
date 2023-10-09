using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private Monster monsterPrefab;

    [SerializeField]
    private MonsterMarker monsterMarkerPrefab;

    private readonly List<Monster> _spawnedMonsters = new();
    private readonly List<MonsterMarker> _spawnedMarkers = new(PlatformGrid.LinesCount);

    void Start()
    {
        for (int i = 0; i < _spawnedMarkers.Capacity; i++)
        {
            var marker = Instantiate(monsterMarkerPrefab, new Vector3(i * 2f - 5f, 10f, 0f), Quaternion.identity);
            marker.gameObject.SetActive(false);

            _spawnedMarkers.Add(marker);
        }

        StartCoroutine(SpawnMonsterWithInterval());

        Messaging<LevelStateChangedEvent>.Register(HandleLevelStateChange);
    }

    void OnDisable()
    {
        StopAllCoroutines();

        Messaging<LevelStateChangedEvent>.Unregister(HandleLevelStateChange);
    }

    private IEnumerator SpawnMonsterWithInterval()
    {
        if (LevelManager.Instance != null)
        {
            for (int i = 0; i < LevelManager.Instance.SelectedLevel.Monsters.Count; i++)
            {
                var parameters = LevelManager.Instance.SelectedLevel.Monsters[i];
                var monster = Instantiate(monsterPrefab).SetAttackLine(Random.Range(0, PlatformGrid.LinesCount)).SetParameters(parameters);

                _spawnedMonsters.Add(monster);
                _spawnedMarkers[monster.Line].SetMarker(monster);

                yield return new WaitForSeconds(LevelManager.Instance.SelectedLevel.MonsterSpawnRate);
            }
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

        for (int i = 0; i < _spawnedMarkers.Capacity; i++)
        {
            _spawnedMarkers[i].UpdateMarker();
        }
    }

    private void HandleLevelStateChange(LevelState state)
    {
        StopAllCoroutines();
    }
}
