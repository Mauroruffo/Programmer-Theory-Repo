using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : Animal
{
    // Start is called before the first frame update
    void Start()
    {
        // Horse's speed
        speed += 3;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetDifficulty();
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
}
