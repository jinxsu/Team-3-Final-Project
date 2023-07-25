using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreGameSceneScript : MonoBehaviour
{
    public string sceneTo;


    private void Start()
    {
        sceneTo = InputManager.sceneTo;
    }


    // Update is called once per frame
    void Update()
    {
        SceneManager.LoadScene(sceneTo);
    }
}
