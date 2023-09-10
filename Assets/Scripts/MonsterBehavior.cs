using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    void FixedUpdate()
    {
        Monster[] monsters = FindObjectsOfType<Monster>();

        foreach (Monster monster in monsters)
        {
            if (monster.gameObject.activeSelf)
            {
                monster.transform.Translate(Vector2.down * monster.Speed * Time.deltaTime);
            }
        }
    }
}
