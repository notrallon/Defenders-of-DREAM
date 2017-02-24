using UnityEngine;

public class PickupWaterBalloonGun : Interactable {

    public override void Interact() {
        Item item = new Item("WaterBalloonGun1");
        Player.GetComponent<PlayerWeaponController>().EquipWeapon(item);
        Destroy(gameObject);
    }
}
