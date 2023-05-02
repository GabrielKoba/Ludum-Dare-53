using UnityEngine;
using UnityEngine.UI;
using static IngredientData;

public class UI_Customer : UI_Element {

    private Slider slider;
    [SerializeField] private Image progressBarBackground;
    [SerializeField] private Image progressBarFiller;
    public CustomerInstance customer;
    [Space]
    [SerializeField] private Image bubble2;
    [SerializeField] private Image bubble3;
    [SerializeField] private Image bubble4;
    [Space]
    [SerializeField] private GameObject grillIcon;
    [SerializeField] private GameObject stoveIcon;
    [SerializeField] private GameObject cuttingBoardIcon;
    [Space]
    [SerializeField] private Image check1;
    [SerializeField] private Image check2;
    [SerializeField] private Image check3;
    [SerializeField] private Image check4;
    [Space]
    [SerializeField] private Image ingredient1;
    [SerializeField] private Image ingredient2;
    [SerializeField] private Image ingredient3;
    [SerializeField] private Image ingredient4;
    [Space]
    [SerializeField] private RectTransform layout;
    [SerializeField] private RectTransform ingredient1Layout;
    [SerializeField] private RectTransform ingredient2Layout;
    [SerializeField] private RectTransform ingredient3Layout;
    [SerializeField] private RectTransform ingredient4Layout;
    private bool bubbleSpawned;

    private void Awake() {
        slider = GetComponent<Slider>();
    }
    private void Start() {
        UpdateUI();
    }

