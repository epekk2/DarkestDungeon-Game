using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Red Tile", menuName = "Tiles/Red Tile")]
public class redTile : Tile
{
    public GameObject tilePrefab; // Prefab to spawn for the tile

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
       // base.RefreshTile(position, tilemap);

        // Get the Tilemap component from the ITilemap interface
       // Tilemap map = tilemap as Tilemap;
       /*
        // Ensure the Tilemap component exists
        if (map != null)
        {
            // Check if the collider already exists
            if (map.GetComponent<TilemapCollider2D>() == null)
            {
                //map.gameObject.AddComponent<TilemapCollider2D>();
            }

            // Optionally, you can instantiate a prefab with a collider
            if (tilePrefab != null)
            {
               //GameObject tileObject = GameObject.Instantiate(tilePrefab, map.GetCellCenterWorld(position), Quaternion.identity);
               // tileObject.layer = LayerMask.NameToLayer("Default"); // Set to appropriate layer
            }
        }
       */
    }
}
