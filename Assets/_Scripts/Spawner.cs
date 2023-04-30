using NaughtyAttributes;
using UnityEngine;
using static Tile;

public class Spawner : MonoBehaviour {

    [Header("Spawner")]
    [SerializeField] private SpriteRenderer iconSpriteRenderer;
    [SerializeField] private Sprite spawnerIcon;
    public TileDirection spawnerAnchor;
    public Tile tileToSpawnIn;
    [Space]
    public FoodData foodToSpawn;

    private void OnValidate() {
        UpdateSpawner();
    }
    private void Start() {
        UpdateSpawner();
    }

    private void UpdateSpawner() {
        iconSpriteRenderer.sprite = spawnerIcon;
        iconSpriteRenderer.color = Color.white;

        if (tileToSpawnIn) {
            switch (spawnerAnchor) {
                case TileDirection.None:
                    transform.position = new Vector2(tileToSpawnIn.transform.position.x, tileToSpawnIn.transform.position.y);
                    break;
                case TileDirection.Left:
                    transform.position = new Vector2(tileToSpawnIn.transform.position.x + -tileToSpawnIn.neighbouringTilesDistance, tileToSpawnIn.transform.position.y);
                    break;
                case TileDirection.Right:
                    transform.position = new Vector2(tileToSpawnIn.transform.position.x + tileToSpawnIn.neighbouringTilesDistance, tileToSpawnIn.transform.position.y);
                    break;
                case TileDirection.Down:
                    transform.position = new Vector2(tileToSpawnIn.transform.position.x, tileToSpawnIn.transform.position.y + -tileToSpawnIn.neighbouringTilesDistance);
                    break;
                case TileDirection.Up:
                    transform.position = new Vector2(tileToSpawnIn.transform.position.x, tileToSpawnIn.transform.position.y + tileToSpawnIn.neighbouringTilesDistance);
                    break;
            }
        }
    }
    public void SpawnItem() {
        if (tileToSpawnIn.TileEmpty()) {
            GameObject spawnedTile = Instantiate(foodToSpawn.prefab, tileToSpawnIn.transform.position, tileToSpawnIn.transform.rotation, transform);

            spawnedTile.transform.GetComponent<Item>().currentTile = tileToSpawnIn;
            spawnedTile.transform.GetComponent<Item>().UpdateItem();
            tileToSpawnIn.UpdateTile();
        }
    }

}