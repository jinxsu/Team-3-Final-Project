using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController characterController;
    private AudioSource audio;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audio=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded == true && characterController.velocity.magnitude > 2f && audio.isPlaying==false)
        {
            audio.volume = Random.Range(0.8f, 1);
            audio.pitch = Random.Range(0.8f, 1.1f);
            audio.Play();
        }
        
    }
}
