using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceProvider : MonoBehaviour
{
    public static Player Player { get; set; }

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
    }
}
