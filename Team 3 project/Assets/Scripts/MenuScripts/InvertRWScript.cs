using UnityEngine;
using UnityEngine.UI;

public class InvertRWScript : MonoBehaviour
{
    

    [SerializeField]
    string toggleName;

    public void UpdateToggle()
    {
        gameObject.GetComponent<Toggle>().isOn = (PlayerPrefs.GetInt(toggleName) != 0);
    }
}
