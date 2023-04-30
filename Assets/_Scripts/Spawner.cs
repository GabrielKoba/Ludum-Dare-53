using NaughtyAttributes;
using UnityEngine;
using static Tile;

public class Spawner : MonoBehaviour {

    [Header("Spawner")]
    [SerializeField] private SpriteRenderer iconSpriteRenderer;
    [SerializeField] private Sprite spawnerIcon;
    public TileDirection spawnerAnchor;
    public Tile linkedTile;
    [Space]
    public GameObject itemToSpawn;

    private void OnValidate() {
        UpdateSpawner();
    }
    private void Start() {
        UpdateSpawner();
    }

    private void UpdateSpawner() {
        iconSpriteRenderer.sprite = spawnerIcon;
        iconSpriteRenderer.color = Color.white;

        switch (spawnerAnchor) {
            case TileDirection.None:
                transform.position = new Vector2(linkedTile.transform.position.x, linkedTile.transform.position.y);
                break;
            case TileDirection.Left:
                transform.position = new Vector2(linkedTile.transform.position.x + -linkedTile.neighbouringTilesDistance, linkedTile.transform.position.y);
                break;
            case TileDirection.Right:
                transform.position = new Vector2(linkedTile.transform.position.x + linkedTile.neighbouringTilesDistance, linkedTile.transform.position.y);
                break;
            case TileDirection.Down:
                transform.position = new Vector2(linkedTile.transform.position.x, linkedTile.transform.position.y + -linkedTile.neighbouringTilesDistance);
                break;
            case TileDirection.Up:
                transform.position = new Vector2(linkedTile.transform.position.x, linkedTile.transform.position.y + linkedTile.neighbouringTilesDistance);
                break;
        }
    }
    public void SpawnItem() {
        if (linkedTile.TileEmpty()) {
            Instantiate(itemToSpawn, linkedTile.transform.position, linkedTile.transform.rotation, linkedTile.transform);
            linkedTile.UpdateTile();
        }
    }

}