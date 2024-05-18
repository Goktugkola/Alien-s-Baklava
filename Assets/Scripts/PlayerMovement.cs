using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using System.Linq;
public class PlayerMovement : MonoBehaviour
{
    private int screenWidth = Screen.width;
    public int speed;
    public int maxSpeed;
    public PlayerInput playerInput;
    private float hold = 1;
    private Rigidbody2D rb;
    private float movementInput;
    private Quaternion currentRotation;
    private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }
    void Update()
    {
        Finger activefinger = Touch.activeFingers[0];
        Vector2 touchPosition = activefinger.screenPosition;
        float movementInput = touchPosition.x;
        if (Touch.activeTouches.Count > 1) { movementInput = 0; }
        else if (Touch.activeTouches.Count == 1)
        {
            movementInput = (movementInput < screenWidth / 2) ? 1f : -1f;
        }
        else
        {
            movementInput = 0;
            hold = 1;
        }


        currentRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0, 0, -Mathf.Abs(360 - transform.eulerAngles.z) - movementInput * 45 * hold);
        // Slerp between the current rotation and the target rotation


    }
    void LateUpdate()
    {
        if (Touch.activeTouches.Count > 0)
        {
            rb.AddForce(transform.up * speed, ForceMode2D.Force);
            if (hold < 3)
                hold += Time.deltaTime / 2;
        }
        if (Touch.activeTouches.Count > 1)
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed * 2);
        else
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime);
    }
    public void over()
    {
        Debug.Log("Game Over");
    }
}
