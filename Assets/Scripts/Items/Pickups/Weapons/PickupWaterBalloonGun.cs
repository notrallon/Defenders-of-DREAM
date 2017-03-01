using UnityEngine;

public class PickupWaterBalloonGun : Interactable {

    public override void Interact() {
        Item item = new Item("WaterBalloonGun1");
        Player.GetComponent<PlayerWeaponController>().EquipWeapon(item);
        Destroy(gameObject);
    }

    public override void SetPickupPlayerColor(Material playerColorMaterial) {
        // Set the weapons highlighted color to the player color
        GetComponent<Renderer>().materials[2].color = playerColorMaterial.color;
        GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", playerColorMaterial.GetColor("_EmissionColor"));
    }
}
