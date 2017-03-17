using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_01 : MonoBehaviour {

    [SerializeField]
    private CapsuleCollider collider;
    float attackDelay = 2.0f;
    float attackDuration = 2.5f;
    // Use this for initialization
    void Start () {

        collider = GetComponent<CapsuleCollider>();
        collider.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
        if (attackDelay > 0)
        {
            attackDelay -= Time.deltaTime;
            Debug.Log(attackDelay);
        }
        if(attackDelay <= 0)
        {
            collider.enabled = true;
            attackDelay = 0;
        }
        if(collider.enabled == true)
        {
            attackDuration -= Time.deltaTime;
            Debug.Log(attackDuration);
            if(attackDuration <= 0)
            {
                Debug.Log("destroed");
                Destroy(this.gameObject);
            }
        }

	}
}
