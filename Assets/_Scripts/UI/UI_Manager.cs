using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_Manager : MonoBehaviour {

    [Header("References")]
    [SerializeField] List<Tile_Station> stations;

    [Header("UI Settings")]
    [SerializeField] Transform progressBarsParent;
    [SerializeField] GameObject progressBarPrefab;

    private void Start() {
        GenerateUI();
    }

    private void GenerateUI() {        
        foreach (Tile_Station station in stations) {
            GameObject instantiatedProgressBar = Instantiate(progressBarPrefab, station.transform.position, station.transform.rotation, progressBarsParent.transform);
            instantiatedProgressBar.GetComponent<UI_ProgressBar>().station = station;
        }

    }
}