using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject manager;

    public void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EditKnM()
    {
        InputManager.editingKeyboardControls = true;
    }

    public void EditGamepad()
    {
        InputManager.editingKeyboardControls = false;
    }
}
