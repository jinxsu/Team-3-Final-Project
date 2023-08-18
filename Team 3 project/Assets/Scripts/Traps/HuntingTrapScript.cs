using UnityEngine;

public class HuntingTrapScript : MonoBehaviour
{
    public bool isClosed = false;

    [SerializeField]
    Mesh closedMesh;
    public AudioSource audio;
    public AudioClip TrapClosingsound;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss") && !isClosed)
        {
            audio.PlayOneShot(TrapClosingsound);
            Debug.Log("Chomp");
            gameObject.transform.GetChild(1).GetComponent<MeshFilter>().mesh = closedMesh;
            gameObject.transform.GetChild(1).GetComponent<Transform>().position += new Vector3(0,0.5f,-0.5f);
            gameObject.transform.GetChild(1).GetComponent<Transform>().rotation = Quaternion.Euler(0,-10f,0);

            

            other.GetComponent<StateController>().hp -= 1;

            isClosed = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
