using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float speed = 1;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        // Destroy after two seconds
        Destroy(gameObject, 2);
    }

}
