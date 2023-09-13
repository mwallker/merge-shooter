using UnityEngine;

[CreateAssetMenu(fileName = "Gun_Tier_00", menuName = "Asset/Gun")]
public class GunTierTemplate : ScriptableObject
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
