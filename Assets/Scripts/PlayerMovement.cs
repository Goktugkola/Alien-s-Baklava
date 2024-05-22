using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{
    private const float ROTATION_MULTIPLIER = 45f;
    private const float HOLD_INCREMENT = 0.5f;
    private const float MAX_HOLD = 3f;

    private int screenWidth;
    public int speed;
    public int maxSpeed;
    public PlayerInput playerInput;

    private float hold = 1;
    private Rigidbody2D rb;
    private float movementInput;
    private Quaternion currentRotation;
    private Quaternion targetRotation;

    void Start()
    {
        screenWidth = Screen.width;
        rb = GetComponent<Rigidbody2D>();
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }

    void Update()
    {
        ProcessTouchInput();
        CalculateRotation();
    }

    void LateUpdate()
    {
        ApplyMovement();
    }

    private void ProcessTouchInput()
    {
        if (Touch.activeTouches.Count == 1)
        {
            Vector2 touchPosition = Touch.activeTouches[0].screenPosition;
            movementInput = (touchPosition.x < screenWidth / 2) ? 1f : -1f;
        }
        else
        {
            movementInput = 0;
            hold = 1;
        }
    }

    private void CalculateRotation()
    {
        currentRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0, 0, -Mathf.Abs(360 - transform.eulerAngles.z) - movementInput * ROTATION_MULTIPLIER * hold);
    }

    private void ApplyMovement()
    {
        if (Touch.activeTouches.Count > 0)
        {
            rb.AddForce(transform.up * speed, ForceMode2D.Force);
            if (hold < MAX_HOLD)
                hold += Time.deltaTime * HOLD_INCREMENT;
        }

        float maxAllowedSpeed = (Touch.activeTouches.Count > 1) ? maxSpeed * 2 : maxSpeed;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxAllowedSpeed);
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation,  hold* Time.deltaTime);
    }

    public void over()
    {
        Debug.Log("Game Over");
    }
}
