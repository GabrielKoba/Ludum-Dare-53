using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static FoodData;
using static Tile;

public class Item : MonoBehaviour {

    [Header("Item")]
    public FoodData data;
    [SerializeField] private SpriteRenderer itemSpriteRenderer;

    [Header("Food")]
    protected FoodState currentState;
    protected FoodStyle currentStyle;

    public Tile currentTile;
    public List<Tile> tilesToMoveThrough;
    [HideInInspector] public TileDirection directionOfOrigin;
    private float itemMoveSpeed;

    private void OnValidate() {
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
        UpdateItem();
    }
    private void Awake() {
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
        UpdateItem();
    }
    private void Update() {
        if (currentTile) currentTile.itemInTile = this;

        if (tilesToMoveThrough.Count > 0) {
            if (transform.position == tilesToMoveThrough[0].transform.position) {
                currentTile = tilesToMoveThrough[0];
                tilesToMoveThrough[0].UpdateTile();
                tilesToMoveThrough.Remove(tilesToMoveThrough[0]);
            }
            else {
                float step = itemMoveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, tilesToMoveThrough[0].transform.position, step);
            }
        }
        else {
            transform.position = currentTile.transform.position;
        }
    }

    #region Item Functions

    public void ChangeItemState(FoodState state) {
        currentState = state;
        UpdateItem();
    }
    public void ChangeItemCut(FoodStyle style) {
        currentStyle = style;
        UpdateItem();
    }
    public void MoveItemToTile(Tile tileToMoveTo, TileDirection directionOfOrigin, float timeToReachTile) {
        this.directionOfOrigin = directionOfOrigin;
        this.itemMoveSpeed = timeToReachTile;

        if (!tilesToMoveThrough.Contains(tileToMoveTo) && tileToMoveTo.TileEmpty()) {
            tilesToMoveThrough.Add(tileToMoveTo);
        }
    }
    public void UpdateItem() {
        switch (currentState) {
            case FoodState.Raw:
                if (currentStyle == FoodStyle.Sliced) {
                    itemSpriteRenderer.sprite = data.slicedRawSprite;
                }
                else {
                    itemSpriteRenderer.sprite = data.simpleRawSprite;
                }
                itemSpriteRenderer.color = Color.white;

                break;

            case FoodState.Cooked:
                if (currentStyle == FoodStyle.Sliced) {
                    itemSpriteRenderer.sprite = data.slicedCookedSprite;
                }
                else {
                    itemSpriteRenderer.sprite = data.simpleCookedSprite;
                }
                itemSpriteRenderer.color = Color.white;

                break;

            case FoodState.Grilled:
                if (currentStyle == FoodStyle.Sliced) {
                    itemSpriteRenderer.sprite = data.slicedGrilledSprite;
                }
                else {
                    itemSpriteRenderer.sprite = data.simpleGrilledSprite;
                }
                itemSpriteRenderer.color = Color.white;

                break;
        }
    }

    #endregion

}
