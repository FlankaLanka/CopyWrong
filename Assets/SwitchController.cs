using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    public List<GameObject> blockades;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            for(int i = 0; i < blockades.Count; i++)
            {
                blockades[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            for (int i = 0; i < blockades.Count; i++)
            {
                blockades[i].gameObject.SetActive(true);
            }
        }
    }
}
