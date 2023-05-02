using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using static IngredientData;
using FMODUnity;

public class Tile_Station : Tile {

    public enum StationType { Stove, Grill, CuttingBoard };

    [HorizontalLine(color: EColor.Gray)]
    [Header("Station Tile")]
    [SerializeField] public StationType currentStationType;
    [Space]
    [SerializeField] protected SpriteRenderer stationSpriteRenderer;
    [Space]
    [SerializeField] protected Sprite stoveSprite;
    [SerializeField] protected Sprite grillSprite;
    [SerializeField] protected Sprite cuttingBoardSprite;
    [Space]
    [SerializeField] protected bool preparingIngredient;
    public float maxProgress;
    public float currentProgress;
    [SerializeField] private bool directionOfOriginBlocked;
    [SerializeField] EventReference cooking;
    [SerializeField] EventReference grilling;
    [SerializeField] EventReference slicing;

    protected override void Update() {
        if (ingredientInTile && ingredientInTile.currentTile != this) ingredientInTile = null;

        if (TileEmpty()) {
            preparingIngredient = false;
        }
        else if (!TileEmpty() && !preparingIngredient) {
            StartCoroutine(PrepareIngredient());
        }
    }

    #region Tile Function Functions

    protected IEnumerator PrepareIngredient() {
        preparingIngredient = true;
        directionOfOriginBlocked = false;
        currentProgress = 0;

        if (currentStationType == StationType.Stove) { maxProgress = ingredientInTile.data.cookTime; FMODUnity.RuntimeManager.PlayOneShot(cooking); }
        else if (currentStationType == StationType.Grill) { maxProgress = ingredientInTile.data.grillTime; FMODUnity.RuntimeManager.PlayOneShot(grilling); }
        else if (currentStationType == StationType.CuttingBoard) { maxProgress = ingredientInTile.data.sliceTime; FMODUnity.RuntimeManager.PlayOneShot(slicing); }

        while (preparingIngredient) {
            currentProgress += Time.deltaTime;

            if (currentProgress >= maxProgress) {
                if (currentStationType == StationType.Stove) ingredientInTile.ChangeIngredientState(IngredientState.Cooked);
                else if (currentStationType == StationType.Grill) ingredientInTile.ChangeIngredientState(IngredientState.Grilled);
                else if (currentStationType == StationType.CuttingBoard) ingredientInTile.ChangeIngredientCut(IngredientCut.Sliced);

                SendIngredient();

                currentProgress = 0;
                preparingIngredient = false;
                break;
            }
            else yield return null;
        }
    }

    private void SendIngredient() {
        currentTileDirection = ingredientInTile.directionOfOrigin;
        if (!GetTargetTileAndSendItem()) directionOfOriginBlocked = true;

        if (directionOfOriginBlocked) {
            switch (currentTileDirection) {
                case TileDirection.None:
                    currentTileDirection = TileDirection.Right;
                    SendItemToTargetedTile();
                    break;
                case TileDirection.Right:
                    currentTileDirection = TileDirection.Down;
                    SendItemToTargetedTile();
                    break;
                case TileDirection.Left:
                    currentTileDirection = TileDirection.Up;
                    SendItemToTargetedTile();
                    break;
                case TileDirection.Up:
                    currentTileDirection = TileDirection.Right;
                    SendItemToTargetedTile();
                    break;
                case TileDirection.Down:
                    currentTileDirection = TileDirection.Left;
                    SendItemToTargetedTile();
                    break;
            }
        }

        preparingIngredient = false;
    }

    protected override void UpdateTile() {
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
    }

    #endregion

}