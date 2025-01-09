using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTag : MonoBehaviour
{
    GameObject player;
    public TextMeshProUGUI playerTag;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerTag.text = DataHandler.Instance.PlayerName;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Stay on Player's position
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 1.5f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
