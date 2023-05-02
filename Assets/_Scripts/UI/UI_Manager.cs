using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Collections.Specialized.BitVector32;

public class UI_Manager : MonoBehaviour {

    #region Singleton

    private static UI_Manager instance;
    public static UI_Manager Instance { get { return instance; } }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    #endregion

    [Header("References")]
    [SerializeField] List<Tile_Station> stations;
    [SerializeField] List<UI_Customer> customersUI;

    [Header("UI Settings")]
    [SerializeField] Transform stationBarParent;
    [SerializeField] GameObject stationBarPrefab;
    [Space]
    [SerializeField] Transform customerUIParent;
    [SerializeField] GameObject customerUIPrefab;

    private void Start() {
        GenerateUI();
    }

    private void GenerateUI() {
        if (stations.Count == 0) {
            GameObject[] stationsFound = GameObject.FindGameObjectsWithTag("Station");

            foreach (GameObject stationFound in stationsFound) {
                stations.Add(stationFound.GetComponent<Tile_Station>());
            }
        }

        foreach (Tile_Station station in stations) {
            GameObject instantiatedProgressBar = Instantiate(stationBarPrefab, station.transform.position, station.transform.rotation, stationBarParent.transform);
            instantiatedProgressBar.GetComponent<UI_Station>().station = station;
        }
    }

    public void GenerateCostumerUI(CustomerInstance customer) {
        GameObject instantiatedProgressBar = Instantiate(customerUIPrefab, customer.transform.localPosition, customer.transform.localRotation, customerUIParent.transform);
        instantiatedProgressBar.GetComponent<UI_Customer>().customer = customer;
        instantiatedProgressBar.GetComponent<UI_Customer>().UpdateUI();
    }
}