using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyboxCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    
    private void Update() {
        var dt = Time.deltaTime;

        var rotationValue = gameObject.transform.rotation.eulerAngles;

        rotationValue.x += dt;
        rotationValue.y = -90;
        rotationValue.z = -90;

        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotationValue), 0.5f);
        //gameObject.transform.rotation = rotationValue;
    }
}
