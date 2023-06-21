using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public PlayerControls playerControls;
    public GameObject player;

    public GameObject manager;

    public GameObject knmSensSlider;
    public GameObject knmSensValue;

    public GameObject ctrSensSlider;
    public GameObject ctrSensValue;

    public GameObject playerUI;

    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI");
        playerControls = player.GetComponent<PlayerControllerScript>().controls;
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControls.Player.Pause.triggered)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        playerUI.SetActive(false);
        Time.timeScale = 0.0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        playerUI.SetActive(true);
        Time.timeScale = 1.0f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {

    }

    public void EditGamepad()
    {
        InputManager.editingKeyboardControls = false;
        ctrSensSlider.GetComponent<Slider>().value = PlayerPrefs.GetInt("ctrSens") / 10;
        ctrSensValue.GetComponent<TextMeshProUGUI>().text = (PlayerPrefs.GetInt("ctrSens") / 10).ToString();
    }

    public void EditMouse()
    {
        knmSensSlider.GetComponent<Slider>().value = PlayerPrefs.GetInt("knmSens") / 5;
        knmSensValue.GetComponent<TextMeshProUGUI>().text = (PlayerPrefs.GetInt("knmSens") / 5).ToString();
    }

    public void UpdateMouseSens()
    {
        knmSensValue.GetComponent<TextMeshProUGUI>().text = knmSensSlider.GetComponent<Slider>().value.ToString();
        PlayerPrefs.SetInt("knmSens", (int)knmSensSlider.GetComponent<Slider>().value * 5);
        player.GetComponent<MouseLook>().mouseSens = (int)knmSensSlider.GetComponent<Slider>().value * 5;
    }

    public void UpdateControllerSens()
    {
        ctrSensValue.GetComponent<TextMeshProUGUI>().text = ctrSensSlider.GetComponent<Slider>().value.ToString();
        PlayerPrefs.SetInt("ctrSens", (int)ctrSensSlider.GetComponent<Slider>().value * 10);
        player.GetComponent<MouseLook>().controllerSens = (int)ctrSensSlider.GetComponent<Slider>().value * 10;
    }

    public void ToggleYInvert(bool toggleState)
    {
        InputManager.ToggleYInvert(toggleState);
    }

    public void ToggleXInvert(bool toggleState)
    {
        InputManager.ToggleXInvert(toggleState);
    }
}