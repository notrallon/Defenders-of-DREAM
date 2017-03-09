/// <summary>
/// This script makes the PuzzleCube glow by changing it's material
/// once it has been pushed into the PuzzleFinish trigger. 
/// </summary>

using UnityEngine;

public class CubeChangeColor : MonoBehaviour
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
        if (col.gameObject.CompareTag("PuzzleFinish"))
        {
            //Get the material on the GameObject
            Renderer rend = GetComponent<Renderer>();
            //rend.sharedMaterial = materials[0];
            rend.material.shader = Shader.Find("Standard");
            rend.material.SetColor("_EmissionColor", Color.cyan);
        }
    }
}
