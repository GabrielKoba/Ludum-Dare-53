using UnityEngine;

public class UI_Element : MonoBehaviour {

    public void Show() {
        gameObject.SetActive(true);
    }
    public void Hide() {
        gameObject.SetActive(false);
    }

}