using UnityEngine;

public class TilesGenerator : MonoBehaviour
{
    public int size;
    public int length;
    public int height;

    Tile[][] tiles;

    [SerializeField]
    Transform tileParent;

    private void Awake()
    {
        LoadTiles();
    }

    private void LoadTiles()
    {
        int x, y;

        tiles = new Tile[length][];
        Tile[] rawTiles = tileParent.GetComponentsInChildren<Tile>();

        for (int i = 0; i < length; i++)
        {
            x = i % length;
            tiles[x] = new Tile[height];

            for (int j = 0; j < height; j++)
            {
                y = j;

                tiles[x][y] = rawTiles[(y * length) + x];
                tiles[x][y].InitTile(new Vector2Int(x, y), this);
            }

        }
    }

    public void HoverMultipleTiles(Vector2Int center)
    {
        MultipleTilesActions(center, true);
    }

    public void UnhoverMultipleTiles(Vector2Int center)
    {
        MultipleTilesActions(center, false);
    }

    public void MultipleTilesActions(Vector2Int center, bool hovering)
    {
        tiles[center.x][center.y].HoverTile(hovering);

        for (int x = 0; x < length; x++)
        {
            if (x >= center.x - size && x <= center.x + size)
            {
                for (int y = 0; y < height; y++)
                {
                    if (y >= center.y - size && y <= center.y + size)
                    {
                        tiles[x][y].HoverTile(hovering);
                    }
                }
            }
        }
    }
}
