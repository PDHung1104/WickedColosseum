using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI tmp;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = "Score: " + ScoreScript.score.ToString() + "\n" + "High Score: " + PlayerPrefs.GetInt("highScore", 0);
    }
}
