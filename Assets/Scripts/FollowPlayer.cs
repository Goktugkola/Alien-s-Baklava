using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D playerrb;
    private Vector2 playerdir;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerrb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        playerdir = playerrb.velocity.normalized;
        Vector2 movement = new Vector2(playerdir.x *10 +  player.transform.position.x,player.transform.position.y + playerdir.y *10) ;
        Vector3 targetposition = new Vector3(movement.x,movement.y,transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetposition, 0.02f);
    }
}
