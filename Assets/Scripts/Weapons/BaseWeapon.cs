using UnityEngine;

public class BaseWeapon : MonoBehaviour, IWeapon, IProjectileWeapon {
    public Transform ProjectileSpawn { get; set; }

    // Use this for initialization
    private void Start () {
		
	}
	
	// Update is called once per frame
    private void Update () {
		
	}

    public void Attack() {
        Debug.Log("Base Weapon Attack");
    }

    public void ShootProjectile() {
        Debug.Log("Base weapon shoot");
    }
}
