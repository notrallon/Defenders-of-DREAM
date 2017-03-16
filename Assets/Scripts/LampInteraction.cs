using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampInteraction : MonoBehaviour {

    private SphereCollider LightCollider;
    private GameObject[] GreatLamp;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (LightCollider != null)
        {
            if (LightCollider.radius < 70)
            {
                LightCollider.radius += 15*Time.deltaTime;
            }
        }
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //If someone presses A on any controller or E on keyboard the button is set to having been pressed (true)
            if (Input.GetKeyDown(KeyCode.JoystickButton0) || (Input.GetKeyDown(KeyCode.E)))
            {

                LightCollider = GameObject.FindGameObjectWithTag("GreatLamp").GetComponent<SphereCollider>();

                //GreatLamp = GameObject.FindGameObjectsWithTag("GreatLamp");

                //foreach (GameObject Lamp in GreatLamp)
                //{
                //    LightCollider = GetComponent<SphereCollider>();
                //}

            }
        }
    }
}
