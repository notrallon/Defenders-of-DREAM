using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyBase {

    //set the Emitter
    public GameObject ProjectileEmitter;

    //set projectile in Unity
    public GameObject Projectile;

    //set velocity in Unity
    public float ProjectileVelocity;

    // cooldown variables
    private float m_NextFire;
    //public float FireRate = 1;


    public override void Attack()
    {
        if (!(m_NextFire < Time.time)) return;

        //Play shootSound
        //float vol = Random.Range(volLowRange, volHighRange);
        //audioSource.PlayOneShot(shootSound, vol);

        // Instantiate projectile
        var temporaryProjectile = Instantiate(Projectile, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation);

        //Projectiles may appear rotated  incorrectly due to the way its pivot was set from original model
        //Corrected here if needed:
        temporaryProjectile.transform.Rotate(Vector3.left * Random.Range(0, 360));

        //Retrieve Rigidbody from instantiated projectile and control it
        var temporaryRb = temporaryProjectile.GetComponent<Rigidbody>();

        //Give the projectile a velocity
        temporaryRb.AddForce(transform.forward * ProjectileVelocity);

        //Destroy projectile after 2 sec
        Destroy(temporaryProjectile, 2.0f);

        m_NextFire = Time.time + FireRate;
    }
}
