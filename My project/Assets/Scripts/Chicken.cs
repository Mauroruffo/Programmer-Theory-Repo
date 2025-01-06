using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class Chicken : Animal
{
 
    // Start is called before the first frame update
    void Start()
    {
        speed *= 2;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
}
