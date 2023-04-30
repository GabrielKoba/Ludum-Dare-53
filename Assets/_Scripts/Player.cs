using UnityEngine;

public class Player : MonoBehaviour {

    private InputManager input;

    [Header("Player Settings")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _levelMask;
    [SerializeField] private LayerMask _itemMask;
    private RaycastHit2D mouseHit;
    private bool clickedLeft;

    private void Start() {
        input = InputManager.Instance;
    }
    private void Update() {
        ReadFlags();
    }

    private void ReadFlags() {
        if (input.mouseLeftClick) {
            mouseHit = Physics2D.GetRayIntersection(_playerCamera.ScreenPointToRay(input.mouse), Mathf.Infinity, _levelMask);

            if (mouseHit.collider != null) {
                if (mouseHit.collider.gameObject.tag == "Spawner") {
                    Spawner spawnerHit = mouseHit.collider.GetComponent<Spawner>();
                    spawnerHit.SpawnItem();
                }
            }
        }
        else if (input.mouseLeftHold) {
            mouseHit = Physics2D.GetRayIntersection(_playerCamera.ScreenPointToRay(input.mouse), Mathf.Infinity, _levelMask);

            if (mouseHit.collider != null) {
                if (mouseHit.collider.gameObject.tag == "Tile") {
                    Tile tileHit = mouseHit.collider.GetComponent<Tile>();
                    if (input.movement != Vector2.zero) tileHit.ChangeTileDirection(input.movement);
                }
            }
        }
        else if (input.mouseRight) {
            if (input.shift) {
                mouseHit = Physics2D.GetRayIntersection(_playerCamera.ScreenPointToRay(input.mouse), Mathf.Infinity, _levelMask);

                if (mouseHit.collider != null && mouseHit.collider.gameObject.tag == "Tile") {
                    Tile tileHit = mouseHit.collider.GetComponent<Tile>();
                    tileHit.ClearTileDirection();
                }
            }
            else {
                mouseHit = Physics2D.GetRayIntersection(_playerCamera.ScreenPointToRay(input.mouse), Mathf.Infinity, _itemMask);

                if (mouseHit.collider != null && mouseHit.collider.gameObject.tag == "Item") {
                    Destroy(mouseHit.collider.gameObject);
                }
            }
        }
    }
}