using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int position;

    TilesGenerator generator;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Sprite tile, tileHover, tileSelect;

    private void OnMouseEnter()
    {
        generator.HoverMultipleTiles(position);
    }

    private void OnMouseExit()
    {
        generator.UnhoverMultipleTiles(position);
    }

    public void HoverTile(bool hovering)
    {
        spriteRenderer.sprite = (hovering) ? tileHover : tile;
    }

    public void InitTile(Vector2Int position, TilesGenerator generator)
    {
        this.position = position;
        this.generator = generator;
    }
}
