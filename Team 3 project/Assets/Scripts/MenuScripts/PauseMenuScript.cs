using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public PlayerControls playerControls;
    public GameObject player;
    PlayerControllerScript playerController;

    public GameObject manager;

    public GameObject knmSensSlider;
    public GameObject knmSensValue;

    public GameObject ctrSensSlider;
    public GameObject ctrSensValue;

    public GameObject playerUI;

    public GameObject interactUI;

    public GameObject deathMenu;

    bool playerDied;

    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI");
        playerController = player.GetComponent<PlayerControllerScript>();
        playerControls = playerController.controls;
        manager = GameObject.FindGameObjectWithTag("Manager");
        DontDestroyOnLoad(gameObject);
        playerDied = false;
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

        if (playerController.isDead && !playerDied)
        {
            playerDied = true;
            PlayerDeath();
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
        isPaused = false;
        Destroy(player);
        Destroy(gameObject);
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
        player.GetComponentInChildren<MouseLook>().mouseSens = (int)knmSensSlider.GetComponent<Slider>().value * 5;
    }

    public void UpdateControllerSens()
    {
        ctrSensValue.GetComponent<TextMeshProUGUI>().text = ctrSensSlider.GetComponent<Slider>().value.ToString();
        PlayerPrefs.SetInt("ctrSens", (int)ctrSensSlider.GetComponent<Slider>().value * 10);
        player.GetComponentInChildren<MouseLook>().controllerSens = (int)ctrSensSlider.GetComponent<Slider>().value * 10;
    }

    public void ToggleYInvert(bool toggleState)
    {
        InputManager.ToggleYInvert(toggleState);
    }

    public void ToggleXInvert(bool toggleState)
    {
        InputManager.ToggleXInvert(toggleState);
    }

    public void PlayerDeath()
    {
        GameObject.FindWithTag("ItemCamera").SetActive(false);
        deathMenu.SetActive(true);
        playerUI.SetActive(false);
        Time.timeScale = 0.0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
