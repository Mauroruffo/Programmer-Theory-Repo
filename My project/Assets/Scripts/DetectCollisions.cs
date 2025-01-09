using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject particlePush;

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
            gameManager.Pushed();
            // Animal pushes player with impulse
            collision.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 10, ForceMode.Impulse);
            AnimalParticlePush();
            gameManager.addLives(-1);
        }
    }
    
    void AnimalParticlePush()
    {
        GameObject pushInstance = Instantiate(particlePush, transform.position + transform.forward * 0.5f, transform.rotation);
        pushInstance.GetComponent<ParticleSystem>().Play();
        Destroy(pushInstance, 4);
    }

}
