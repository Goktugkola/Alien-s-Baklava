using System.Collections.Generic;
using UnityEngine;

public class IslandPool : MonoBehaviour
{
    [SerializeField] Dictionary<GameObject, Queue<GameObject>> pool = new();

public void PreloadFromScene()
{
    Poolable[] found = FindObjectsOfType<Poolable>(true); // include inactive
    foreach (var poolable in found)
    {
        if (poolable.PrefabSource == null)
        {
            Debug.LogWarning($"{poolable.name} has no PrefabSource assigned — cannot pool it.");
            continue;
        }

        if (!pool.ContainsKey(poolable.PrefabSource))
            pool[poolable.PrefabSource] = new Queue<GameObject>();

        poolable.Deactivate();
        pool[poolable.PrefabSource].Enqueue(poolable.gameObject);
    }
}

    public GameObject GetFromPool(GameObject prefab)
    {
        if (!pool.ContainsKey(prefab))
            pool[prefab] = new Queue<GameObject>();

        if (pool[prefab].Count > 0)
        {
            var obj = pool[prefab].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        GameObject newObj = Instantiate(prefab, transform);
        newObj.GetComponent<Poolable>().PrefabSource = prefab;
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.GetComponent<Poolable>()?.Deactivate();
        if (obj.TryGetComponent(out Poolable poolable))
        {
            if (!pool.ContainsKey(poolable.PrefabSource))
                pool[poolable.PrefabSource] = new Queue<GameObject>();

            pool[poolable.PrefabSource].Enqueue(obj);
        }
    }
}
