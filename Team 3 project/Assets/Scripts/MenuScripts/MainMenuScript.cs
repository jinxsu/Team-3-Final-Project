using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject manager;

    public GameObject knmSensSlider;
    public GameObject knmSensValue;

    public GameObject ctrSensSlider;
    public GameObject ctrSensValue;

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
        knmSensValue.GetComponent<TextMeshProUGUI>().text = knmSensSlider.GetComponent<Slider>().value.ToString();
        PlayerPrefs.SetInt("knmSens", ((int)knmSensSlider.GetComponent<Slider>().value * 5));
    }

    public void UpdateControllerSens()
    {
        ctrSensValue.GetComponent<TextMeshProUGUI>().text = ctrSensSlider.GetComponent<Slider>().value.ToString();
        PlayerPrefs.SetInt("ctrSens", ((int)ctrSensSlider.GetComponent<Slider>().value * 10));
    }
}
