using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetQuality : MonoBehaviour
{
    private Dropdown dropdown;
    private string[] GraphicSettingNames;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
