/// <summary>
/// This script is used to quickly highlight the button GameObject to
/// help indicate to the player that this object is interactable just
/// like how the player picks up weapons.
/// </summary>

using UnityEngine;

public class ButtonChangeColor : Interactable {
    public override void Interact() {
        Destroy(GetComponent<BoxCollider>());
        GetComponentInParent<ActivatePuzzle>().Activate();
        //Get the material on the GameObject
        Renderer rend = GetComponent<Renderer>();
        //rend.sharedMaterial = materials[0];
        rend.material.shader = Shader.Find("Standard");
        rend.material.SetColor("_EmissionColor", Color.cyan);
    }
}
