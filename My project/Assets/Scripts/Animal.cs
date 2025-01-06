using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.0f;
    GameObject playerRB;

    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    public void Spawn()
    {
        // Find Player
        playerRB = GameObject.Find("Player");
    }

    public void FollowPlayer()
    {
        // Get Player's position
        Vector3 vector3 = playerRB.transform.position;
        // Make forward direction point to Player
        transform.LookAt(new Vector3(vector3.x, gameObject.transform.position.y, vector3.z));
        // Move animal forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

}
