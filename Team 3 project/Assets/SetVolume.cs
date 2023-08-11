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


    // Start is called before the first frame update
    void Start()
    {

        volumeSlider = GetComponent<Slider>();
        float volume=PlayerPrefs.GetFloat(nameParam, 0.3f);
        volumeSlider.value = volume;
        mixer.SetFloat(nameParam, Mathf.Log10(volume)*30);
        
    }
    public void setVolume(float vol)
    {
        mixer.SetFloat(nameParam, Mathf.Log10(vol)*30);
        PlayerPrefs.SetFloat(nameParam, vol);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
