using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AlienBaklava/MapGenerationConfig")]
public class MapGenerationConfig : ScriptableObject
{
    public List<GameObject> islandPrefabs;
    public List<GameObject> policePrefabs;

    public int numberOfIslands = 50;
    public Vector2 islandSpacingRange = new Vector2(10, 20);
    [Header("Police Settings")]
    public bool spawnPolice = true;
    public float policeSpawnChance = 0.3f;
    public float policeMinDistance = 5f;
    public int maxActivePolice = 10;
}
