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
    private int spawnAmount = 20;

    private readonly Monster[] monsters = new Monster[20];

    void Start()
    {
        StartCoroutine(SpawnMonsterWithInterval());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnMonsterWithInterval()
    {
        for (int monsterIndex = 0; monsterIndex < spawnAmount; monsterIndex++)
        {
            monsters[monsterIndex] = Instantiate(monsterPrefab).AttackLine(Random.Range(0, Grid.LinesCount));

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
