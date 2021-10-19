using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPlayerMovements : MonoBehaviour
{
    private GameObject player;
    private int i = 0;
    private Rigidbody2D rb;
    //public List<Vector2> enemyMovements;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //enemyMovements = player.GetComponent<PlayerMovement>().movements;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(i < player.GetComponent<PlayerMovement>().movements.Count)
        {
            //controls clone's movements and directions they face
            rb.transform.position = player.GetComponent<PlayerMovement>().movements[i];
            transform.eulerAngles = player.GetComponent<PlayerMovement>().directionsFacing[i];
            i++;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
