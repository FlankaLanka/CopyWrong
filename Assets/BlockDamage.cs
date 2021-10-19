using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDamage : MonoBehaviour
{
    private void OnEnable()
    {
        //gameObject.transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().shieldActivated = true;
    }


    private void OnDisable()
    {
        //gameObject.transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.transform.parent.gameObject.GetComponent<PlayerMovement>().shieldActivated = false;
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }*/
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision);
            gameObject.SetActive(false);
        }
    }*/
}
