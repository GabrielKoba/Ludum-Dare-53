using UnityEngine;
using UnityEngine.UI;
using static Tile_Station;

public class UI_Station : UI_Element {

    private Slider slider;
    public Tile_Station station;
    [SerializeField] private Image background;
    [SerializeField] private Image filler;
    [Space]
    [SerializeField] private Sprite grillSprite;
    [SerializeField] private Sprite stoveSprite;
    [SerializeField] private Sprite cuttingBoardSprite;
    private Color desiredColor;

    private void Awake() {
        slider = GetComponent<Slider>();
    }
    private void Update() {
        if (station) {
            if (station.currentStationType == StationType.Grill) {
                background.sprite = grillSprite;
                desiredColor = new Color32(229, 141, 5, 255);
            }
            else if (station.currentStationType == StationType.Stove) {
                background.sprite = stoveSprite;
                desiredColor = new Color32(41, 171, 255, 255);
            }
            else if (station.currentStationType == StationType.CuttingBoard) {
                background.sprite = cuttingBoardSprite;
                desiredColor = new Color32(195, 159, 126, 255);
            }

            if (!station.ingredientInTile || station.currentProgress < 0.5f) {
                background.color = Color.clear;
                filler.color = Color.clear;
            }
            else {
                background.color = Color.white;
                filler.color = desiredColor;

                slider.maxValue = station.maxProgress;
                slider.value = station.currentProgress;
            }
        }
    }
}