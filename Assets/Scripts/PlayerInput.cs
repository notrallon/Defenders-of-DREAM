using System;
using UnityEngine;
using XInputDotNetPure;

public class PlayerInput : MonoBehaviour {
    [SerializeField] private ControllerInputs_t m_PlayerInput;
    [SerializeField] private float m_Speed = 6.0f;

    private string m_RightJoyHor;
    private string m_RightJoyVert;
    private string m_LeftJoyHor;
    private string m_LeftJoyVert;
    private string m_ShootTrigger;
    private string m_MeleeTrigger;

    private KeyCode m_InteractButton;

    [SerializeField]
    private bool m_Debug;

    public Interactable Interact { get; set; }

    private CharacterController m_PlayerController;
    private PlayerStates_t m_PlayerState;

    private Animator m_Animator;

    [SerializeField]
    private AudioClip m_StepSound;
    private AudioSource audio;

    public InputMethod_t InputMethod = InputMethod_t.X_INPUT;

    private float footStepRate = 0.3f;
    private float m_nextStep = 0.5f;
    private float stepVolume = 0.05f;

    private PlayerIndex m_PlayerIndex;
    private GamePadState m_GamePadState;
    private float m_Vibration;

    private float ShootingRotateSpeed = 1f;
    private float DefaultRotateSpeed = 10f;
    private float m_RotateSpeed;

    // Particle system that takes care of ground splatter.
    public GameObject PlayerParticleSystem;

