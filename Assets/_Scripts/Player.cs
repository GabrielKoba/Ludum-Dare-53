using UnityEngine;

public class Player : MonoBehaviour {

    private InputManager input;

    [Header("Player Settings")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _levelMask;
    private RaycastHit2D mouseHit;

    private void Start() {
        input = InputManager.Instance;
    }
    private void Update() {
        ReadFlags();
    }

    private void ReadFlags() {
        if (input.mouseLeft) {
            mouseHit = Physics2D.GetRayIntersection(_playerCamera.ScreenPointToRay(input.mouse), Mathf.Infinity, _levelMask);

            if (mouseHit.collider.gameObject.tag == "Tile") {
                Debug.Log("Tile left clicked");

                if (input.movement != Vector2.zero) {
                    mouseHit.collider.GetComponent<Tile>().ChangeTileDirection(input.movement);
                }
            }
        }
        else if (input.mouseRight) {
            mouseHit = Physics2D.GetRayIntersection(_playerCamera.ScreenPointToRay(input.mouse), Mathf.Infinity, _levelMask);

            if (mouseHit.collider.gameObject.tag == "Tile") {
                Debug.Log("Tile right clicked");

                mouseHit.collider.GetComponent<Tile>().ClearTile();
            }
        }
    }

}