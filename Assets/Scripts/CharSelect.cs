using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelect : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static void SelectKnight()
    {
        PlayerPrefs.SetInt("SelectCharacter", 0);
        SceneManager.LoadScene("PVE");
    }
    public static void SelectMage()
    {
        PlayerPrefs.SetInt("SelectCharacter", 1);
        SceneManager.LoadScene("PVE");
    }
}
