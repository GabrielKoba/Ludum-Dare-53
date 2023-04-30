using UnityEngine;

public class Customer : MonoBehaviour {

    [Header("Customer")]
    public Tile tileToSpawnIn;

    private void OnValidate() {
        UpdateCostumer();
    }
    private void Start() {
        UpdateCostumer();
    }

    private void UpdateCostumer() {

    }
}