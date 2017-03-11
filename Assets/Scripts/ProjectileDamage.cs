using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{

    public float damage;
    public GameObject explosion;
    //private MeshRenderer rend;

    protected AudioSource audioSource;
    public AudioClip impact;

    //pitch and volume for SFX
    private float vol = 0.3f;
    private float newPitch;
    private float minPitch = 0.7f;
    private float maxPitch = 1.3f;

    GameObject enemyObject;

    ParticleController partController;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.Pause();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            // Set a random pitch and play audio
            newPitch = Random.Range(minPitch, maxPitch);
            audioSource.pitch = newPitch;
            audioSource.PlayOneShot(impact, vol);

            // Find the colliding object, send damage to colliding object (enemy)
            enemyObject = col.gameObject;
            var script = enemyObject.GetComponent<IEnemy>();
            script.TakeDamage(damage, GetComponent<Renderer>().material.color);

            // Hide gameObject and inactivate the collider, destroyt object after one second
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            Destroy(gameObject, 1);

            // instantiate particle effect on enemy position
            //GameObject splat = Instantiate(explosion, enemyObject.transform.position, enemyObject.transform.rotation) as GameObject; // Instantiate the particle splash effect

            partController.EmitSplatterAtLocation(enemyObject.transform, GetComponent<Renderer>().material.color);

            // destroy the splash-system
            //Destroy(splat, 1.0f); 



            //Debug.Log("Enemy is damaged!");
        }
    }

    public void SetParticleController(ParticleController controller) {
        partController = controller;
    }


}