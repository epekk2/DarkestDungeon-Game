using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class EnemyPatroll : MonoBehaviour
{
    public Transform[] waypoints;  // Manually assigned waypoints
    public float speed = 2f;       // Speed of the enemy

    private int currentWaypointIndex = 0; // To track the current waypoint

    [SerializeField] private Tilemap groundTilemap; // Reference to the ground tilemap

    void Update()
    {
        MoveToNextWaypoint();

        if (IsOnRedTile())
        {
            Destroy(gameObject);  // Destroy the enemy
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        SceneManager.LoadScene(1);//Index for Level select
    }

    void MoveToNextWaypoint()
    {
        // Move the enemy towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Check if the enemy has reached the current waypoint
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Switch to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    bool IsOnRedTile()
    {
        // Get the tile position under the enemy
        Vector3Int tilePosition = groundTilemap.WorldToCell(transform.position);

        // Get the tile at the enemy's current position
        TileBase tile = groundTilemap.GetTile(tilePosition);

        // Check if the tile is red
        if (tile != null && groundTilemap.GetColor(tilePosition) == Color.red)
        {
            return true;  // Enemy is on a red tile
        }

        return false;
    }

    // Draws lines between waypoints in the Scene view
    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2)
            return;

        // Set the Gizmo color
        Gizmos.color = Color.red;

        // Loop through all the waypoints and draw lines between them
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }

        // Optionally, draw a line from the last waypoint back to the first to close the loop
        Gizmos.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);
    }
}
