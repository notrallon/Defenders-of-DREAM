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

    private float vol = 0.4f;
    private float newPitch;
    private float minPitch = 0.7f;
    private float maxPitch = 1.3f;

    GameObject enemyObject;

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
            newPitch = Random.Range(minPitch, maxPitch);
            audioSource.pitch = newPitch;
            audioSource.PlayOneShot(impact, vol);

            enemyObject = col.gameObject;
            var script = enemyObject.GetComponent<IEnemy>();
            script.TakeDamage(damage);

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            Destroy(gameObject, 1);

            GameObject splat = Instantiate(explosion, enemyObject.transform.position, enemyObject.transform.rotation) as GameObject; // Instantiate the particle splash effect

            Destroy(splat, 1.0f); // destroy the splash-system



            //Debug.Log("Enemy is damaged!");
        }
    }


}