using System.IO.Pipes;
using UnityEngine;

public class Tile : MonoBehaviour {

    public enum TileType { Preparator, Cooker };
    public enum TileDirection { Empty, Right, Left, Up, Down };

    [Header("Tile Direction")]
    [SerializeField] private TileDirection currentTileDirection;
    [Space]
    [SerializeField] private SpriteRenderer arrowSpriteRenderer;
    [Space]
    [SerializeField] private Sprite horizontalArrowSprite;
    [SerializeField] private Sprite veriticalArrowSprite;

    private void OnValidate() {
        UpdateTile();
    }
    private void Awake() {
        UpdateTile();
    }

    #region Tile Direction

    public void ClearTile() {
        currentTileDirection = TileDirection.Empty;

        UpdateTile();
    }

    public void ChangeTileDirection(Vector2 tileDirection) {
        if (tileDirection.x > 0) {
            currentTileDirection = TileDirection.Right;
        }
        else if (tileDirection.x < 0) {
            currentTileDirection = TileDirection.Left;
        }
        else if (tileDirection.y > 0) {
            currentTileDirection = TileDirection.Up;
        }
        else if (tileDirection.y < 0) {
            currentTileDirection = TileDirection.Down;
        }

        UpdateTile();
    }

    public void UpdateTile() {
        switch (currentTileDirection) {
            case TileDirection.Right:
                arrowSpriteRenderer.color = Color.white;
                arrowSpriteRenderer.sprite = horizontalArrowSprite;
                arrowSpriteRenderer.flipX = false;
                break;
            case TileDirection.Left:
                arrowSpriteRenderer.color = Color.white;
                arrowSpriteRenderer.sprite = horizontalArrowSprite;
                arrowSpriteRenderer.flipX = true;
                break;
            case TileDirection.Up:
                arrowSpriteRenderer.color = Color.white;
                arrowSpriteRenderer.sprite = veriticalArrowSprite;
                arrowSpriteRenderer.flipY = false;
                break;
            case TileDirection.Down:
                arrowSpriteRenderer.color = Color.white;
                arrowSpriteRenderer.sprite = veriticalArrowSprite;
                arrowSpriteRenderer.flipY = true;
                break;
            case TileDirection.Empty:
                arrowSpriteRenderer.color = Color.clear;
                break;
        }
    }

    #endregion

}