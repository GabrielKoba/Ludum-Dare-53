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
    protected IngredientCut currentCut;


    private void OnValidate() {
        iconRenderer = GetComponent<SpriteRenderer>();
        UpdateItem();
    }
    private void Awake() {
        iconRenderer = GetComponent<SpriteRenderer>();
        UpdateItem();
    }
    private void Update() {
        if (currentTile) currentTile.ingredientInTile = this;

        if (tileToMoveTo) {
            if (transform.position == tileToMoveTo.transform.position) {
                currentTile = tileToMoveTo;
                // tileToMoveTo.UpdateTile();
                tileToMoveTo = null;
            }
            else {
                float step = ingredientMoveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, tileToMoveTo.transform.position, step);
            }
        }
        else {
            transform.position = currentTile.transform.position;

            if (currentTile.gameObject.tag == ("Delivery")) {
                currentTile.GetComponent<Tile_Delivery>().DeliverIngredient(data, currentState, currentCut);
                Destroy(gameObject);
            }
        }
    }

    #region Item Functions

    public void ChangeIngredientState(IngredientState state) {
        currentState = state;
        UpdateItem();
    }
    public void ChangeIngredientCut(IngredientCut style) {
        currentCut = style;
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
                if (currentCut == IngredientCut.Sliced) {
                    iconRenderer.sprite = data.slicedRawSprite;
                }
                else {
                    iconRenderer.sprite = data.simpleRawSprite;
                }
                iconRenderer.color = Color.white;

                break;

            case IngredientState.Cooked:
                if (currentCut == IngredientCut.Sliced) {
                    iconRenderer.sprite = data.slicedCookedSprite;
                }
                else {
                    iconRenderer.sprite = data.simpleCookedSprite;
                }
                iconRenderer.color = Color.white;

                break;

            case IngredientState.Grilled:
                if (currentCut == IngredientCut.Sliced) {
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
