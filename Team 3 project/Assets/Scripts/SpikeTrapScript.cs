using Unity.AI.Navigation;
using UnityEngine;

public class SpikeTrapScript : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface surface;

    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            other.GetComponent<StateController>().hp -= 1;
            Destroy(gameObject);
        }
    }
}
