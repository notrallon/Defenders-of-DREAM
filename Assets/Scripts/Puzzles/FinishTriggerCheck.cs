using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTriggerCheck : MonoBehaviour
{
    //Put the PuzzleCube prefab in this field in the inspector
    [SerializeField]
    private GameObject Cube;

    void OnTriggerEnter(Collider Cube)
    {
        //Write what you want to trigger here
        //Added debug message for testing
        Debug.Log("Cube Entered Trigger");
        
    }
}
