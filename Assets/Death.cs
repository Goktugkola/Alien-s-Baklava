using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    void death()
    {
        // Destroy the object that this script is attached to
        Destroy(gameObject);
    }
}
