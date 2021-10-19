using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHighScores : MonoBehaviour
{
    public void deletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
