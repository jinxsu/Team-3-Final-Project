using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3TransitionScript : MonoBehaviour
{
    public bool SmallBossDefeated = false;
    public bool SpitterBossDefeated = false;

    [SerializeField] private GameObject[] objects;
    [SerializeField] private List<Material> materials;
    [SerializeField] private List<MeshCollider> colliders;

    [SerializeField] private float dissolveRate = 0.0125f;
    [SerializeField] private float refreshRate = 0.025f;

    [SerializeField] private GameObject DeathBoxes;

    private void Start()
    {
        foreach(GameObject obj in objects)
        {
            materials.Add(obj.GetComponent<MeshRenderer>().material);
            colliders.Add(obj.GetComponent<MeshCollider>());
        }
    }

    public void AreAllBossesDefeated()
    {
        if (SmallBossDefeated & SpitterBossDefeated)
        {
            StartCoroutine(TransitionLevel3());
        }
    }

    private IEnumerator TransitionLevel3()
    {
        yield return new WaitForSeconds(2f);
        DeathBoxes.SetActive(false);
        print("floor starts breaking now");

        if(colliders.Count > 0)
        {
            foreach(MeshCollider collider in colliders)
            { 
                collider.enabled = false; 
            }
        }

        if (materials.Count > 0)
        {
            float counter = 0f;
            while (materials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for (int i = 0; i < materials.Count; i++)
                {
                    materials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
