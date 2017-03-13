using UnityEngine;

public class PickupWaterGun_TopTank : Interactable
{
    public override void Interact()
    {
        Item item = new Item("WaterGun_TopTank"); //Name of the weapon prefab that will held in the players hand
        Player.GetComponent<PlayerWeaponController>().EquipWeapon(item);
        Destroy(gameObject);
    }

    public override void SetPickupPlayerColor(Material playerColorMaterial)
    {
        // Set the weapons highlighted color to the player color
        GetComponent<Renderer>().materials[2].color = playerColorMaterial.color;
        GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", playerColorMaterial.GetColor("_EmissionColor"));
    }
}
