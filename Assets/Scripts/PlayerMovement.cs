using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float rotSpeed = 10f;

    Vector3 movement;
    Vector3 Rot;
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
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float u = Input.GetAxis("Mouse X");
        float d = Input.GetAxis("Mouse Y");


        Move(h, v, u, d);
        

    }

    void Move(float h, float v,float u, float d)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        Rot.Set(u, 0f, d);
        Rot = Rot.normalized * speed * Time.deltaTime;

        playerRigidBody.transform.rotation = Quaternion.LookRotation(Rot);

        playerRigidBody.MovePosition(transform.position + movement);
    }






    
}
