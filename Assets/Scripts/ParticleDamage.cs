using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour {

    public float damage;
    public GameObject explosion;

    GameObject enemyObject;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnParticleCollision(GameObject col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemyObject = col.gameObject;
            var script = enemyObject.GetComponent<IEnemy>();
            script.TakeDamage(damage);

            GameObject splat = Instantiate(explosion, enemyObject.transform.position, enemyObject.transform.rotation) as GameObject; // Instantiate the particle splash effect
            splat.transform.localScale *= 0.5f;

            Destroy(splat, 1.0f); // destroy the splash-system

            //Debug.Log("Enemy is damaged!");
        }
    }
}
