using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera cam;
    public Vector3 mousePos;
    public GameObject food;
    public float speed;
    private float xRange = 22.0f;
    private float yRange = 10.0f;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.paused)
        {
            Aim();

            Fire();

            KeepPlayerInbounds();

            PlayerMovement();
        }
    }

    void Aim()
    {
        // Look at mouse
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.LookAt(new Vector3(mousePos.x, gameObject.transform.position.y, mousePos.z), Vector3.up);
    }

    void Fire()
    {
        // Fire food at mouse
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(food, transform.position + transform.forward * 0.5f, transform.rotation);
        }
    }

    void PlayerMovement()
    {
        // Player movement
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime, Space.World);
        }
    }

    void KeepPlayerInbounds()
    {
        // Keep player inside camera
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < -yRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -yRange);
        }

        if (transform.position.z > yRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, yRange);
        }
    }

}
