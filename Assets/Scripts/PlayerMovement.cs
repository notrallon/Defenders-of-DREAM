using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float rotSpeed = 10f;

    Vector3 movement;
    Vector3 mousePos;
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
        
        mousePos = Input.mousePosition;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float d = mousePos.y;
        float u = mousePos.x;
        
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.rigidbody != playerRigidBody)
                transform.rotation = Quaternion.LookRotation(hit.point - transform.position);
            
            //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }

        Move(h, v);

    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidBody.MovePosition(transform.position + movement);
    }
}
