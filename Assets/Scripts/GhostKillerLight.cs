using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostKillerLight : MonoBehaviour {

    private SphereCollider LightCollider;
    GameObject enemyObject;
    private float damage = 3000f;

    public GameObject PlayerParticleSystem;
    public Color PlayerColor;

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

        //var script = enemyObject.GetComponent<EnemyBase>();
        //script.TakeDamage(damage);

        for (var i = 0; i < 10; i++) {
            PlayerParticleSystem.GetComponent<ParticleController>().EmitSplatterAtLocation(enemyObject.transform, PlayerColor);
        }

        Destroy(enemyObject);
    }
}
