using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetQuality : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    private string[] GraphicSettingNames;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        GraphicSettingNames=QualitySettings.names;
        List<string> dropdownOption = new List<string>();
        foreach(string name in GraphicSettingNames)
        {
            dropdownOption.Add(name);
        }
        dropdown.AddOptions(dropdownOption);
        dropdown.value=QualitySettings.GetQualityLevel();
    }

    public void setQuality()
    {
        QualitySettings.SetQualityLevel(dropdown.value,true);
    }
   void Update()
    {
        
    }

}
