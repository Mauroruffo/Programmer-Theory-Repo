using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy if it falls below the ground 
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
