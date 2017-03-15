using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    [SerializeField]
    private float selfDestructInSeconds;

	
	void Start () {

        Destroy(gameObject, selfDestructInSeconds);

	}
	
}
