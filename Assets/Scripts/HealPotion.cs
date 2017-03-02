using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour {

    public int healing = 50;

	void OnTriggerEnter (Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHealth>().PickUpHealth(healing);
            Destroy(gameObject);
        }
    }

}
