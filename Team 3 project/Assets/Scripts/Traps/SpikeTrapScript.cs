using Unity.AI.Navigation;
using UnityEngine;

public class SpikeTrapScript : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface surface;
    public AudioSource audio;
    public AudioClip trapBreakingSound;

    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Detected " + other.ToString());

        if (other.gameObject.CompareTag("Boss"))
        {
            Debug.Log("Hit a boss");
            other.GetComponent<StateController>().hp -= 1;
            audio.PlayOneShot(trapBreakingSound);
            Destroy(gameObject);
        }
    }
}
