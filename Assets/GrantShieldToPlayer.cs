using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrantShieldToPlayer : MonoBehaviour
{

    public AudioClip bushSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(bushSound, Camera.main.transform.position, 0.5f);
            collision.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
