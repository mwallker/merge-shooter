using UnityEngine;

[CreateAssetMenu(fileName = "Monster_00", menuName = "Asset/Monster")]
public class MonsterTemplate : ScriptableObject
{
    public int Id = 0;

    public float HitPoints = 10;

    public float Speed = 2f;

    public float Reward = 5f;

    public Sprite Body;

    public Sprite Eyes;

    public Sprite LeftHand;

    public Sprite RightHand;

    public Sprite Mouth;

    public Color ColorTint;
}
