using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using static FoodData;

public class Tile_Station : Tile {

    protected enum StationType { Stove, Grill, CuttingBoard };

    [HorizontalLine(color: EColor.Gray)]
    [Header("Station Tile")]
    [SerializeField] protected StationType currentTileFunction;
    [Space]
    [SerializeField] protected SpriteRenderer stationSpriteRenderer;
    [Space]
    [SerializeField] protected Sprite stoveSprite;
    [SerializeField] protected Sprite grillSprite;
    [SerializeField] protected Sprite cuttingBoardSprite;
    [Space]
    [SerializeField] protected bool preparingItem;
    public float currentProgress;

    protected override void Update() {
        if (!TileEmpty()) UseStation();
    }

    #region Tile Function Functions

    protected void UseStation() {
        switch (currentTileFunction) {
            case StationType.Stove:
                if (!preparingItem) StartCoroutine(PrepareItemAndSendToTargetTile(StationType.Stove));
                break;
            case StationType.Grill:
                if (!preparingItem) StartCoroutine(PrepareItemAndSendToTargetTile(StationType.Grill));
                break;
            case StationType.CuttingBoard:
                if (!preparingItem) StartCoroutine(PrepareItemAndSendToTargetTile(StationType.CuttingBoard));
                break;
        }
    }

    protected IEnumerator PrepareItemAndSendToTargetTile(StationType stationType) {
        while (true) {
            preparingItem = true;
            currentProgress += Time.deltaTime;

            switch (stationType) {
                case (StationType.Stove) :
                    yield return new WaitForSeconds(itemInTile.data.cookTime);

                    itemInTile.ChangeItemState(FoodState.Cooked);
                    GetTargetTileAndSendItem();

                    break;
                case (StationType.Grill) :
                    yield return new WaitForSeconds(itemInTile.data.grillTime);

                    itemInTile.ChangeItemState(FoodState.Grilled);
                    GetTargetTileAndSendItem();

                    break;
                case (StationType.CuttingBoard) :
                    yield return new WaitForSeconds(itemInTile.data.sliceTime);

                    itemInTile.ChangeItemCut(FoodStyle.Sliced);
                    GetTargetTileAndSendItem();

                    break;
            }

            preparingItem = false;
            currentProgress = 0;
        }
    }

    public override void UpdateTile() {
        base.UpdateTile();

        switch (currentTileFunction) {
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