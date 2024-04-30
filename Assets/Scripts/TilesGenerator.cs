using System.Collections.Generic;
using UnityEngine;

public class TilesGenerator : MonoBehaviour
{
    public int size;
    public enum Type
    {
        Radius,
        Vertical,
        Horizontal
    };
    public Type type;

    public int length;
    public int height;
    public bool selectHover;

    Tile[][] tiles;
    public Tile currentTile;

    [SerializeField]
    Transform tileParent;
    public PlayerManager playerManager;

    private void Awake()
    {
        Default();
        LoadTiles();
    }

    public void Default()
    {
        size = 0;
        type = Type.Radius;
        selectHover = false;
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

    public void ClearTiles()
    {
        foreach (Tile[] tiles in tiles)
        {
            foreach (Tile tile in tiles)
            {
                tile.UnhoverTile();
            }
        }
    }

    public void HoverMultipleTiles(Vector2Int center)
    {
        MultipleTilesActions(center, true, selectHover);
    }

    public void UnhoverMultipleTiles(Vector2Int center)
    {
        MultipleTilesActions(center, false, selectHover);
    }

    public void MultipleTilesActions(Vector2Int center, bool hovering, bool select)
    {
        switch (type)
        {
            case Type.Radius:
                for (int x = 0; x < length; x++)
                {
                    if (x >= center.x - size && x <= center.x + size)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            if (y >= center.y - size && y <= center.y + size)
                            {
                                tiles[x][y].HoverTile(hovering, select);
                            }
                        }
                    }
                }
                break;

            case Type.Vertical:
                for (int y = 0; y < height; y++)
                {
                    if (y >= center.y - size && y <= center.y + size)
                    {
                        tiles[center.x][y].HoverTile(hovering, select);
                    }
                }
                break;

            case Type.Horizontal:
                for (int x = 0; x < length; x++)
                {
                    if (x >= center.x - size && x <= center.x + size)
                    {
                        tiles[x][center.y].HoverTile(hovering, select);
                    }
                }
                break;
        };
    }

    public List<Tile> GetMultipleTiles(Vector2Int center)
    {
        List<Tile> finalTiles = new();

        switch (type)
        {
            case Type.Radius:
                for (int x = 0; x < length; x++)
                {
                    if (x >= center.x - size && x <= center.x + size)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            if (y >= center.y - size && y <= center.y + size)
                            {
                                finalTiles.Add(tiles[x][y]);
                            }
                        }
                    }
                }
                break;

            case Type.Vertical:
                for (int y = 0; y < height; y++)
                {
                    if (y >= center.y - size && y <= center.y + size)
                    {
                        finalTiles.Add(tiles[center.x][y]);
                    }
                }
                break;

            case Type.Horizontal:
                for (int x = 0; x < length; x++)
                {
                    if (x >= center.x - size && x <= center.x + size)
                    {
                        finalTiles.Add(tiles[x][center.y]);
                    }
                }
                break;
        };

        return finalTiles;
    }
}
