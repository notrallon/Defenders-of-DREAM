using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    [SerializeField] private ControllerInputs_t m_PlayerInput;
    [SerializeField] private float m_Speed = 6.0f;

    private string m_RightJoyHor;
    private string m_RightJoyVert;
    private string m_LeftJoyHor;
    private string m_LeftJoyVert;

    private CharacterController m_PlayerController;

	// Use this for initialization
    private void Start () {
        switch (m_PlayerInput) {
            case ControllerInputs_t.PLAYER_1: {
                m_LeftJoyHor = "JoyP1HorizontalL";
                m_LeftJoyVert = "JoyP1VerticalL";
                m_RightJoyHor = "JoyP1HorizontalR";
                m_RightJoyVert = "JoyP1VerticalR";
            } break;
            case ControllerInputs_t.PLAYER_2: {
                m_LeftJoyHor = "JoyP2HorizontalL";
                m_LeftJoyVert = "JoyP2VerticalL";
                m_RightJoyHor = "JoyP2HorizontalR";
                m_RightJoyVert = "JoyP2VerticalR";
            } break;
            case ControllerInputs_t.PLAYER_3: {
                m_LeftJoyHor = "JoyP3HorizontalL";
                m_LeftJoyVert = "JoyP3VerticalL";
                m_RightJoyHor = "JoyP3HorizontalR";
                m_RightJoyVert = "JoyP3VerticalR";
            } break;
            case ControllerInputs_t.PLAYER_4: {
                m_LeftJoyHor = "JoyP4HorizontalL";
                m_LeftJoyVert = "JoyP4VerticalL";
                m_RightJoyHor = "JoyP4HorizontalR";
                m_RightJoyVert = "JoyP4VerticalR";
            } break;
            case ControllerInputs_t.KEYBOARD: {
                m_LeftJoyHor = "JoyKeyboardHorizontalL";
                m_LeftJoyVert = "JoyKeyboardVerticalL";
                m_RightJoyHor = "JoyKeyboardHorizontalR";
                m_RightJoyVert = "JoyKeyboardVerticalR";
            } break;

            default: {
                
            } break;
        }

        m_PlayerController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
    private void Update () {
        var deltaMoveX = Input.GetAxis(m_LeftJoyHor) * m_Speed;
        var deltaMoveZ = Input.GetAxis(m_LeftJoyVert) * m_Speed;
        var deltaRotX = Input.GetAxis(m_RightJoyHor);
        var deltaRotZ = Input.GetAxis(m_RightJoyVert);
        
        Vector3 movement = new Vector3(deltaMoveX, 0, deltaMoveZ);

        Vector3 rotation;

        if (Mathf.Abs(deltaRotX) > 0 || Mathf.Abs(deltaRotZ) > 0) {
            // nothing
            rotation = new Vector3(deltaRotX, 0, deltaRotZ);
            transform.rotation = Quaternion.LookRotation(rotation);
        } else if (Mathf.Abs(movement.magnitude) > 0) {
            rotation = movement;
            transform.rotation = Quaternion.LookRotation(rotation);
        }
        
        movement = Vector3.ClampMagnitude(movement, m_Speed);
        movement.y -= 9.8f;
        movement *= Time.deltaTime;
        m_PlayerController.Move(movement);


        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        Debug.Log(transform.position);
    }
}
