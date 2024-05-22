using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapPrefab; // Prefab of your map
    public GameObject player; // Reference to your player
    public float distanceFromBorder = 5f; // Distance from border to trigger generation
    public float maxDistance = 30f; // Maximum distance a map can be from the current map before being removed

    private Vector2 mapSize; // Store the map size
    private GameObject currentMap;
    private bool isGenerating = false; // Flag to track if generation is in progress
    private Dictionary<Vector2, GameObject> maps = new Dictionary<Vector2, GameObject>(); // Dictionary to store maps by position
    private int mapCounter = 0; // Counter to ensure unique map names

    void Start()
    {
        // Initialize the first map and its properties
        currentMap = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity);
        currentMap.name = "Map_" + mapCounter++;
        mapSize = mapPrefab.GetComponent<SpriteRenderer>().bounds.size;
        maps.Add(Vector2.zero, currentMap); // Add the initial map to the dictionary
    }

    void Update()
    {
        // Check if the player is near the border and generation is not already in progress
        if (!isGenerating && IsPlayerNearBorder())
        {
            isGenerating = true;
            GenerateSurroundingMaps();
            RemoveOldMaps();
            isGenerating = false;
        }

        // Update the current map reference
        currentMap = GetCurrentMapPlayerIsIn();
    }

    GameObject GetCurrentMapPlayerIsIn()
    {
        Vector3 playerPos = player.transform.position;
        Vector2 playerGridPos = new Vector2(
            Mathf.Floor(playerPos.x / mapSize.x),
            Mathf.Floor(playerPos.y / mapSize.y)
        );

        if (maps.TryGetValue(playerGridPos, out GameObject map))
        {
            return map;
        }

        // If player isn't in any map, return the most recently generated one
        return currentMap;
    }

    bool IsPlayerNearBorder()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 mapPos = currentMap.transform.position;

        // Calculate the border boundaries
        float leftBorder = mapPos.x - mapSize.x / 2 + distanceFromBorder;
        float rightBorder = mapPos.x + mapSize.x / 2 - distanceFromBorder;
        float bottomBorder = mapPos.y - mapSize.y / 2 + distanceFromBorder;
        float topBorder = mapPos.y + mapSize.y / 2 - distanceFromBorder;

        // Check if player is within the distanceFromBorder range of any border
        return playerPos.x < leftBorder || playerPos.x > rightBorder ||
               playerPos.y < bottomBorder || playerPos.y > topBorder;
    }

    bool IsMapAtPosition(Vector3 position)
    {
        Vector2 gridPos = new Vector2(
            Mathf.Floor(position.x / mapSize.x),
            Mathf.Floor(position.y / mapSize.y)
        );

        return maps.ContainsKey(gridPos);
    }

    void GenerateSurroundingMaps()
    {
        Vector3[] directions = new Vector3[]
        {
            new Vector3(-1, 1, 0),  // Top Left
            new Vector3(0, 1, 0),   // Top
            new Vector3(1, 1, 0),   // Top Right
            new Vector3(-1, 0, 0),  // Left
            new Vector3(1, 0, 0),   // Right
            new Vector3(-1, -1, 0), // Bottom Left
            new Vector3(0, -1, 0),  // Bottom
            new Vector3(1, -1, 0)   // Bottom Right
        };

        Vector3 mapPos = currentMap.transform.position;

        foreach (Vector3 direction in directions)
        {
            Vector3 newMapPos = mapPos + new Vector3(
                direction.x * mapSize.x,
                direction.y * mapSize.y,
                0);

            // Check if there's already a map at the new position
            if (!IsMapAtPosition(newMapPos))
            {
                GameObject newMap = Instantiate(mapPrefab, newMapPos, Quaternion.identity);
                newMap.name = "Map_" + mapCounter++;
                maps.Add(new Vector2(
                    Mathf.Floor(newMapPos.x / mapSize.x),
                    Mathf.Floor(newMapPos.y / mapSize.y)
                ), newMap);
            }
        }
    }

    void RemoveOldMaps()
    {
        Vector3 currentMapPos = currentMap.transform.position;
        Vector2 currentGridPos = new Vector2(
            Mathf.Floor(currentMapPos.x / mapSize.x),
            Mathf.Floor(currentMapPos.y / mapSize.y)
        );

        List<Vector2> mapsToRemove = new List<Vector2>();

        foreach (KeyValuePair<Vector2, GameObject> kvp in maps)
        {
            if (Vector2.Distance(kvp.Key, currentGridPos) * mapSize.x > maxDistance)
            {
                mapsToRemove.Add(kvp.Key);
            }
        }

        foreach (Vector2 gridPos in mapsToRemove)
        {
            Destroy(maps[gridPos]);
            maps.Remove(gridPos);
        }
    }
}
