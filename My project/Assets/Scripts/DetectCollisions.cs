using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Food hitting Animal
        if (collision.gameObject.tag == "Animal" && gameObject.tag == "Food")
        {
            collision.gameObject.GetComponent<AnimalHunger>().feedAnimal(1);
            Destroy(gameObject);
        }

        // Animal hitting Player
        if (collision.gameObject.tag == "Player" && gameObject.tag == "Animal")
        {
            // Animal pushes player with impulse
            collision.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 10, ForceMode.Impulse);
            gameManager.addLives(-1);
        }

    }

}
