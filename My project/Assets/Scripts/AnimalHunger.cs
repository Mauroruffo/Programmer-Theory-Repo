using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalHunger : MonoBehaviour
{
    public Slider hungerSlider;
    public int amountToBeFed;
    private int currentFedAmount;
    private GameManager gameManager;

    public GameObject particleExplosion;
    public GameObject particleEat;
    public float eatOffSet = 0;

    // Start is called before the first frame update
    void Start()
    {
        hungerSlider.maxValue = amountToBeFed;
        hungerSlider.value = 0;
        hungerSlider.fillRect.gameObject.SetActive(false);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void feedAnimal(int amount)
    {
        currentFedAmount += amount;
        hungerSlider.fillRect.gameObject.SetActive(true);
        AnimalEat();
        hungerSlider.value = currentFedAmount;

        if (currentFedAmount >= amountToBeFed)
        {
            gameManager.addScore(5);
            AnimalFull();
            Destroy(gameObject, 0.01f);
        }
    }

    void AnimalFull()
    {
        GameObject explosionInstance = Instantiate(particleExplosion, gameObject.transform.position, gameObject.transform.rotation);
        explosionInstance.GetComponent<ParticleSystem>().Play();
        Destroy(explosionInstance, 2);
    }

    void AnimalEat()
    {
        GameObject eatInstance = Instantiate(particleEat, transform.position + transform.forward * eatOffSet, transform.rotation);
        eatInstance.GetComponent<ParticleSystem>().Play();
        Destroy(eatInstance, 0.2f);
    }

}
