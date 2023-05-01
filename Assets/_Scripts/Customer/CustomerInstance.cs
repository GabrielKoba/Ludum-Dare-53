using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using static IngredientData;

public class CustomerInstance : MonoBehaviour {

    private UI_Manager UIManager = UI_Manager.Instance;
    private CustomerManager customerManager = CustomerManager.Instance;

    [Header("Customer")]
    [SerializeField] private SpriteRenderer customerRenderer;
    [SerializeField] public CustomerData customerData;
    public Tile_Delivery customerTile;
    public RecipeData customerOrder;
    [Space]
    [Range(0, 60)] public float currentPatience;
    [Range(0, 60)] public float maxPatience;

    private void Awake() {
        customerRenderer.GetComponent<SpriteRenderer>();
    }
    private void Update() {
        currentPatience -= Time.deltaTime;

        if (customerTile && customerOrder && CheckOrderComplete()) {
            LeaveHappy();
        }
        else if (currentPatience <= 0) {
            LeaveAngry();
        }
    }

    public void UpdateCostumer() {
        customerRenderer.sprite = customerData.sprite;
        customerRenderer.color = Color.white;

        UIManager.GenerateCostumerUI(this);
        customerTile.customerOrder = customerOrder;
    }
    private bool CheckOrderComplete() {
        if (CompareIngredient(1) && CompareIngredient(2) && CompareIngredient(3) && CompareIngredient(4)) {
            return true;
        }
        else return false;
    }
    private bool CompareIngredient(int ingredientSlotToCompare) {
        switch (ingredientSlotToCompare) {
            case 1:
                if (customerOrder.ingredient1 == null) return true;
                else if (customerTile.ingredient1 == null) return false;
                else if (customerTile.ingredient1 == customerOrder.ingredient1 && customerTile.ingredientState1 == customerOrder.ingredientState1 && customerTile.ingredientCut1 == customerOrder.ingredientCut1) return true;
                else return false;
            case 2:
                if (customerOrder.ingredient2 == null) return true;
                else if (customerTile.ingredient2 == null) return false;
                else if (customerTile.ingredient2 == customerOrder.ingredient2 && customerTile.ingredientState2 == customerOrder.ingredientState2 && customerTile.ingredientCut2 == customerOrder.ingredientCut2) return true;
                else return false;
            case 3:
                if (customerOrder.ingredient3 == null) return true;
                else if (customerTile.ingredient3 == null) return false;
                else if (customerTile.ingredient3 == customerOrder.ingredient3 && customerTile.ingredientState3 == customerOrder.ingredientState3 && customerTile.ingredientCut3 == customerOrder.ingredientCut3) return true;
                else return false;
            case 4:
                if (customerOrder.ingredient4 == null) return true;
                else if (customerTile.ingredient4 == null) return false;
                else if (customerTile.ingredient4 == customerOrder.ingredient4 && customerTile.ingredientState4 == customerOrder.ingredientState4 && customerTile.ingredientCut4 == customerOrder.ingredientCut4) return true;
                else return false;
        }

        return false;
    }

    private void LeaveHappy() {
        customerManager.RemoveCustomer(this);
    }
    private void LeaveAngry() {
        customerManager.RemoveCustomer(this);
    }

}