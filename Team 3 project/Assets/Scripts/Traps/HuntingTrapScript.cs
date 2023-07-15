using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingTrapScript : MonoBehaviour
{
    public bool isClosed = false;

    [SerializeField]
    Mesh closedMesh;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss") && !isClosed)
        {

            Debug.Log("Chomp");
            gameObject.transform.GetChild(1).GetComponent<MeshFilter>().mesh = closedMesh;
            gameObject.transform.GetChild(1).GetComponent<Transform>().position += new Vector3(0,0.5f,-0.5f);
            gameObject.transform.GetChild(1).GetComponent<Transform>().rotation = Quaternion.Euler(0,-10f,0);

            gameObject.tag = "UsedTrap";

            other.GetComponent<StateController>().hp -= 1;

            isClosed = true;
        }
    }
}
