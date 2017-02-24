using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPickup : MonoBehaviour {


    //GameObject playerObject;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //playerObject = col.gameObject;
            //var script = playerObject.GetComponent<-get_what???->();
            //script.-weaponScript(-activate weapon-);

            Destroy(gameObject);

            Debug.Log("You picked up a Weapon!");
        }
    }
}

