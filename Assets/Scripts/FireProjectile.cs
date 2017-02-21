using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    //set the Emitter
    public GameObject ProjectileEmitter;

    //set projectile in Unity
    public GameObject projectile;

    //set velocity in Unity
    public float projectileVelocity;

       
        // Use this for initialization
    void Start()
    {

    }

        // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            // Instantiate projectile
            GameObject TemporaryProjectile;
            TemporaryProjectile = Instantiate(projectile, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;

            //Projectiles may appear rotated  incorrectly due to the way its pivot was set from original model
            //Corrected here if needed:
            TemporaryProjectile.transform.Rotate(Vector3.left * 90);

            //Retrieve Rigidbody from instantiated projectile and control it
            Rigidbody Temporary_rb;
            Temporary_rb = TemporaryProjectile.GetComponent<Rigidbody>();

            //Give the projectile a velocity
            Temporary_rb.AddForce(transform.forward * projectileVelocity);

            //Destroy projectile after 2 sec
            Destroy(TemporaryProjectile, 2.0f);
        }
            
    }

}