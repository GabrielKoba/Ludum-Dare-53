using UnityEngine;
using UnityEngine.UI;

public class UI_Customer : UI_Element {

    private Slider slider;
    public CustomerInstance customer;
    [SerializeField] private Image progressBarBackground;
    [SerializeField] private Image progressBarFiller;

    private void Awake() {
        slider = GetComponent<Slider>();
    }
    private void Update() {
        if (customer) {
            slider.maxValue = customer.maxPatience;
            slider.value = customer.currentPatience;

            if (customer.currentPatience <= 0.01f) {
                progressBarBackground.color = Color.clear;
                progressBarFiller.color = Color.clear;
            }
            else {
                progressBarBackground.color = Color.white;
                progressBarFiller.color = Color.white;
            }
        }
        else {
            Destroy(gameObject);
        }
    }

}