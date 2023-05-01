using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using static IngredientData;

public class Tile_Station : Tile {

    protected enum StationType { Stove, Grill, CuttingBoard };

    [HorizontalLine(color: EColor.Gray)]
    [Header("Station Tile")]
    [SerializeField] protected StationType currentStationType;
    [Space]
    [SerializeField] protected SpriteRenderer stationSpriteRenderer;
    [Space]
    [SerializeField] protected Sprite stoveSprite;
    [SerializeField] protected Sprite grillSprite;
    [SerializeField] protected Sprite cuttingBoardSprite;
    [Space]
    public float currentProgress;
    public float maxProgress;
    private bool directionOfOriginBlocked;

    protected override void Update() {
        if (itemInTile && itemInTile.currentTile != this) itemInTile = null;
        if (!TileEmpty() && !currentlySendingItem) Prepare();
    }

    #region Tile Function Functions

    protected void UseStation() {
        preparingItem = true;
    }

    protected void Prepare() {
        if (preparingItem) {
            currentProgress += Time.deltaTime;
            directionOfOriginBlocked = false;

            switch (currentStationType) {
                case StationType.Stove:
                    maxProgress = itemInTile.data.cookTime;
                    break;
                case StationType.Grill:
                    maxProgress = itemInTile.data.grillTime;
                    break;
                case StationType.CuttingBoard:
                    maxProgress = itemInTile.data.sliceTime;
                    break;
            }

            if (currentProgress >= maxProgress) {
                preparingItem = false;
            }
        }
        else {
            currentProgress = 0;

            switch (currentStationType) {
                case (StationType.Stove):
                    itemInTile.ChangeItemState(IngredientState.Cooked);
                    break;
                case (StationType.Grill):
                    itemInTile.ChangeItemState(IngredientState.Grilled);
                    break;
                case (StationType.CuttingBoard):
                    itemInTile.ChangeItemCut(IngredientCut.Sliced);
                    break;
            }

            if (!directionOfOriginBlocked) {
                currentTileDirection = itemInTile.directionOfOrigin;
            }
            if (!GetTargetTileAndSendItem()) {
                directionOfOriginBlocked = true;

                switch (currentTileDirection) {
                    case TileDirection.None:
                        currentTileDirection = TileDirection.Right;
                        GetTargetTileAndSendItem();
                        break;
                    case TileDirection.Right:
                        currentTileDirection = TileDirection.Down;
                        GetTargetTileAndSendItem();
                        break;
                    case TileDirection.Left:
                        currentTileDirection = TileDirection.Up;
                        GetTargetTileAndSendItem();
                        break;
                    case TileDirection.Up:
                        currentTileDirection = TileDirection.Right;
                        GetTargetTileAndSendItem();
                        break;
                    case TileDirection.Down:
                        currentTileDirection = TileDirection.Left;
                        GetTargetTileAndSendItem();
                        break;
                }
            }
        }
    }

    public override void UpdateTile() {
        base.UpdateTile();

        switch (currentStationType) {
            case StationType.Stove:
                stationSpriteRenderer.sprite = stoveSprite;
                stationSpriteRenderer.color = Color.white;
                break;
            case StationType.Grill:
                stationSpriteRenderer.sprite = grillSprite;
                stationSpriteRenderer.color = Color.white;
                break;
            case StationType.CuttingBoard:
                stationSpriteRenderer.sprite = cuttingBoardSprite;
                stationSpriteRenderer.color = Color.white;
                break;
        }

        if (!TileEmpty() && !currentlySendingItem) UseStation();
    }

    #endregion

}