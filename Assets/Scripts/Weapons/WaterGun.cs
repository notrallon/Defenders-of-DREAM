using UnityEngine;

public class WaterGun : BaseWeapon, IWeapon {
    // String to the weapon pickup that should spawn when throwing away the weapon
    public string WeaponPickupSlug { get; set; } 

    // Use this for initialization
    private void Start () {
        WeaponPickupSlug = "WaterGun_Pickup";
        audioSource = GetComponent<AudioSource>();
    }
	
    public void Attack(Vector3 dir) {
        // Use base attack
        base.Attack(dir);
    }

    // Sets the correct placement for this weapon.
    public override void SetPlacement() {
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        transform.localPosition = new Vector3(0.001f, 0f, 0.085f);
        var rot = new Vector3(57.766f, 90.79201f, 270.044f);
        transform.localRotation = Quaternion.Euler(rot);
    }
}
