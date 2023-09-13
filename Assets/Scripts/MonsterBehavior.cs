using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Level.Instance.CurrentState != LevelState.Idle)
        {
            return;
        }

        Monster[] monsters = FindObjectsOfType<Monster>();

        foreach (Monster monster in monsters)
        {
            if (monster.gameObject.activeSelf)
            {
                monster.transform.Translate(monster.Speed * Time.deltaTime * Vector2.down);
            }
        }
    }
}
