using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Exit the game
    public void ExitButton() {
        Application.Quit();
        Debug.Log("See you next time!");
    }

    // Start the game by going into the gamemode scene
    public void StartPlaying() {
        SceneManager.LoadScene("GameMode");
    }
}
