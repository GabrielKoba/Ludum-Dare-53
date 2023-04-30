using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;
using static FoodData;

public class Item : MonoBehaviour {

    [Header("Item")]
    public FoodData data;
    [SerializeField] private SpriteRenderer itemSpriteRenderer;
    
    [Header("Food")]
    protected FoodState currentState;
    protected FoodStyle currentStyle;

    private Vector2 currentVelocity = Vector2.zero;
    private float timeToReachTile;

    private void OnValidate() {
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
        UpdateItem();
    }
    private void Awake() {
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
        UpdateItem();
    }
    private void Update() {
        transform.localPosition = Vector2.SmoothDamp(transform.localPosition, Vector3.zero, ref currentVelocity, timeToReachTile);
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
    public void MoveItemToTile(Tile tileToMoveTo, float timeToReachTile) {
        this.timeToReachTile = timeToReachTile;

        if (tileToMoveTo.TileEmpty()) {
            gameObject.transform.SetParent(tileToMoveTo.transform);
            UpdateItem();

            tileToMoveTo.UpdateTile();
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
