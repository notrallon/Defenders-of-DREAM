/// <summary>
/// This script moves the PuzzleLid upwards once the player has moved within the trigger
/// and has pressed the button, allowing the player(s) to take on the puzzle or event
/// that's hidden under the PuzzleLid GameObject.
/// </summary>

using UnityEngine;

public class ActivatePuzzle : MonoBehaviour
{
    //drag the puzzlelid to this field in the inspector
    [SerializeField]
    private GameObject PuzzleLid;
    //To smooth out the motion a bit
    [SerializeField]
    private float smooth;
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
            //(the directions are for some reason scued and need x to be -15 and z to be 60 to let the y axis move straight up/down)
            PuzzleLid.transform.position = Vector3.MoveTowards(PuzzleLid.transform.position, new Vector3(-15, 100, 60), Time.deltaTime * smooth);
            //Get the material on the GameObject
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Standard");
            rend.material.SetColor("_EmissionColor", Color.cyan);
        }
    }
}
