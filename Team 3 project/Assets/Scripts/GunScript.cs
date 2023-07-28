using UnityEngine;

public class GunScript : MonoBehaviour
{
    
    public PlayerControls controls;

    public Camera playerCam;

    public LayerMask layerMask;

    public Animator animator;
    public ParticleSystem particles;
    private AudioSource audio;


    private void Awake()
    {
        controls = InputManager.inputActions;
        playerCam = GameObject.FindWithTag("Player").GetComponentInChildren<Camera>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (controls.Player.Fire.triggered && !PauseMenuScript.isPaused)
        {
            animator.SetTrigger("Fire");
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 500, layerMask))
        {
            Debug.Log("Shot: " + hit.transform.tag);
            if (hit.transform.tag == "Boss")
            {
                hit.transform.SendMessage("BossHitByRay");
            }
        }
        //add a shot sound
        audio.Play();
        //Neither of these worked to get the particle system to reset and play from the beginning on every click even though, as far as I can tell, they should

        //particles.Clear();
        //particles.Simulate(0f,true,true);
        particles.Play();
    }
}