    public void UpdateUI() {
        transform.position = customer.transform.position;

        if (customer.customerOrder.ingredient4 != null) {
            ingredient4Layout.gameObject.SetActive(true);
            if (!bubbleSpawned) { bubble4.color = Color.white; layout.parent = bubble4.transform; bubbleSpawned = true; }

            if (customer.customerOrder.ingredientState4 == IngredientState.Grilled) {
                Instantiate(stoveIcon, transform.position, transform.rotation, ingredient4Layout.transform);

                if (customer.customerOrder.ingredientCut4 == IngredientCut.Sliced) ingredient4.sprite = customer.customerOrder.ingredient4.slicedGrilledSprite;
                else ingredient4.sprite = customer.customerOrder.ingredient4.simpleGrilledSprite;
            }
            else if (customer.customerOrder.ingredientState4 == IngredientState.Cooked) {
                Instantiate(stoveIcon, transform.position, transform.rotation, ingredient4Layout.transform);

                if (customer.customerOrder.ingredientCut4 == IngredientCut.Sliced) ingredient4.sprite = customer.customerOrder.ingredient4.slicedCookedSprite;
                else ingredient4.sprite = customer.customerOrder.ingredient4.simpleCookedSprite;
            }
            else {
                if (customer.customerOrder.ingredientCut4 == IngredientCut.Sliced) ingredient4.sprite = customer.customerOrder.ingredient4.slicedRawSprite;
                else ingredient4.sprite = customer.customerOrder.ingredient4.simpleRawSprite;
            }
        }
        if (customer.customerOrder.ingredient3 != null) {
            ingredient3Layout.gameObject.SetActive(true);
            if (!bubbleSpawned) { bubble3.color = Color.white; layout.parent = bubble3.transform; bubbleSpawned = true; }

            if (customer.customerOrder.ingredientState3 == IngredientState.Grilled) {
                Instantiate(stoveIcon, transform.position, transform.rotation, ingredient3Layout.transform);

                if (customer.customerOrder.ingredientCut3 == IngredientCut.Sliced) ingredient3.sprite = customer.customerOrder.ingredient3.slicedGrilledSprite;
                else ingredient3.sprite = customer.customerOrder.ingredient3.simpleGrilledSprite;
            }
            else if (customer.customerOrder.ingredientState3 == IngredientState.Cooked) {
                Instantiate(stoveIcon, transform.position, transform.rotation, ingredient3Layout.transform);

                if (customer.customerOrder.ingredientCut3 == IngredientCut.Sliced) ingredient3.sprite = customer.customerOrder.ingredient3.slicedCookedSprite;
                else ingredient3.sprite = customer.customerOrder.ingredient3.simpleCookedSprite;
            }
            else {
                if (customer.customerOrder.ingredientCut3 == IngredientCut.Sliced) ingredient3.sprite = customer.customerOrder.ingredient3.slicedRawSprite;
                else ingredient3.sprite = customer.customerOrder.ingredient3.simpleRawSprite;
            }        
        }
        if (customer.customerOrder.ingredient2 != null) {
            ingredient1Layout.gameObject.SetActive(true);
            ingredient2Layout.gameObject.SetActive(true);

            if (!bubbleSpawned) { bubble2.color = Color.white; layout.parent = bubble2.transform; bubbleSpawned = true; }

            if (customer.customerOrder.ingredientState2 == IngredientState.Grilled) {
                Instantiate(stoveIcon, transform.position, transform.rotation, ingredient2Layout.transform);

                if (customer.customerOrder.ingredientCut2 == IngredientCut.Sliced) ingredient2.sprite = customer.customerOrder.ingredient2.slicedGrilledSprite;
                else ingredient2.sprite = customer.customerOrder.ingredient2.simpleGrilledSprite;
            }
            else if (customer.customerOrder.ingredientState2 == IngredientState.Cooked) {
                Instantiate(stoveIcon, transform.position, transform.rotation, ingredient2Layout.transform);

                if (customer.customerOrder.ingredientCut2 == IngredientCut.Sliced) ingredient2.sprite = customer.customerOrder.ingredient2.slicedCookedSprite;
                else ingredient2.sprite = customer.customerOrder.ingredient2.simpleCookedSprite;
            }
            else {
                if (customer.customerOrder.ingredientCut2 == IngredientCut.Sliced) ingredient2.sprite = customer.customerOrder.ingredient2.slicedRawSprite;
                else ingredient2.sprite = customer.customerOrder.ingredient2.simpleRawSprite;
            }

            if (customer.customerOrder.ingredientState1 == IngredientState.Grilled) {
                Instantiate(stoveIcon, transform.position, transform.rotation, ingredient1Layout.transform);

                if (customer.customerOrder.ingredientCut1 == IngredientCut.Sliced) ingredient1.sprite = customer.customerOrder.ingredient1.slicedGrilledSprite;
                else ingredient1.sprite = customer.customerOrder.ingredient1.simpleGrilledSprite;
            }
            else if (customer.customerOrder.ingredientState1 == IngredientState.Cooked) {
                Instantiate(stoveIcon, transform.position, transform.rotation, ingredient1Layout.transform);

                if (customer.customerOrder.ingredientCut1 == IngredientCut.Sliced) ingredient1.sprite = customer.customerOrder.ingredient1.slicedCookedSprite;
                else ingredient1.sprite = customer.customerOrder.ingredient1.simpleCookedSprite;
            }
            else {
                if (customer.customerOrder.ingredientCut1 == IngredientCut.Sliced) ingredient1.sprite = customer.customerOrder.ingredient1.slicedRawSprite;
                else ingredient1.sprite = customer.customerOrder.ingredient1.simpleRawSprite;
            }
        }
    }

    private void Update() {
        if (customer.customerTile.ingredient4 != null) check4.color = Color.white;
        if (customer.customerTile.ingredient3 != null) check3.color = Color.white;
        if (customer.customerTile.ingredient2 != null) check2.color = Color.white;
        if (customer.customerTile.ingredient1 != null) check1.color = Color.white;

        if (customer) {
            slider.maxValue = customer.maxPatience;
            slider.value = customer.currentPatience;

            if (customer.currentPatience <= 0.01f) {
                progressBarBackground.color = Color.clear;
                progressBarFiller.color = Color.clear;
            }
            else {
                progressBarBackground.color = Color.white;
                progressBarFiller.color = new Color32(62, 54, 70, 255);
            }
        }
        else {
            Destroy(gameObject);
        }
    }

}