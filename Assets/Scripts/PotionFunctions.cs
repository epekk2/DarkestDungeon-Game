using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PotionFunctions : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Color redTileColor = Color.red;
    [SerializeField] private Color blueTileColor = Color.blue;
    [SerializeField] private float destructionRadius = 0.5f;
    [SerializeField] private LayerMask destructibleLayers; // Should include both enemy and player layers

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            CheckAndDestroyOnRedTile(player);
        }

        // Check all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            CheckAndDestroyOnRedTile(enemy);
        }
    }

    private void Start()
    {
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }
    }

    private void CheckAndDestroyOnRedTile(GameObject entity)
    {
        Vector3Int entityCell = groundTilemap.WorldToCell(entity.transform.position);
        Color tileColor = groundTilemap.GetColor(entityCell);

        // If the tile is red (allowing for some color variation)
        if (Mathf.Approximately(tileColor.r, redTileColor.r) &&
            Mathf.Approximately(tileColor.g, redTileColor.g) &&
            Mathf.Approximately(tileColor.b, redTileColor.b))
        {
            Destroy(entity);
            string entityType = entity.CompareTag("Player") ? "Player" : "Enemy";
            //Debug.Log($"{entityType} destroyed by red tile!");
        }
    }

    public bool useHealthPotion(Vector3 position)
    {
        // Debug.Log("Health Potion Used!");
        return true;
    }

    public bool useFirePotion(Vector3 position)
    {
        if (groundTilemap == null)
        {
           // Debug.LogError("Missing tilemap reference for Fire Potion!");
            return false;
        }

        Vector3Int targetCell = groundTilemap.WorldToCell(position);

        Vector3Int[] cellsToColor = new Vector3Int[]
        {
            targetCell, // Center
            targetCell + new Vector3Int(1, 0, 0),  // Right
            targetCell + new Vector3Int(-1, 0, 0), // Left
            targetCell + new Vector3Int(0, 1, 0),  // Up
            targetCell + new Vector3Int(0, -1, 0), // Down
            targetCell + new Vector3Int(1, 1, 0),  // Upper-right
            targetCell + new Vector3Int(-1, 1, 0), // Upper-left
            targetCell + new Vector3Int(1, -1, 0), // Lower-right
            targetCell + new Vector3Int(-1, -1, 0) // Lower-left
        };

        bool effectApplied = false;
        foreach (Vector3Int cell in cellsToColor)
        {
            TileBase tile = groundTilemap.GetTile(cell);
            if (tile != null)
            {
                // Change tile color
                groundTilemap.SetTileFlags(cell, TileFlags.None);
                groundTilemap.SetColor(cell, redTileColor);

                // Get the world position of the cell's center
                Vector3 worldPosition = groundTilemap.GetCellCenterWorld(cell);

                // Find all colliders in the destruction radius
                Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPosition, destructionRadius, destructibleLayers);

                // Destroy each entity found (enemy or player)
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("Enemy") || collider.CompareTag("Player"))
                    {
                        Destroy(collider.gameObject);
                    }
                }
                effectApplied = true;
            }
        }

        if (effectApplied)
        {
           // Debug.Log("Fire Potion used - surrounding tiles turned red and entities destroyed");
        }
        return true;
    }

    public bool useWaterPotion(Vector3 position)
    {
        if (groundTilemap == null)
        {
            // Debug.LogError("Missing tilemap reference for Fire Potion!");
            return false;
        }

        Vector3Int targetCell = groundTilemap.WorldToCell(position);

        Vector3Int[] cellsToColor = new Vector3Int[]
        {
            targetCell, // Center
            targetCell + new Vector3Int(1, 0, 0),  // Right
            targetCell + new Vector3Int(-1, 0, 0), // Left
            targetCell + new Vector3Int(0, 1, 0),  // Up
            targetCell + new Vector3Int(0, -1, 0), // Down
            targetCell + new Vector3Int(1, 1, 0),  // Upper-right
            targetCell + new Vector3Int(-1, 1, 0), // Upper-left
            targetCell + new Vector3Int(1, -1, 0), // Lower-right
            targetCell + new Vector3Int(-1, -1, 0) // Lower-left
        };

        bool effectApplied = false;
        foreach (Vector3Int cell in cellsToColor)
        {
            TileBase tile = groundTilemap.GetTile(cell);
            if (tile != null)
            {
                // Change tile color
                groundTilemap.SetTileFlags(cell, TileFlags.None);
                groundTilemap.SetColor(cell, blueTileColor);

                // Get the world position of the cell's center
                Vector3 worldPosition = groundTilemap.GetCellCenterWorld(cell);
            }
        }

        return true;
    }

    // Optional: Visualize the destruction radius in the editor
    private void OnDrawGizmosSelected()
    {
        if (groundTilemap != null)
        {
            Vector3Int playerCell = groundTilemap.WorldToCell(transform.position);
            Vector3Int[] cells = new Vector3Int[]
            {
                playerCell,
                playerCell + new Vector3Int(1, 0, 0),
                playerCell + new Vector3Int(-1, 0, 0),
                playerCell + new Vector3Int(0, 1, 0),
                playerCell + new Vector3Int(0, -1, 0),
                playerCell + new Vector3Int(1, 1, 0),
                playerCell + new Vector3Int(-1, 1, 0),
                playerCell + new Vector3Int(1, -1, 0),
                playerCell + new Vector3Int(-1, -1, 0)
            };

            Gizmos.color = Color.red;
            foreach (Vector3Int cell in cells)
            {
                Vector3 worldPos = groundTilemap.GetCellCenterWorld(cell);
                Gizmos.DrawWireSphere(worldPos, destructionRadius);
            }
        }
    }
}