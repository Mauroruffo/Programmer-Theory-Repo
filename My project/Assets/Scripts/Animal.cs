using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public float speed = 1.0f;
    GameObject playerRB;
    protected GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetDifficulty();
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

    public void SetDifficulty()
    {
        // Set animal's speed depending on Player's score;
        int difficulty = gameManager.score / 100;
        speed += difficulty;
    }

}
