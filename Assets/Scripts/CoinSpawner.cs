using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField]
    private Coin coinPrefab;

    private Pool<Coin> _coinsPool;

    void Start()
    {
        _coinsPool = new Pool<Coin>(CreateCoin, GetCoin, ReleaseCoin);

        Messaging<MonsterDefeatedEvent>.Register(HandleMonsterDefeat);
    }

    void OnDisable()
    {
        _coinsPool.Clear();

        Messaging<MonsterDefeatedEvent>.Unregister(HandleMonsterDefeat);
    }

    private Coin CreateCoin()
    {
        return Instantiate(coinPrefab);
    }

    private void GetCoin(Coin coin)
    {
        coin.gameObject.SetActive(true);
    }

    private void ReleaseCoin(Coin coin)
    {
        coin.gameObject.SetActive(false);
    }

    private void HandleMonsterDefeat(Monster monster)
    {
        Coin coin = _coinsPool.Get();

        coin.SetPool(_coinsPool);
        coin.SetValue(monster.Reward);
        coin.SetPosition(monster.transform.position);
    }
}
