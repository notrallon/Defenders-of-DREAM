using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePuzzle : MonoBehaviour
{
    //drag the puzzlelid to this field in the inspector
    public GameObject PuzzleLid;
    //To smooth out the motion a bit
    public float smooth;
    //To check if the player has pressed the button
    private bool buttonPressed;

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
        }
    }

    void Update()
    {
        //When someone has pressed the button
        if (buttonPressed == true)
        {
            //Move the PuzzleLid GameObject upwards
            PuzzleLid.transform.position = Vector3.MoveTowards(PuzzleLid.transform.position, new Vector3(0, 100, 0), Time.deltaTime * smooth);
            //Get the material on the GameObject
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Standard");
            rend.material.SetColor("_EmissionColor", Color.cyan);

        }
    }
}
