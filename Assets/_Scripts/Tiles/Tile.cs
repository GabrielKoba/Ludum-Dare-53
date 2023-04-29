using NaughtyAttributes;
using System.IO.Pipes;
using UnityEngine;

public class Tile : MonoBehaviour {

    protected enum TileDirection { Empty, Right, Left, Up, Down };
    protected enum TileFunction { None, Boiler, Grill, Slicer };

    [Header("Tile Direction")]
    [SerializeField] protected TileDirection currentTileDirection;
    [Space]
    [SerializeField] protected SpriteRenderer arrowSpriteRenderer;
    [Space]
    [SerializeField] protected Sprite horizontalArrowSprite;
    [SerializeField] protected Sprite veriticalArrowSprite;

    [Header("Tile Function")]
    [SerializeField] protected TileFunction currentTileFunction;
    [Space]
    [SerializeField] protected SpriteRenderer functionSpriteRenderer;
    [Space]
    [SerializeField] protected Sprite boilerStationSprite;
    [SerializeField] protected Sprite grillStationSprite;
    [SerializeField] protected Sprite slicerStationSprite;

    [Header("Tile Data")]
    [InfoBox("The data contained in this tile.")]
    [ShowAssetPreview]
    [SerializeField] protected TileData tileData;

    protected virtual void OnValidate() {
        UpdateTileSprites();
    }
    protected virtual void Awake() {
        UpdateTileSprites();
    }

    #region Tile Direction Functions

    public void ClearTileDirection() {
        currentTileDirection = TileDirection.Empty;

        UpdateTileSprites();
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

        UpdateTileSprites();
    }

    #endregion

    #region Tile Function Functions

    public void ApplyTileFunctionToData() {
        switch (currentTileFunction) {
            case TileFunction.None:
                
                break;
            case TileFunction.Boiler:

                break;
            case TileFunction.Grill:

                break;
            case TileFunction.Slicer:

                break;
        }
    }

    #endregion

    #region General Tile Functions

    public void UpdateTileSprites() {
        switch (currentTileFunction) {
            case TileFunction.None:
                functionSpriteRenderer.color = Color.clear;
                functionSpriteRenderer.sprite = null;
                break;
            case TileFunction.Boiler:
                functionSpriteRenderer.sprite = boilerStationSprite;
                functionSpriteRenderer.color = Color.white;
                break;
            case TileFunction.Grill:
                functionSpriteRenderer.sprite = grillStationSprite;
                functionSpriteRenderer.color = Color.white;
                break;
            case TileFunction.Slicer:
                functionSpriteRenderer.sprite = slicerStationSprite;
                functionSpriteRenderer.color = Color.white;
                break;
        }

        switch (currentTileDirection) {
            case TileDirection.Empty:
                arrowSpriteRenderer.color = Color.clear;
                arrowSpriteRenderer.sprite = null;
                arrowSpriteRenderer.flipX = false;
                arrowSpriteRenderer.flipY = false;
                break;
            case TileDirection.Right:
                arrowSpriteRenderer.flipX = false;
                arrowSpriteRenderer.sprite = horizontalArrowSprite;
                arrowSpriteRenderer.color = Color.white;
                break;
            case TileDirection.Left:
                arrowSpriteRenderer.flipX = true;
                arrowSpriteRenderer.sprite = horizontalArrowSprite;
                arrowSpriteRenderer.color = Color.white;
                break;
            case TileDirection.Up:
                arrowSpriteRenderer.flipY = false;
                arrowSpriteRenderer.sprite = veriticalArrowSprite;
                arrowSpriteRenderer.color = Color.white;
                break;
            case TileDirection.Down:
                arrowSpriteRenderer.flipY = true;
                arrowSpriteRenderer.sprite = veriticalArrowSprite;
                arrowSpriteRenderer.color = Color.white;
                break;
        }
    }

    #endregion

}