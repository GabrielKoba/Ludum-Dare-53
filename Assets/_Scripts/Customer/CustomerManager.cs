using System.Collections.Generic;
using UnityEngine;
using static Tile;

public class CustomerManager : MonoBehaviour {

    #region Singleton

    private static CustomerManager instance;
    public static CustomerManager Instance { get { return instance; } }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    #endregion

    [Header("Customer Settings")]
    [SerializeField] private List<CustomerData> possibleCustomers;
    [SerializeField] private List<Tile_Delivery> possibleCustomerTiles;
    [Range(5f, 30f)][SerializeField] private float timeUntilAddCustomer = 30f;
    [SerializeField] private float countToAddCustomer;
    [Range(10f, 60f)][SerializeField] private float customerPatience = 60f;
    [Space]
    [SerializeField] TileDirection customersAnchor = TileDirection.Up;
    [SerializeField] private Dictionary<CustomerInstance, Tile_Delivery> activeCustomers = new Dictionary<CustomerInstance, Tile_Delivery>();
    [Space]
    [Header("Order Settings")]
    [SerializeField][Range(1, 3)] private int currentDifficultyLevel = 1;
    [SerializeField] private List<RecipeData> possibleOrdersDifficulty1;
    [SerializeField] private List<RecipeData> possibleOrdersDifficulty2;
    [SerializeField] private List<RecipeData> possibleOrdersDifficulty3;
    private RecipeData randomOrder;

    private void Update() {
        countToAddCustomer += Time.deltaTime;

        if (countToAddCustomer >= timeUntilAddCustomer && activeCustomers.Count < possibleCustomerTiles.Count) {
            AddCostumer();
        }
    }
    private void AddCostumer() {
        countToAddCustomer = 0;

        // Generate costumer data.
        CustomerData randomCustomer = possibleCustomers[Random.Range(0, possibleCustomers.Count)];
        Tile_Delivery randomCustomerTile = possibleCustomerTiles[Random.Range(0, possibleCustomerTiles.Count)];

        if (currentDifficultyLevel == 1) randomOrder = possibleOrdersDifficulty1[Random.Range(0, possibleOrdersDifficulty1.Count)];
        else if (currentDifficultyLevel == 2) randomOrder = possibleOrdersDifficulty2[Random.Range(0, possibleOrdersDifficulty2.Count)];
        else if (currentDifficultyLevel == 3) randomOrder = possibleOrdersDifficulty3[Random.Range(0, possibleOrdersDifficulty3.Count)];
        else randomOrder = possibleOrdersDifficulty1[Random.Range(0, possibleOrdersDifficulty1.Count)];

        // Spawn Costumer & Set Values. 
        GameObject instantiatedCustomer = Instantiate(randomCustomer.prefab, transform.position, transform.rotation, transform);
        CustomerInstance instantiatedCustomerInstance = instantiatedCustomer.GetComponent<CustomerInstance>();

        instantiatedCustomerInstance.customerData = randomCustomer;
        instantiatedCustomerInstance.customerTile = randomCustomerTile; 
        instantiatedCustomerInstance.customerOrder = randomOrder;
        instantiatedCustomerInstance.maxPatience = customerPatience;
        instantiatedCustomerInstance.currentPatience = customerPatience;
        instantiatedCustomerInstance.UpdateCostumer();

        // Align Costumer to tile.
        if (randomCustomerTile) {
            switch (customersAnchor) {
                case TileDirection.None:
                    instantiatedCustomer.transform.position = new Vector2(randomCustomerTile.transform.position.x, randomCustomerTile.transform.position.y);
                    break;
                case TileDirection.Left:
                    instantiatedCustomer.transform.position = new Vector2(randomCustomerTile.transform.position.x + -randomCustomerTile.neighbouringTilesDistance, randomCustomerTile.transform.position.y);
                    break;
                case TileDirection.Right:
                    instantiatedCustomer.transform.position = new Vector2(randomCustomerTile.transform.position.x + randomCustomerTile.neighbouringTilesDistance, randomCustomerTile.transform.position.y);
                    break;
                case TileDirection.Down:
                    instantiatedCustomer.transform.position = new Vector2(randomCustomerTile.transform.position.x, randomCustomerTile.transform.position.y + -randomCustomerTile.neighbouringTilesDistance);
                    break;
                case TileDirection.Up:
                    instantiatedCustomer.transform.position = new Vector2(randomCustomerTile.transform.position.x, randomCustomerTile.transform.position.y + randomCustomerTile.neighbouringTilesDistance);
                    break;
            }
        }

        activeCustomers.Add(instantiatedCustomerInstance, randomCustomerTile);
        possibleCustomerTiles.Remove(randomCustomerTile);
    }

    public void RemoveCustomer(CustomerInstance customerToRemove) {
        activeCustomers.Remove(customerToRemove, out customerToRemove.customerTile);
        possibleCustomerTiles.Add(customerToRemove.customerTile);

        Destroy(customerToRemove.gameObject);
    }
}