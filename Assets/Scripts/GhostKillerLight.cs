using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostKillerLight : MonoBehaviour {

    private SphereCollider LightCollider;
    GameObject enemyObject;
    private float damage = 3000f;

    // Use this for initialization
    void Start () {
        LightCollider = gameObject.GetComponent<SphereCollider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.CompareTag("Enemy") && (!col.gameObject.CompareTag("PurpleGhost"))) return;
        
            enemyObject = col.gameObject;

            var script = enemyObject.GetComponent<EnemyBase>();
            script.TakeDamage(damage);

            Destroy(enemyObject);
        
    }
}
