using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoyP1Move : MonoBehaviour
{
    public float speed = 6f;
    public float rotSpeed = 10f;

    Vector3 movement;
    Vector3 rotaton;
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
        float h = Input.GetAxis("JoyP1HorizontalL");
        float v = Input.GetAxis("JoyP1VerticalL");
        float d = Input.GetAxis("JoyP1HorizontalR");
        float u = Input.GetAxis("JoyP1VerticalR");
        


        Move(h, v, d, u);


    }

    void Move(float h, float v, float d, float u)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        rotaton.Set(d, 0f, u);
        rotaton = rotaton.normalized * speed * Time.deltaTime;

        if(rotaton.magnitude != 0.0f)
        {
            playerRigidBody.transform.rotation = Quaternion.LookRotation(rotaton);
        }

        else if(movement.magnitude != 0.0f)
            playerRigidBody.transform.rotation = Quaternion.LookRotation(movement);

        playerRigidBody.MovePosition(transform.position + movement);
        

    }


}