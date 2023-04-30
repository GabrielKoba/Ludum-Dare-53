using UnityEngine;
using UnityEngine.UI;

public class UI_ProgressBar : UI_Element {

    private Slider slider;
    public Tile_Station station;
    [SerializeField] private Image background;
    [SerializeField] private Image filler;

    private void Awake() {
        slider = GetComponent<Slider>();
    }
    private void Update() {
        if (station) {
            slider.maxValue = station.maxProgress;
            slider.value = station.currentProgress;

            if (station.currentProgress <= 0.01f) {
                background.color = Color.clear;
                filler.color = Color.clear;
            }
            else {
                background.color = Color.white;
                filler.color = Color.white;
            }
        }
    }

}