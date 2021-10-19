using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    static AudioSettings bgm;
    // Start is called before the first frame update
    void Awake()
    {
        if(bgm != null && bgm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            bgm = this;
            DontDestroyOnLoad(gameObject);
        }
    }



}
