using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class setResolution : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    private Resolution[] resolutions;
 

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        resolutions = Screen.resolutions;
        List<string> list = new List<string>();
        int position = 0;
        int i = 0;
        Resolution currentResolution=Screen.currentResolution;
        foreach(Resolution resolution in resolutions)
        {
            string value=resolution.ToString();
            list.Add(value);
            if( resolution.width==currentResolution.width&&
                resolution.height==currentResolution.height&&
                resolution.refreshRate==currentResolution.refreshRate )
            {
                position = i;
            }
            i++;
        }
        dropdown.AddOptions(list);
        dropdown.value = position;
    }
    public void setresolution()
    {
        Resolution resolution = resolutions[dropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRate);
    }

    void Update()
    {
        
    }
}
