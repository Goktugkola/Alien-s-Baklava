using System.Collections.Generic;
using UnityEngine;

public class EnviromentGenerator : MonoBehaviour
{
    public MapGenerationConfig config;
    public Transform player;
    public float spacing = 15f;
    public Transform PlanetContainer;
    public Transform PoliceContainer;
    public float spawnDistance = 50f;
    public float despawnDistance = 60f;

    [Header("Spawn Control")]
    public float minimumDistanceFromPlayer = 20f;

    public int gridWidth = 10;
    public int gridHeight = 5;

    private List<IslandInstance> islandInstances = new();
    private IslandPool pool;
    [SerializeField] int policeMinDistance = 5;
    private List<GameObject> activePolice = new();
    private Vector2Int GetPlayerGridPosition()
    {
        Vector3 pos = player.position;
        int x = Mathf.RoundToInt(pos.x / spacing);
        int y = Mathf.RoundToInt(pos.y / spacing);
        return new Vector2Int(x, y);
    }
    private void Start()
    {
        pool = GetComponent<IslandPool>();
        pool.PreloadFromScene(); // Grab islands already in scene
        GenerateGrid();
    }

    private void Update()
    {
        foreach (var island in islandInstances)
        {
            float dist = Vector3.Distance(player.position, island.data.worldPosition);
            if (dist < spawnDistance && island.instance == null)
                SpawnIsland(island);
            else if (dist > despawnDistance && island.instance != null)
                DespawnIsland(island);
        }
    }

    private void GenerateGrid()
    {
          Vector2Int playerGrid = GetPlayerGridPosition();

        for (int x = -gridWidth / 2; x < gridWidth / 2; x++)
        {
            for (int y = -gridHeight / 2; y < gridHeight / 2; y++)
            {
                Vector2Int gridCoord = new Vector2Int(x, y);
                if (gridCoord == playerGrid)
                    continue; 
                // Define the center of this cell in world space
                Vector3 cellCenter = new Vector3(x * spacing, y * spacing, 0);

                // Add a small random offset within half-spacing
                float maxOffset = spacing * 0.4f; // Keep margin between cells
                Vector3 randomOffset = new Vector3(
                    Random.Range(-maxOffset, maxOffset),
                    Random.Range(-maxOffset, maxOffset),
                    0
                );

                Vector3 finalPosition = cellCenter + randomOffset;

                int prefabIndex = Random.Range(0, config.islandPrefabs.Count);
                var data = new IslandData(gridCoord, finalPosition, prefabIndex);
                islandInstances.Add(new IslandInstance(data));
            }
        }
    }


    private void SpawnIsland(IslandInstance island)
    {
        GameObject prefab = config.islandPrefabs[island.data.prefabIndex];
        island.instance = pool.GetFromPool(prefab);
        island.instance.GetComponent<Poolable>().Activate(island.data.worldPosition);
        island.instance.transform.SetParent(PlanetContainer);

        if (config.spawnPolice && Random.value < config.policeSpawnChance)
        {
            // Check max count
            GameObject police = null;
            if (activePolice.Count < config.maxActivePolice)
            {
                GameObject policePrefab = config.policePrefabs[Random.Range(0, config.policePrefabs.Count)];

                // Direction & distance from island
                Vector2 randomDir = Random.insideUnitCircle.normalized;
                Vector3 policePos = island.data.worldPosition + (Vector3)(randomDir * config.policeMinDistance);
                float angle = Mathf.Atan2(randomDir.y, randomDir.x) * Mathf.Rad2Deg;
                Quaternion policeRotation = Quaternion.Euler(0f, 0f, angle + 180f);

                // Get from pool
                police = pool.GetFromPool(policePrefab);
                police.GetComponent<Poolable>().Activate(policePos);
                police.transform.rotation = policeRotation;
                police.transform.SetParent(island.instance.transform);

                activePolice.Add(police); // Track it
            }
            else
            {
                Debug.Log("Police limit reached. Skipping new police spawn.");
            }
        }
    }

    private void DespawnIsland(IslandInstance island)
    {
        foreach (Transform child in island.instance.transform)
        {
            if (child.TryGetComponent(out Poolable p))
            {
                // Check if this is a police and remove from active list
                if (activePolice.Contains(child.gameObject))
                    activePolice.Remove(child.gameObject);

                pool.ReturnToPool(child.gameObject);
            }
        }

        pool.ReturnToPool(island.instance);
        island.instance = null;
    }
}
