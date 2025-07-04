using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
public class LoadZoneGizmo : MonoBehaviour
{
    [Header("Player & Map Data")]
    public Transform player;
    public EnviromentGenerator mapGenerator;

    private float spawnDistance;
    private float despawnDistance;
    private float gridSpacing;
    private int gridWidth;
    private int gridHeight;

    [Header("Grid Cell Debug")]
    public bool drawGridCells = true;
    public bool drawIslandPositions = true;
    public Color gridColor = new Color(0f, 1f, 1f, 0.2f);
    public Color islandPointColor = Color.yellow;

    [Header("Runtime Island Data")]
    public List<Vector3> islandPositions = new(); // Optional: fill from runtime to visualize actual island data

    private void OnValidate()
    {
        if (mapGenerator != null)
        {
            spawnDistance = mapGenerator.spawnDistance;
            despawnDistance = mapGenerator.despawnDistance;
            gridSpacing = mapGenerator.spacing;
            gridWidth = mapGenerator.gridWidth;
            gridHeight = mapGenerator.gridHeight;
        }
    }

    private void OnDrawGizmos()
    {
        if (player == null || mapGenerator == null) return;

        // Draw spawn and despawn zones
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(player.position, spawnDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, despawnDistance);

        // Draw grid
        if (drawGridCells)
        {
            Gizmos.color = gridColor;

            for (int x = -gridWidth / 2; x < gridWidth / 2; x++)
            {
                for (int y = -gridHeight / 2; y < gridHeight / 2; y++)
                {
                    Vector3 center = new Vector3(x * gridSpacing, y * gridSpacing, 0f);
                    Gizmos.DrawWireCube(center, Vector3.one * gridSpacing);
                }
            }
        }

        // Draw island spawn points (if provided at runtime)
        if (drawIslandPositions && islandPositions != null)
        {
            Gizmos.color = islandPointColor;
            foreach (var pos in islandPositions)
            {
                Gizmos.DrawSphere(pos, 0.5f);
            }
        }
    }
}
