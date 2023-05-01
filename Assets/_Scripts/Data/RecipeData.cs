using UnityEngine;
using static IngredientData;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 1)]
public class RecipeData : ScriptableObject {

    [Header("Recipe")]
    public string recipeName;
    [Range(1, 3)] public int difficultyLevel;

    [Header("Ingredient 1")]
    public IngredientData ingredient1;
    [Range(0, 5)] public int amountRequired1;
    public IngredientState ingredientState1;
    public IngredientCut ingredientCut1;

    [Header("Ingredient 2")]
    public IngredientData ingredient2;
    [Range(0, 5)] public int amountRequired2;
    public IngredientState ingredientState2;
    public IngredientCut ingredientCut2;

    [Header("Ingredient 3")]
    public IngredientData ingredient3;
    [Range(0, 5)] public int amountRequired3;
    public IngredientState ingredientState3;
    public IngredientCut ingredientCut3;

    [Header("Ingredient 4")]
    public IngredientData ingredient4;
    [Range(0, 5)] public int amountRequired4;
    public IngredientState ingredientState4;
    public IngredientCut ingredientCut4;

}