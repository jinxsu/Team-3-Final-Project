using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    public GameObject manager;

    public GameObject knmSensSlider;
    public GameObject knmSensValue;

    public GameObject ctrSensSlider;
    public GameObject ctrSensValue;
    private Slider kSlider;

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
        if (kSlider == null)
        {
            kSlider = knmSensSlider.GetComponent<Slider>(); 
        }
        knmSensValue.GetComponent<TextMeshProUGUI>().text = kSlider.value.ToString();
        PlayerPrefs.SetInt("knmSens", ((int)kSlider.value * 5));
    }

    public void UpdateControllerSens()
    {
        ctrSensValue.GetComponent<TextMeshProUGUI>().text = ctrSensSlider.GetComponent<Slider>().value.ToString();
        PlayerPrefs.SetInt("ctrSens", ((int)ctrSensSlider.GetComponent<Slider>().value * 10));
    }
}
