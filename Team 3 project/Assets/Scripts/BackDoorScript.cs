using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackDoorScript : MonoBehaviour
{
    private bool playerDetected;
    private PlayerControllerScript player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
            player = other.gameObject.GetComponent<PlayerControllerScript>();
            player.intString = "Press E to open...?";
            player.canInteract = true;
        }
    }

    private void Update()
    {
        if(playerDetected && player.controls.Player.Interact.triggered)
        {
            player.canInteract = false;
            StartCoroutine(LoadLevel2());
        }
    }


    private IEnumerator LoadLevel2()
    {
        Debug.Log("Fade to black...");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.canInteract = false;
            player = null;
            playerDetected = false;
        }
    }

}
