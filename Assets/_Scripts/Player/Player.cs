using FMODUnity;
using UnityEngine;

public class Player : MonoBehaviour {

    private InputManager input;

    [Header("Player Settings")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _levelMask;
    [SerializeField] private LayerMask _itemMask;
    private RaycastHit2D mouseHit;
    private bool clickedLeft;

    [SerializeField] EventReference clickOnSpawner;
    [SerializeField] EventReference conveyorRemove;
    [SerializeField] EventReference removeFood;

    private void Start() {
        input = InputManager.Instance;
    }
    private void Update() {
        ReadFlags();
    }

    private void ReadFlags() {
        if (input.mouseLeftClick) {
            mouseHit = Physics2D.GetRayIntersection(_playerCamera.ScreenPointToRay(input.mouse), Mathf.Infinity, _itemMask);

            if (mouseHit.collider != null && mouseHit.collider.gameObject.tag == "Item") {
                Destroy(mouseHit.collider.gameObject);
                FMODUnity.RuntimeManager.PlayOneShot(removeFood);
            }

            mouseHit = Physics2D.GetRayIntersection(_playerCamera.ScreenPointToRay(input.mouse), Mathf.Infinity, _levelMask);

            if (mouseHit.collider != null && mouseHit.collider.gameObject.tag == "Spawner") {
                Spawner spawnerHit = mouseHit.collider.GetComponent<Spawner>();
                spawnerHit.SpawnItem();
                FMODUnity.RuntimeManager.PlayOneShot(clickOnSpawner);
            }
        }
        else if (input.movement != Vector2.zero && input.mouseLeftHold) {
            mouseHit = Physics2D.GetRayIntersection(_playerCamera.ScreenPointToRay(input.mouse), Mathf.Infinity, _levelMask);

            if (mouseHit.collider != null) {
                if (mouseHit.collider.gameObject.tag == "Tile") {
                    Tile tileHit = mouseHit.collider.GetComponent<Tile>();
                    tileHit.ChangeTileDirection(input.movement);
                }
            }
        }
        else if (input.mouseRight) {
            mouseHit = Physics2D.GetRayIntersection(_playerCamera.ScreenPointToRay(input.mouse), Mathf.Infinity, _levelMask);

            if (mouseHit.collider != null && mouseHit.collider.gameObject.tag == "Tile") {
                Tile tileHit = mouseHit.collider.GetComponent<Tile>();
                tileHit.ClearTileDirection();
                FMODUnity.RuntimeManager.PlayOneShot(conveyorRemove);
            }
        }
    }
}