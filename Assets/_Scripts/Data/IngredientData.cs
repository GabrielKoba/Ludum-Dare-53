using UnityEngine;

public class IngredientData : ScriptableObject {

    public enum IngredientState { Raw, Grilled, Cooked };
    public enum IngredientCut { Simple, Sliced };

    [Header("FoodData Settings")]
    public Sprite simpleRawSprite;
    public Sprite simpleCookedSprite;
    public Sprite simpleGrilledSprite;
    public Sprite slicedRawSprite;
    public Sprite slicedCookedSprite;
    public Sprite slicedGrilledSprite;
    [Space]
    [Range(0f, 20f)] public float grillTime;
    [Range(0f, 20f)] public float cookTime;
    [Range(0f, 20f)] public float sliceTime;
    [Space]
    public GameObject prefab;

}