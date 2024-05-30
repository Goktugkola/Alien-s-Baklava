using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class comics : MonoBehaviour
{
    private int pagenumber = 0;
    private bool isOnCooldown = false;
    private float cooldownTime = 3f;
    [SerializeField] private Animator page1;
    [SerializeField] private Animator page2;
    [SerializeField] private Animator page3;
    void Start()
    {
        page1.SetBool("active", true);
        EnhancedTouchSupport.Enable();
    }
 void Update()
    {
        foreach (UnityEngine.InputSystem.EnhancedTouch.Touch touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
        {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began && !isOnCooldown)
            {
                pagenumber++;
                print(pagenumber);
                if (pagenumber == 1)
                {
                    page2.SetBool("active", true);
                 StartCoroutine(TouchCooldown());
                }
                else if (pagenumber == 2)
                {
                    page3.SetBool("active", true);
                // Add more conditions if you have more pages
                 StartCoroutine(TouchCooldown());
                
                }
               
            }
        }
    }

    private IEnumerator TouchCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }

}
