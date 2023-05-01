using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "ScriptableObjects/Customer", order = 1)]
public class CustomerData : ScriptableObject {

    [Header("Customer")]
    public Sprite sprite;
    public GameObject prefab;

}