using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SpikeTrapScript : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface surface;

    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            other.GetComponent<StateController>().hp -= 1;
            Destroy(this.gameObject);
        }
    }
}
