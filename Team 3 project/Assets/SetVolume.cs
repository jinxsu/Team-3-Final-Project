using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class SetVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer = null;
    [SerializeField] private string nameParam = null;
    private Slider volumeSlider;
    private static readonly string firstPlay = "FirstPlay";
    private int firstPlayInt;
    float volume;


    // Start is called before the first frame update
    void Start()
    {
      
        volumeSlider = GetComponent<Slider>();
        firstPlayInt = PlayerPrefs.GetInt(firstPlay);
        

        if (firstPlayInt == 0)
        {
            volume = PlayerPrefs.GetFloat(nameParam, 0.3f);
            volumeSlider.value = volume;
            mixer.SetFloat(nameParam, Mathf.Log10(volume) * 30f);
            PlayerPrefs.SetFloat(nameParam, volume);
            PlayerPrefs.SetInt(firstPlay,-1);
            
        }
        else
        {
           volume= PlayerPrefs.GetFloat(nameParam);
            volumeSlider.value= volume;
        }

    }
    public void setVolume(float vol)
    {
        mixer.SetFloat(nameParam, Mathf.Log10(vol)*30f);
        PlayerPrefs.SetFloat(nameParam, vol);
        SavesoundSetting();
        
    }
    public void SavesoundSetting()
    {
        PlayerPrefs.SetFloat(nameParam, volumeSlider.value);
    }
    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            SavesoundSetting();
        }
    }
    public void UpdateSound()
    {
        
    }
}
