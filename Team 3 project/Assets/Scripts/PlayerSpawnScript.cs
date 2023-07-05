using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log("Player selected", this);
    }

    private void Start()
    {
        player.transform.position = transform.position;
        Debug.Log("Player moved", this);
    }
}
