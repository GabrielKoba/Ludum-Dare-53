using UnityEngine;
using FMODUnity;

public class Tile_Conveyor : Tile
{

    [Header("Conveyor Tile")]
    [SerializeField] protected SpriteRenderer conveyorSpriteRenderer;
    [SerializeField] protected Animator conveyorAnimator;
    [SerializeField] protected string conveyorAnimatorString;
    [SerializeField] EventReference conveyorTurn;
    FMOD.Studio.EventInstance turnSFX;

    protected override void Start() {
        base.Start();
    }

    protected override void UpdateTile() {
        if (!conveyorSpriteRenderer) conveyorSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        switch (currentTileDirection) {
            case TileDirection.None:
                conveyorSpriteRenderer.color = Color.clear;
                conveyorSpriteRenderer.sprite = null;
                conveyorSpriteRenderer.flipX = false;
                conveyorSpriteRenderer.flipY = false;
                break;
            case TileDirection.Right:
                conveyorSpriteRenderer.flipX = false;
                conveyorAnimator.SetBool(conveyorAnimatorString, true);
                conveyorSpriteRenderer.color = Color.white;
                break;
            case TileDirection.Left:
                conveyorSpriteRenderer.flipX = true;
                conveyorAnimator.SetBool(conveyorAnimatorString, true);
                conveyorSpriteRenderer.color = Color.white;
                break;
            case TileDirection.Up:
                conveyorSpriteRenderer.flipY = false;
                conveyorAnimator.SetBool(conveyorAnimatorString, false);
                conveyorSpriteRenderer.color = Color.white;
                break;
            case TileDirection.Down:
                conveyorSpriteRenderer.flipY = true;
                conveyorAnimator.SetBool(conveyorAnimatorString, false);
                conveyorSpriteRenderer.color = Color.white;
                break;
        }
    }
    protected override bool SendItemToTargetedTile() {
        if (targetedTileHit.collider != null && !this.TileEmpty()) {
            if (targetedTileHit.collider.gameObject.tag == "Tile") {
                targetedTile = targetedTileHit.collider.GetComponent<Tile>();

                ingredientInTile.MoveItemToTile(targetedTile, currentTileDirection, conveyorMoveSpeed);
                this.UpdateTile();

                currentlySendingIngredient = false;
                return true;
            }
            else if (targetedTileHit.collider.gameObject.tag == "Station") {
                targetedTile = targetedTileHit.collider.GetComponent<Tile_Station>();

                ingredientInTile.MoveItemToTile(targetedTile, currentTileDirection, conveyorMoveSpeed);
                this.UpdateTile();

                currentlySendingIngredient = false;
                return true;
            }
            else if (targetedTileHit.collider.gameObject.tag == "Delivery") {
                targetedTile = targetedTileHit.collider.GetComponent<Tile_Delivery>();

                ingredientInTile.MoveItemToTile(targetedTile, currentTileDirection, conveyorMoveSpeed);
                this.UpdateTile();

                currentlySendingIngredient = false;
                return true;
            }
        }

        currentlySendingIngredient = false;
        return false;
    }

}