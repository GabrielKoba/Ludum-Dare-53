using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Tile : MonoBehaviour {

    public enum TileDirection { None, Right, Left, Up, Down };

    [Header("Tile")]
    [SerializeField] protected TileDirection currentTileDirection;
    public float neighbouringTilesDistance;
    protected RaycastHit2D targetedTileHit;
    [SerializeField] protected Tile targetedTile;
    [Space]
    [SerializeField] public IngredientInstance ingredientInTile;
    [SerializeField] protected bool currentlySendingIngredient;
    [SerializeField] protected float conveyorMoveSpeed = 0.5f;

    protected virtual void OnValidate() {
        UpdateTile();
    }
    protected virtual void Awake() {
        UpdateTile();
    }
    protected virtual void Start() {
        UpdateTile();
    }

    protected virtual void Update() {
        if (ingredientInTile && ingredientInTile.currentTile != this) ingredientInTile = null;
        if (!TileEmpty()) GetTargetTileAndSendItem();
    }

    #region Tile Directions & Neighbour

    public void ClearTileDirection() {
        currentTileDirection = TileDirection.None;

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

    protected virtual void UpdateTile() { }

    public bool TileEmpty() {
        if (ingredientInTile) return false;
        else return true;
    }
    protected bool GetTargetTileAndSendItem() {
        currentlySendingIngredient = true;

        switch (currentTileDirection) {
            case TileDirection.None:
                return false;
            case TileDirection.Right:
                targetedTileHit = Physics2D.Raycast(transform.position, Vector2.right, neighbouringTilesDistance + 0.01f);
                return SendItemToTargetedTile();
            case TileDirection.Left:
                targetedTileHit = Physics2D.Raycast(transform.position, -Vector2.right, neighbouringTilesDistance + 0.01f);
                return SendItemToTargetedTile();
            case TileDirection.Up:
                targetedTileHit = Physics2D.Raycast(transform.position, Vector2.up, neighbouringTilesDistance + 0.01f);
                return SendItemToTargetedTile();
            case TileDirection.Down:
                targetedTileHit = Physics2D.Raycast(transform.position, -Vector2.up, neighbouringTilesDistance + 0.01f);
                return SendItemToTargetedTile();
        }

        return false;
    }
    protected virtual bool SendItemToTargetedTile()  {
        if (targetedTileHit.collider != null && !this.TileEmpty() && targetedTileHit.collider.gameObject.tag == "Tile") {
            targetedTile = targetedTileHit.collider.GetComponent<Tile>();

            UpdateTile();
            ingredientInTile.MoveItemToTile(targetedTile, currentTileDirection, conveyorMoveSpeed);

            currentlySendingIngredient = false;
            return true;
        }

        currentlySendingIngredient = false;
        return false;
    }
    #endregion

}