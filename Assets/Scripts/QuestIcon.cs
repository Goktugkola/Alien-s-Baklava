using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationIcon : MonoBehaviour
{
    private GameObject player;
    private GameObject radar;
    private Vector2 direction;
    public GameObject Destination;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        radar = GameObject.FindGameObjectWithTag("Radar");

    }

    // Update is called once per frame
    void Update()
    {
        direction = Destination.transform.position - player.transform.position;
        direction.Normalize();
        transform.position = radar.transform.position + new Vector3(direction.x, direction.y, 0) * 180;
    }
}
