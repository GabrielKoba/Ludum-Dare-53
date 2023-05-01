using UnityEngine;
using static IngredientData;

public class Tile_Delivery : Tile {

    [Header("Delivery Tile")]

    [HideInInspector] public RecipeData customerOrder;

    [Header("Ingredient 1")]
    public IngredientData ingredient1;
    public IngredientState ingredientState1;
    public IngredientCut ingredientCut1;

    [Header("Ingredient 2")]
    public IngredientData ingredient2;
    public IngredientState ingredientState2;
    public IngredientCut ingredientCut2;

    [Header("Ingredient 3")]
    public IngredientData ingredient3;
    public IngredientState ingredientState3;
    public IngredientCut ingredientCut3;

    [Header("Ingredient 4")]
    public IngredientData ingredient4;
    public IngredientState ingredientState4;
    public IngredientCut ingredientCut4;

    private bool FeedIngredient(IngredientData ingredientToAdd, IngredientState ingredientToAddState, IngredientCut ingredientToAddCut) {
        bool fedIngredient = false;

        if (ingredientToAdd == customerOrder.ingredient1 && ingredientToAddState == customerOrder.ingredientState1 && ingredientToAddCut == customerOrder.ingredientCut1) {
            ingredient1 = ingredientToAdd;
            ingredientState1 = ingredientToAddState;
            ingredientCut1 = ingredientToAddCut;
            fedIngredient = true;
        }
        if (ingredientToAdd == customerOrder.ingredient2 && ingredientToAddState == customerOrder.ingredientState2 && ingredientToAddCut == customerOrder.ingredientCut2) {
            ingredient1 = ingredientToAdd;
            ingredientState1 = ingredientToAddState;
            ingredientCut1 = ingredientToAddCut;
            fedIngredient = true;
        }
        if (ingredientToAdd == customerOrder.ingredient3 && ingredientToAddState == customerOrder.ingredientState3 && ingredientToAddCut == customerOrder.ingredientCut3) {
            ingredient1 = ingredientToAdd;
            ingredientState1 = ingredientToAddState;
            ingredientCut1 = ingredientToAddCut;
            fedIngredient = true;
        }
        if (ingredientToAdd == customerOrder.ingredient4 && ingredientToAddState == customerOrder.ingredientState4 && ingredientToAddCut == customerOrder.ingredientCut4) {
            ingredient1 = ingredientToAdd;
            ingredientState1 = ingredientToAddState;
            ingredientCut1 = ingredientToAddCut;
            fedIngredient = true;
        }

        return fedIngredient;
    }
}