using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackDoorScript : MonoBehaviour
{
    public void Interact()
    {
        StartCoroutine(LoadLevel2());
    }

    private IEnumerator LoadLevel2()
    {
        Debug.Log("Fade to black...");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
