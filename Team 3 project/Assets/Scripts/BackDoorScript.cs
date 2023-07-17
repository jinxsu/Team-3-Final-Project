using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackDoorScript : MonoBehaviour
{
    private bool playerDetected;
    private PlayerControls player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
            player = other.gameObject.GetComponent<PlayerControllerScript>().controls;
        }
    }

    private void Update()
    {
        if(playerDetected && player.Player.Interact.triggered)
        {
            StartCoroutine(LoadLevel2());
        }
    }


    private IEnumerator LoadLevel2()
    {
        Debug.Log("Fade to black...");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
            playerDetected = false;
        }
    }

}
