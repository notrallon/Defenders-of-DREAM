using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampInteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //If someone presses A on any controller or E on keyboard the button is set to having been pressed (true)
            if (Input.GetKeyDown(KeyCode.JoystickButton0) || (Input.GetKeyDown(KeyCode.E)))
            {
                //STUFF
            }
        }
    }
}
