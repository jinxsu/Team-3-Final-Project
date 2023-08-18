using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSettings : MonoBehaviour
{
    [SerializeField] private string nameParam = null;
    float volume;
   

    void Awake()
    {
        SavedSoundSetting();
    }
    private void SavedSoundSetting()
    {
        volume=PlayerPrefs.GetFloat(nameParam);
    }
    
}
