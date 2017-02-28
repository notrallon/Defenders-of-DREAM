using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour {
    //set the Emitter
    public GameObject ProjectileEmitter;

    //set projectile in Unity
    public GameObject Projectile;

    //set velocity in Unity
    public float ProjectileVelocity;

    // cooldown variables
    private float m_NextFire;
    public float FireRate = 1;

    public void Shoot() {
        if (!(m_NextFire < Time.time)) return;
        // Instantiate projectile
        var temporaryProjectile = Instantiate(Projectile, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation);

        //Projectiles may appear rotated  incorrectly due to the way its pivot was set from original model
        //Corrected here if needed:
        temporaryProjectile.transform.Rotate(Vector3.left * 270);

        //Retrieve Rigidbody from instantiated projectile and control it
        var temporaryRb = temporaryProjectile.GetComponent<Rigidbody>();

        //Give the projectile a velocity
        temporaryRb.AddForce(transform.forward * ProjectileVelocity);

        //Destroy projectile after 2 sec
        Destroy(temporaryProjectile, 2.0f);

        m_NextFire = Time.time + FireRate;
    }
}
