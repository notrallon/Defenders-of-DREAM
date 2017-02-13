using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float rotSpeed = 10f;

    Vector3 movement;
    Vector3 rotating;
    Animator anim;
    Rigidbody playerRigidBody;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        //anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        Move(h, v);
        //Turning(r, l);
        //Animating(h, v);

    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidBody.MovePosition(transform.position + movement);
    }






    //void Turning(float r, float l)
    //{

    //    playerRigidBody.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);

    //    //playerRigidBody.Position(transform.rotation);
    //}

    //void Turning ()
    //{
    //    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    RaycastHit floorHit;

    //    if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
    //    {
    //        Vector3 playerToMouse = floorHit.point - transform.position;
    //        playerToMouse.y = 0f;

    //        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
    //        playerRigidBody.MoveRotation(newRotation);

    //    }
    //}



    //void Animating(float h, float v)
    //{
    //    bool walking = h != 0f || v != 0f; // is either Vvertical or horizontal pressed? then we are walking
    //    anim.SetBool("IsWalking", walking);

    //}
}
