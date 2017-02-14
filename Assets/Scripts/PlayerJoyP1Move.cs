using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoyP1Move : MonoBehaviour
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
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("JoyP1Horizontal");
        float v = Input.GetAxisRaw("JoyP1Vertical");


        Move(h, v);
        //Turning(r, l);
        

    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidBody.MovePosition(transform.position + movement);
    }
}