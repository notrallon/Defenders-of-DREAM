using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour {

    public float damage;
    public GameObject explosion;

    GameObject enemyObject;

    protected AudioSource audioSource;
    public AudioClip impact;

    private float vol = 1.0f;
    private float newPitch;
    private float minPitch = 0.7f;
    private float maxPitch = 1.3f;
    private bool isPlaying;
    private float counter;

    public Color PlayerColor;

    // Use this for initialization
    void Start () {
        isPlaying = false;
        counter = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnParticleCollision(GameObject col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //if ((isPlaying == false) && (counter == 0))
            //{
            //    newPitch = Random.Range(minPitch, maxPitch);
            //    audioSource.pitch = newPitch;
            //    audioSource.PlayOneShot(impact, vol);
            //    isPlaying = true;
            //    counter = 1.0f;
            //}
            //else
            //{
            //    counter -= Time.deltaTime;
            //}

            enemyObject = col.gameObject;
            var script = enemyObject.GetComponent<IEnemy>();
            script.TakeDamage(damage, PlayerColor);

            GameObject.FindGameObjectWithTag("ParticleController").GetComponent<ParticleController>().EmitSplatterAtLocation(enemyObject.transform, PlayerColor);

            //GameObject splat = Instantiate(explosion, enemyObject.transform.position, enemyObject.transform.rotation) as GameObject; // Instantiate the particle splash effect
            //splat.transform.localScale *= 0.5f;

            //Destroy(splat, 1.0f); // destroy the splash-system


            //Debug.Log("Enemy is damaged!");
        }
    }
}
