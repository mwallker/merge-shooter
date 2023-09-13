using System.Collections;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private Monster monsterPrefab;

    [SerializeField]
    private float spawnInterval = 1.5f;

    [SerializeField]
    private int spawnAmount = 100;

    private Monster[] monsters;

    void Start()
    {
        StartCoroutine(SpawnMonsterWithInterval());

        Messaging<LevelStateChangedEvent>.Register((state) =>
        {
            StopAllCoroutines();
        });
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnMonsterWithInterval()
    {
        monsters = new Monster[spawnAmount];

        for (int monsterIndex = 0; monsterIndex < spawnAmount; monsterIndex++)
        {
            monsters[monsterIndex] = Instantiate(monsterPrefab).SetAttackLine(Random.Range(0, Grid.LinesCount));

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
