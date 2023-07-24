using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    // Start the pve by going into the fight scene
    public void pve() {
        SceneManager.LoadScene("PVE");
    }   
    // Start the pvp by going into character select scene
    public void pvp() {
        SceneManager.LoadScene("PvP");
    }
    public void back() {
        SceneManager.LoadScene("MainMenu");
    }

    public void charSelect()
    {
        SceneManager.LoadScene("CharSelect");
    }

}
