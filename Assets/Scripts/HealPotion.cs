using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour {

    public int healing = 50;
    public GameObject particles;

	void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameObject Player = col.gameObject;
            col.gameObject.GetComponent<PlayerHealth>().PickUpHealth(healing);
            Destroy(gameObject);
            
            //create particles for health pickup
            Instantiate(particles, Player.transform.position, Player.transform.rotation);
        }
    }

}
