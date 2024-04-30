using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int position;

    TilesGenerator generator;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Sprite tile, tileHover, tileSelect;

    public void InitTile(Vector2Int position, TilesGenerator generator)
    {
        this.position = position;
        this.generator = generator;
    }

    public void ClickTile()
    {
        if (generator.selectHover)
        {
            generator.playerManager.Skill(generator.GetMultipleTiles(position));
        }
    }

    private void OnMouseEnter()
    {
        generator.currentTile = this;
        generator.HoverMultipleTiles(position);
    }

    private void OnMouseExit()
    {
        generator.currentTile = null;
        generator.UnhoverMultipleTiles(position);
    }

    public void HoverTile(bool hovering, bool select)
    {
        spriteRenderer.sprite = (hovering) ? ((select) ? tileSelect : tileHover) : tile;
    }

    public void UnhoverTile()
    {
        spriteRenderer.sprite = tile;
    }
}
