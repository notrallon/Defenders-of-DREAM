using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField] private ControllerInputs_t m_PlayerInput;
    private CharacterController m_PlayerController;

    private string m_RightTrigger;

    //set the Emitter
    public GameObject ProjectileEmitter;

    //set projectile in Unity
    public GameObject projectile;

    //set velocity in Unity
    public float projectileVelocity;

    // bool to see if your shoot-button is already in use
    private bool m_isAxisInUse = false;

    // cooldown variables
    private float NextFire;
    public float FireRate = 1;


    private void Start()
    {
        switch (m_PlayerInput)
        {
            case ControllerInputs_t.PLAYER_1:
                {
                    m_RightTrigger = "JoyP1TriggerR";
                }
                break;

            case ControllerInputs_t.PLAYER_2:
                {
                    m_RightTrigger = "JoyP2TriggerR";
                }
                break;

            case ControllerInputs_t.PLAYER_3:
                {
                    m_RightTrigger = "JoyP3TriggerR";
                }
                break;

            case ControllerInputs_t.PLAYER_4:
                {
                    m_RightTrigger = "JoyP4TriggerR";
                }
                break;

            case ControllerInputs_t.KEYBOARD:
                {
                    m_RightTrigger = "JoyKeyboardTriggerR";
                }
                break;

            default:
                {

                }
                break;
        }
        m_PlayerController = GetComponent<CharacterController>();
    }


        // Update is called once per frame
    void Update()
    {
        //when button is pressed, you fire a projectile
        if (Input.GetAxisRaw(m_RightTrigger) != 0)
        {
            if (NextFire <= Time.time)
            {
                m_isAxisInUse = true;

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

                NextFire = Time.time + FireRate;
            }
        }

    }

}


/*
    if (Input.GetAxisRaw(m_RightTrigger) != 0)
    {
        m_isAxisInUse = true;
    }
    
    if (Input.GetAxisRaw(m_RightTrigger) == 0)
        {
            m_isAxisInUse = false;
        }
*/
