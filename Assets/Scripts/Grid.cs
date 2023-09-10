using UnityEngine;

public class Grid : MonoBehaviour
{
    public static readonly int LinesCount = 6;
    public static readonly float Width = 2;

    private GunPlatform[] gunPlatforms;

    [SerializeField]
    private GunPlatform gunPlatformPrefab;

    void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        gunPlatforms = new GunPlatform[LinesCount];

        for (int i = 0; i < LinesCount; i++)
        {
            gunPlatforms[i] = Instantiate(gunPlatformPrefab, GetLinePosition(i), Quaternion.identity);
            gunPlatforms[i].AssignToLine(i);
        }
    }

    private Vector2 GetLinePosition(int line)
    {
        return new Vector2((-LinesCount / 2 + 0.5f + line) * Width, transform.position.y);
    }
}
