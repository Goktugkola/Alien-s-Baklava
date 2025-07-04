using UnityEngine;

[System.Serializable]
public class IslandData
{
     public Vector2Int gridCoord;
    public Vector3 worldPosition;
    public int prefabIndex;

    public IslandData(Vector2Int gridCoord, Vector3 worldPosition, int prefabIndex)
    {
        this.gridCoord = gridCoord;
        this.worldPosition = worldPosition;
        this.prefabIndex = prefabIndex;
    }

}
