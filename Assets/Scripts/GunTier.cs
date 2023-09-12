using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Tier", menuName = "Asset/Gun")]
public class GunTier : ScriptableObject
{
    public string Label = "Tier #";

    public int Id = 0;

    public float Damage = 1f;

    public float Speed = 1f;

    public float Distance = 14f;

    public float Cost = 10f;

    public Sprite Base;

    public Sprite Barrel;
}
