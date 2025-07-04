using UnityEngine;

public class Poolable : MonoBehaviour
{
    public GameObject PrefabSource { get; set; }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Activate(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }
}