    // Use this for initialization
    private void Start () {
        m_RotateSpeed = DefaultRotateSpeed;

        // Instantiate the players particlesystem
        var ps = Instantiate(PlayerParticleSystem);
        PlayerParticleSystem = ps;

        // Check what player is playing and assign keybindings accordingly.
        switch (m_PlayerInput) {
            case ControllerInputs_t.PLAYER_1: {
                switch (InputMethod) {
                    case InputMethod_t.INPUT_MANAGER: {
                        m_LeftJoyHor = "JoyP1HorizontalL";
                        m_LeftJoyVert = "JoyP1VerticalL";
                        m_RightJoyHor = "JoyP1HorizontalR";
                        m_RightJoyVert = "JoyP1VerticalR";
                        m_ShootTrigger = "JoyP1TriggerR";
                        m_MeleeTrigger = "JoyP1TriggerL";
                        m_InteractButton = KeyCode.Joystick1Button0;
                    } break;

                    case InputMethod_t.X_INPUT: {
                        m_PlayerIndex = PlayerIndex.One;
                    } break;

                    default: {
                        
                    } break;

                }
            } break;

            case ControllerInputs_t.PLAYER_2: {
                switch (InputMethod) {
                    case InputMethod_t.INPUT_MANAGER: {
                        m_LeftJoyHor = "JoyP2HorizontalL";
                        m_LeftJoyVert = "JoyP2VerticalL";
                        m_RightJoyHor = "JoyP2HorizontalR";
                        m_RightJoyVert = "JoyP2VerticalR";
                        m_ShootTrigger = "JoyP2TriggerR";
                        m_MeleeTrigger = "JoyP2TriggerL";
                        m_InteractButton = KeyCode.Joystick2Button0;
                    } break;

                    case InputMethod_t.X_INPUT: {
                        m_PlayerIndex = PlayerIndex.Two;
                    } break;

                    default: {
                        
                    } break;
                }
            } break;

            case ControllerInputs_t.PLAYER_3: {
                switch (InputMethod) {
                    case InputMethod_t.INPUT_MANAGER: {
                        m_LeftJoyHor = "JoyP3HorizontalL";
                        m_LeftJoyVert = "JoyP3VerticalL";
                        m_RightJoyHor = "JoyP3HorizontalR";
                        m_RightJoyVert = "JoyP3VerticalR";
                        m_ShootTrigger = "JoyP3TriggerR";
                        m_MeleeTrigger = "JoyP3TriggerL";
                        m_InteractButton = KeyCode.Joystick3Button0;
                    } break;

                    case InputMethod_t.X_INPUT: {
                        m_PlayerIndex = PlayerIndex.Three;
                    } break;

                    default: {
                        
                    } break;
                }
            } break;

            case ControllerInputs_t.PLAYER_4: {
                switch (InputMethod) {
                    case InputMethod_t.INPUT_MANAGER: {
                        m_LeftJoyHor = "JoyP4HorizontalL";
                        m_LeftJoyVert = "JoyP4VerticalL";
                        m_RightJoyHor = "JoyP4HorizontalR";
                        m_RightJoyVert = "JoyP4VerticalR";
                        m_ShootTrigger = "JoyP4TriggerR";
                        m_MeleeTrigger = "JoyP4TriggerL";
                        m_InteractButton = KeyCode.Joystick4Button0;
                    } break;

                    case InputMethod_t.X_INPUT: {
                        m_PlayerIndex = PlayerIndex.Four;
                    } break;

                    default: {
                        
                    } break;
                }
            } break;

            case ControllerInputs_t.KEYBOARD: {
                m_LeftJoyHor = "JoyKeyboardHorizontalL";
                m_LeftJoyVert = "JoyKeyboardVerticalL";
                m_RightJoyHor = "JoyKeyboardHorizontalR";
                m_RightJoyVert = "JoyKeyboardVerticalR";
                m_ShootTrigger = "JoyKeyboardTriggerR";
                m_MeleeTrigger = "JoyKeyboardTriggerL";
                m_InteractButton = KeyCode.Return;
            } break;

            default: {
                
            } break;
        }

        m_PlayerState = PlayerStates_t.IDLE;
        m_Animator = GetComponent<Animator>();
        m_PlayerController = GetComponent<CharacterController>();

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update () {
        switch (InputMethod) {
            case InputMethod_t.INPUT_MANAGER: {
                InputManagerPlayerMove();

                // Check if the player is trying to interact with something
                if (Input.GetKeyDown(m_InteractButton)) {
                    if (Interact != null) {
                        Interact.Interact();
                    }
                }
            } break;

            case InputMethod_t.X_INPUT: {
                var prevState = m_GamePadState;
                m_GamePadState = GamePad.GetState(m_PlayerIndex);
                XInputPlayerMove();

                if (prevState.Buttons.A == ButtonState.Released && m_GamePadState.Buttons.A == ButtonState.Pressed) {
                    if (Interact != null) {
                        Interact.Interact();
                    }
                }

                if (m_GamePadState.Buttons.Y == ButtonState.Pressed && 
                    prevState.Buttons.Y == ButtonState.Released) {
                    GetComponent<PlayerWeaponController>().WeaponThrow();
                }
            } break;

            default: {
                
            } break;
        }

        GamePad.SetVibration(m_PlayerIndex, m_Vibration, m_Vibration);

        return;
        // TODO (richard): Make a state machine
        switch (m_PlayerState) {
            case PlayerStates_t.IDLE: {
            } break;

            case PlayerStates_t.RUNNING: {
            } break;

            case PlayerStates_t.RUN_SHOOTING: {
            } break;

            case PlayerStates_t.STAND_SHOOTING: {
                
            } break;

            default: {
                
            } break;
        }
    }

    private void XInputPlayerMove() {
        // Get values from thumbsticks.
        var deltaMoveX = m_GamePadState.ThumbSticks.Left.X * m_Speed;
        var deltaMoveZ = m_GamePadState.ThumbSticks.Left.Y * m_Speed;
        var deltaRotX = m_GamePadState.ThumbSticks.Right.X;
        var deltaRotZ = m_GamePadState.ThumbSticks.Right.Y;

        var movement = new Vector3(deltaMoveX, 0, deltaMoveZ);

        // Rotate based on movement/aim
        Vector3 rotation;
        if (Mathf.Abs(deltaRotX) > 0 || Mathf.Abs(deltaRotZ) > 0) {
            // Rotate in relation to the axis of our right stick
            rotation = new Vector3(deltaRotX, 0, deltaRotZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotation), m_RotateSpeed * Time.deltaTime);
        } else if (Mathf.Abs(movement.magnitude) > 0) {
            // Rotate in the direction we're moving
            rotation = movement;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotation), m_RotateSpeed * Time.deltaTime);
        }

        // Get dot product from our movement to see what direction we're in relation to 
        // our rotation and send it to our animator so it can decide what animation to use.
        var dotForward = Vector3.Dot(transform.forward, movement);
        var dotRight = Vector3.Dot(transform.right, movement);
        m_Animator.SetFloat("VelocityZ", dotForward);
        m_Animator.SetFloat("VelocityX", dotRight);

        // Clamp magnitude so that we can't move faster than our max speed
        movement = Vector3.ClampMagnitude(movement, m_Speed);
        movement *= Time.deltaTime; // Multiply by deltatime so we don't move based on framerate

        // Check the players position relative to camera viewport
        var pos = Camera.main.WorldToViewportPoint(transform.position + movement);

        // TODO (richard): Ugly way to limit movement, please fix. Try in camera class
        if (pos.y < 0.01f && movement.z < 0 ||
            pos.y > 0.86 && movement.z > 0) {
            movement.z = 0;
        }

        if (pos.x <= 0.1f && movement.x < 0 ||
            pos.x >= 0.9f && movement.x > 0) {
            movement.x = 0;
        }

        if (m_Debug) {
            Debug.Log(pos);
        }

        // Move the player
        m_PlayerController.Move(movement);

        // Move sound effect
        if (movement.magnitude > 0) {
            if (m_nextStep < Time.time && !audio.isPlaying) {
                audio.PlayOneShot(m_StepSound, stepVolume);
                audio.pitch = UnityEngine.Random.Range(1.2f, 1.5f);
                m_nextStep = Time.time + footStepRate;
            }
        }


        // Shooting
        if (Math.Abs(m_GamePadState.Triggers.Right) > 0) {
            GetComponent<Animator>().SetBool("Shoot", true);
            //GetComponent<FireProjectile>().Shoot();
            GetComponent<PlayerWeaponController>().PerformWeaponAttack();
            m_RotateSpeed = ShootingRotateSpeed;
        } else {
            GetComponent<Animator>().SetBool("Shoot", false);
            m_RotateSpeed = DefaultRotateSpeed;
        }
    }

    private void InputManagerPlayerMove() {
        var deltaMoveX = Input.GetAxis(m_LeftJoyHor) * m_Speed;
        var deltaMoveZ = Input.GetAxis(m_LeftJoyVert) * m_Speed;
        var deltaRotX = Input.GetAxis(m_RightJoyHor);
        var deltaRotZ = Input.GetAxis(m_RightJoyVert);

        var movement = new Vector3(deltaMoveX, 0, deltaMoveZ);

        // Rotate based on movement/aim
        Vector3 targetRotation;
        if (Mathf.Abs(deltaRotX) > 0 || Mathf.Abs(deltaRotZ) > 0) {
            // Set targetRotation in relation to the axis of our right stick
            targetRotation = new Vector3(deltaRotX, 0, deltaRotZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetRotation), m_RotateSpeed * Time.deltaTime);
        } else if (Mathf.Abs(movement.magnitude) > 0) {
            // Set targetRotation to the direction we're moving
            targetRotation = movement;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetRotation), m_RotateSpeed * Time.deltaTime);
        }
        
        // Get dot product from our movement to see what direction we're in relation to 
        // our rotation and send it to our animator so it can decide what animation to use.
        var dotForward = Vector3.Dot(transform.forward, movement);
        var dotRight = Vector3.Dot(transform.right, movement);
        m_Animator.SetFloat("VelocityZ", dotForward);
        m_Animator.SetFloat("VelocityX", dotRight);

        // Clamp magnitude so that we can't move faster than our max speed
        movement = Vector3.ClampMagnitude(movement, m_Speed);
        movement *= Time.deltaTime; // Multiply by deltatime so we don't move based on framerate

        // Check the players position relative to camera viewport
        var pos = Camera.main.WorldToViewportPoint(transform.position + movement);

        // TODO (richard): Ugly way to limit movement, please fix. Try in camera class
        if (pos.y < 0.01f && movement.z < 0 ||
            pos.y > 0.86 && movement.z > 0) {
            movement.z = 0;
        }

        if (pos.x <= 0.1f && movement.x < 0 ||
            pos.x >= 0.9f && movement.x > 0) {
            movement.x = 0;
        }

        if (m_Debug) {
            Debug.Log(pos);
        }

        // Move the player
        m_PlayerController.Move(movement);

        // Move sound effect
        if (movement.magnitude > 0)
        {
            if (m_nextStep < Time.time && !audio.isPlaying)
            {
                audio.PlayOneShot(m_StepSound, stepVolume);
                audio.pitch = UnityEngine.Random.Range(1.2f, 1.5f);
                m_nextStep = Time.time + footStepRate;
            }
        }


        // Shooting
        if (Math.Abs(Input.GetAxisRaw(m_ShootTrigger)) > 0) {
            GetComponent<Animator>().SetBool("Shoot", true);
            //GetComponent<FireProjectile>().Shoot();
            GetComponent<PlayerWeaponController>().PerformWeaponAttack();
            m_RotateSpeed = ShootingRotateSpeed;
        } else {
            GetComponent<Animator>().SetBool("Shoot", false);
            m_RotateSpeed = DefaultRotateSpeed;
        }
    }

    public void SetVibration(float amount) {
        m_Vibration = amount;
    }
}
