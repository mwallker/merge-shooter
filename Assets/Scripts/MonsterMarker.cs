using System.Collections.Generic;
using UnityEngine;

public class MonsterMarker : MonoBehaviour
{
    private Animator _animator;

    public int Line { get; set; } = -1;

    public bool Activated { get; set; } = false;

    public readonly List<Monster> TrackedMonsters = new();

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnDisappearEnd()
    {
        _animator.ResetTrigger("Disappear");
        gameObject.SetActive(false);
    }

    public void SetMarker(Monster monster)
    {
        TrackedMonsters.Add(monster);
    }

    public void UpdateMarker()
    {
        bool HasTrackableMonsters = TrackedMonsters.FindAll((monster) => monster.gameObject.activeSelf && monster.transform.position.y > 14).Count > 0;

        if (HasTrackableMonsters != Activated)
        {
            if (HasTrackableMonsters)
            {
                gameObject.SetActive(true);
            }
            else
            {
                _animator.SetTrigger("Disappear");
            }

            Activated = HasTrackableMonsters;
        }
    }
}
