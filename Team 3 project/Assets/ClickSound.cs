using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource audio;
    
   
    public void Click()
    {
        audio.Play();
    }
}
