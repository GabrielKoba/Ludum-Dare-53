using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor;

[ExecuteInEditMode]
public class Grid : MonoBehaviour {

    [SerializeField] private float spacing = 1f;
    [SerializeField] private int columns = 5;
    [SerializeField] private List<Tile> tiles = new List<Tile>();

    private int tilesAddedToCurrentLine = -1;
    private float currentVerticalSpacing = 0;

    private void OnValidate() {
        UpdateGrid();
    }
    private void Awake() {
        UpdateGrid();
    }

    [Button("Update Grid")]
    private void UpdateGrid() {
        tiles.Clear();
        tilesAddedToCurrentLine = -1;
        currentVerticalSpacing = 0;

        for (int i = 0; i < transform.childCount; i++) {
            tiles.Add(transform.GetChild(i).gameObject.GetComponent<Tile>());
        }
        foreach (Tile tile in tiles) {
            tile.transform.GetComponent<BoxCollider2D>().size = new Vector2(spacing, spacing);
            tile.transform.localPosition = Vector3.zero;
            tile.neighbouringTilesDistance = spacing;

            if (tilesAddedToCurrentLine < columns - 1) {
                tilesAddedToCurrentLine++;
            }
            else {
                tilesAddedToCurrentLine = 0;
                currentVerticalSpacing = currentVerticalSpacing + spacing;
            }

            tile.transform.localPosition = new Vector3(tile.transform.position.x + spacing * tilesAddedToCurrentLine, tile.transform.position.y - currentVerticalSpacing, transform.localPosition.z);
        }
    }

}