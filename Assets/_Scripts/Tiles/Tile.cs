using NaughtyAttributes;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Tile : MonoBehaviour {

    public enum TileDirection { None, Right, Left, Up, Down };

    [Header("Tile")]
    [SerializeField] protected TileDirection currentTileDirection;
    [Space]
    [SerializeField] protected SpriteRenderer conveyorSpriteRenderer;
    [Space]
    [SerializeField] protected Sprite horizontalConveyorSprite;
    [SerializeField] protected Sprite veriticalConveyorSprite;
    [Space]
    [SerializeField] protected float conveyorTimeToMove = 0.5f;
    public float neighbouringTilesDistance;
    protected RaycastHit2D targetedTileHit;
    [SerializeField] protected Tile targetedTile;
    [Space]
    [SerializeField] protected Item itemInTile;

    protected virtual void OnValidate() {
        UpdateTile();
    }
    protected virtual void Awake() {
        UpdateTile();
    }
    protected virtual void Update() {
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

    [Button("Update Tile")]
    public virtual void UpdateTile() {
        switch (currentTileDirection) {
            case TileDirection.None:
                conveyorSpriteRenderer.color = Color.clear;
                conveyorSpriteRenderer.sprite = null;
                conveyorSpriteRenderer.flipX = false;
                conveyorSpriteRenderer.flipY = false;
                break;
            case TileDirection.Right:
                conveyorSpriteRenderer.flipX = false;
                conveyorSpriteRenderer.sprite = horizontalConveyorSprite;
                conveyorSpriteRenderer.color = Color.white;
                break;
            case TileDirection.Left:
                conveyorSpriteRenderer.flipX = true;
                conveyorSpriteRenderer.sprite = horizontalConveyorSprite;
                conveyorSpriteRenderer.color = Color.white;
                break;
            case TileDirection.Up:
                conveyorSpriteRenderer.flipY = false;
                conveyorSpriteRenderer.sprite = veriticalConveyorSprite;
                conveyorSpriteRenderer.color = Color.white;
                break;
            case TileDirection.Down:
                conveyorSpriteRenderer.flipY = true;
                conveyorSpriteRenderer.sprite = veriticalConveyorSprite;
                conveyorSpriteRenderer.color = Color.white;
                break;
        }

        if (GetComponentInChildren<Item>()) itemInTile = GetComponentInChildren<Item>();
        else if (!GetComponentInChildren<Item>()) itemInTile = null;
    }
    public bool TileEmpty() {
        if (itemInTile) return false;
        else return true;
    }
    protected void GetTargetTileAndSendItem() {
        switch (currentTileDirection) {
            case TileDirection.None:
                break;
            case TileDirection.Right:
                targetedTileHit = Physics2D.Raycast(transform.position, Vector2.right, neighbouringTilesDistance + 0.01f);
                SendItemToNeighbour();
                break;
            case TileDirection.Left:
                targetedTileHit = Physics2D.Raycast(transform.position, -Vector2.right, neighbouringTilesDistance + 0.01f);
                SendItemToNeighbour();
                break;
            case TileDirection.Up:
                targetedTileHit = Physics2D.Raycast(transform.position, Vector2.up, neighbouringTilesDistance + 0.01f);
               SendItemToNeighbour();
                break;
            case TileDirection.Down:
                targetedTileHit = Physics2D.Raycast(transform.position, -Vector2.up, neighbouringTilesDistance + 0.01f);
                SendItemToNeighbour();
                break;
        }
    }
    private void SendItemToNeighbour() {
        if (targetedTileHit.collider != null && targetedTileHit.collider.gameObject.tag == "Tile") {
            targetedTile = targetedTileHit.collider.GetComponent<Tile>();
            itemInTile.MoveItemToTile(targetedTile, conveyorTimeToMove);

            UpdateTile();
        }
    }

    #endregion

}