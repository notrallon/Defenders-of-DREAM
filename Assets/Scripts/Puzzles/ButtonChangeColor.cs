/// <summary>
/// This script is used to quickly highlight the button GameObject to
/// help indicate to the player that this object is interactable just
/// like how the player picks up weapons.
/// </summary>

using UnityEngine;

public class ButtonChangeColor : MonoBehaviour
{
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
            //Get the material on the GameObject
            Renderer rend = GetComponent<Renderer>();
            //rend.sharedMaterial = materials[0];
            rend.material.shader = Shader.Find("Standard");
            rend.material.SetColor("_EmissionColor", Color.cyan);

        }
    }
}
