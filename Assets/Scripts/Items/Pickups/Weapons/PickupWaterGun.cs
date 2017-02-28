using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWaterGun : Interactable {

    public override void Interact()
    {
        Item item = new Item("WaterGun1");
        Player.GetComponent<PlayerWeaponController>().EquipWeapon(item);
        Destroy(gameObject);
    }
}
