using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkOnTask : MonoBehaviour
{
    //To check if the player has pressed the button
    private bool buttonPressed;
    // count how long you've been holding down the button
    float counter;

    //Initialize the button as having not been pressed
    void Awake()
    {
        buttonPressed = false;
    }

    //Checks if someone enteres the triggerbox for the button
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //If someone presses A on any controller or E on keyboard the button is set to having been pressed (true)
            if (Input.GetKey(KeyCode.JoystickButton0) || (Input.GetKey(KeyCode.E)))
            {
                buttonPressed = true;
            }

            // if you let go of the button, reset counter and buttonPressed
            else if (!Input.GetKey(KeyCode.JoystickButton0) || (!Input.GetKey(KeyCode.E)))
            {
                buttonPressed = false;
                counter = 0;
            }
        }
    }

    void Update()
    {
        //When someone has pressed the button
        if (buttonPressed == true)
        {
            //Move the PuzzleLid GameObject upwards
            //(the directions are for some reason scued and need x to be -15 and z to be 60 to let the y axis move straight up/down)
            counter += Time.deltaTime;

            //Debug if counter is working
            if ((counter >= 1) && (counter <= 2))
            {
                Debug.Log("One second has passed!");
            }
            if ((counter >= 2) && (counter <= 3))
            {
                Debug.Log("Two seconds has passed!");
            }
            if (counter >= 3)
            {
                Debug.Log("Three seconds has passed!");
            }
        }
    }
}
