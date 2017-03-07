using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePuzzle : MonoBehaviour
{
    public GameObject PuzzleLid;
    public float smooth;
    private bool buttonPressed;

    //Initialize the button as having not been pressed
    void Awake()
    {
        buttonPressed = false;
    }

    //Checks if someone enteres the triggerbox for the button
    void OnTriggerStay(Collider col)
    {
        //If someone presses A on any controller or E on keyboard the button is set to having been pressed (true)
        if (Input.GetKey(KeyCode.JoystickButton0) || (Input.GetKey(KeyCode.E)))
        {
            buttonPressed = true;
        }
    }

    void Update()
    {
        //When someone has pressed the button
        if (buttonPressed == true)
        {
            //Move the PuzzleLid GameObject upwards
            PuzzleLid.transform.position = Vector3.MoveTowards(PuzzleLid.transform.position, new Vector3(0, 100, 0), Time.deltaTime * smooth);
        }
    }
}
