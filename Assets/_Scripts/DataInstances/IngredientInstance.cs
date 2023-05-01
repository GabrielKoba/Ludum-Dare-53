using System.Collections.Generic;
using UnityEngine;
using static IngredientData;
using static Tile;

public class IngredientInstance : MonoBehaviour {

    [SerializeField] private SpriteRenderer iconRenderer;

    [Header("Data")]
    public IngredientData data;

    [Header("Instance")]
    public Tile currentTile;
    public Tile tileToMoveTo;
    [HideInInspector] public TileDirection directionOfOrigin;
    private float ingredientMoveSpeed;
    [Space]
    protected IngredientState currentState;
    protected IngredientCut currentStyle;


    private void OnValidate() {
        iconRenderer = GetComponent<SpriteRenderer>();
        UpdateItem();
    }
    private void Awake() {
        iconRenderer = GetComponent<SpriteRenderer>();
        UpdateItem();
    }
    private void Update() {
        if (currentTile) currentTile.itemInTile = this;

        if (tileToMoveTo) {
            if (transform.position == tileToMoveTo.transform.position) {
                currentTile = tileToMoveTo;
                tileToMoveTo.UpdateTile();
                tileToMoveTo = null;
            }
            else {
                float step = ingredientMoveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, tileToMoveTo.transform.position, step);
            }
        }
        else {
            transform.position = currentTile.transform.position;
        }
    }

    #region Item Functions

    public void ChangeItemState(IngredientState state) {
        currentState = state;
        UpdateItem();
    }
    public void ChangeItemCut(IngredientCut style) {
        currentStyle = style;
        UpdateItem();
    }
    public void MoveItemToTile(Tile tileToMoveTo, TileDirection directionOfOrigin, float timeToReachTile) {
        this.directionOfOrigin = directionOfOrigin;
        this.ingredientMoveSpeed = timeToReachTile;

        if (!this.tileToMoveTo == tileToMoveTo && tileToMoveTo.TileEmpty()) {
            this.tileToMoveTo = tileToMoveTo;
        }
    }
    public void UpdateItem() {
        switch (currentState) {
            case IngredientState.Raw:
                if (currentStyle == IngredientCut.Sliced) {
                    iconRenderer.sprite = data.slicedRawSprite;
                }
                else {
                    iconRenderer.sprite = data.simpleRawSprite;
                }
                iconRenderer.color = Color.white;

                break;

            case IngredientState.Cooked:
                if (currentStyle == IngredientCut.Sliced) {
                    iconRenderer.sprite = data.slicedCookedSprite;
                }
                else {
                    iconRenderer.sprite = data.simpleCookedSprite;
                }
                iconRenderer.color = Color.white;

                break;

            case IngredientState.Grilled:
                if (currentStyle == IngredientCut.Sliced) {
                    iconRenderer.sprite = data.slicedGrilledSprite;
                }
                else {
                    iconRenderer.sprite = data.simpleGrilledSprite;
                }
                iconRenderer.color = Color.white;

                break;
        }
    }

    #endregion

}
