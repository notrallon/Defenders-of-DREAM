using UnityEngine;

public interface IWeapon {
    string WeaponPickupSlug { get; set; }
    void Attack(Vector3 dir);
}
