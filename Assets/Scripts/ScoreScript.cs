using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static int score;
    private void Start()
    {
        score = 0;
    }

    public static void SetHighScore()
    {
        PlayerPrefs.SetInt("highScore", score);
    }

}
