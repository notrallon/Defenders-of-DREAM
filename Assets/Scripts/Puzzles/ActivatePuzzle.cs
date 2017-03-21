/// <summary>
/// This script moves the PuzzleLid upwards once the player has moved within the trigger
/// and has pressed the button, allowing the player(s) to take on the puzzle or event
/// that's hidden under the PuzzleLid GameObject.
/// </summary>

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ActivatePuzzle : MonoBehaviour
{
    //drag the puzzlelid to this field in the inspector
    [SerializeField]
    private GameObject PuzzleLid;
    //To smooth out the motion a bit
    [SerializeField]
    private float smooth;
    //To check if the player has pressed the button
    private bool buttonPressed;

    //This section is for if the Lid should spawn enemies instead of being a puzzle.
    [SerializeField]
    private bool SpawnEnemies;
    [SerializeField]
    private GameObject m_Enemy;
    [SerializeField]
    private float m_SpawnTime = 5.0f;
    [SerializeField]
    private Transform[] m_SpawnPoints;
    [SerializeField]
    private int m_EnemiesToSpawn;

    private int m_EnemiesSpawned = 0;
    private int m_PlayersInSpawn = 0;

    [SerializeField]
    private GameObject[] m_ItemRewards;
    [SerializeField]
    public GameObject rewardParticles;

    protected AudioSource audioSource;
    public AudioClip rewardSound;

    private Vector3 m_TargetPos;
    private Vector3 m_StartPos;

    [SerializeField][Range(1, 15)]
    private float m_OpeningTime = 10;
    private float m_CurrentOpeningTime;

    private AudioSource m_AudioSource;
    private AudioClip m_AudioClip;

    /*[SerializeField][Range(0.1f, 5f)]*/ private float m_ShakeDuration = 0.5f;
    /*[SerializeField][Range(0.01f, 0.5f)]*/ private float m_ShakeMagnitude = 0.1f;

    public enum OpenMethods_t {
        SMOOTHSTEP,
        SMOOTHERSTEP,
        EASE_OUT,
        EASE_IN,
        EXPONENTIAL
    }

    [SerializeField] private OpenMethods_t m_OpeningMethod = OpenMethods_t.SMOOTHERSTEP;

    //public GameObject bubblePart;
    //private bool m_Bubbles;

    //Initialize the button as having not been pressed
    void Awake()
    {
        buttonPressed = false;

        // Set up the lids start end target positions
        m_StartPos = PuzzleLid.transform.position;
        m_TargetPos = m_StartPos;
        m_TargetPos.y += 10;

    }

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();

        m_AudioSource = GetComponent<AudioSource>();
        m_AudioClip = Resources.Load("Sound/SoundEffects/OpenPuzzleLid_00") as AudioClip;
    }

    public void Activate() {
        // Shake the camera
        Camera.main.GetComponent<CameraShaker>().Shake(m_ShakeDuration, m_ShakeMagnitude);
        //Get the material on the GameObject
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Standard");
        rend.material.SetColor("_EmissionColor", Color.cyan);

        if (SpawnEnemies) {
            m_EnemiesToSpawn *= GameController.Instance.TotalPlayersSpawned;
            var spawnTime = m_SpawnTime / GameController.Instance.PlayerInstances.Length;
            InvokeRepeating("Spawn", spawnTime, spawnTime);
            SpawnEnemies = false;
        }

        m_AudioSource.PlayOneShot(m_AudioClip);
        Instantiate(Resources.Load("Particle Systems/BubbleBurstEffect"), GetComponentInParent<Transform>().position, Quaternion.Euler(-90, 0 ,0));
        InvokeRepeating("OpenLid", 0, 0.01f);
    }

    private void OpenLid() {
        m_CurrentOpeningTime += Time.deltaTime;

        var t = m_CurrentOpeningTime / m_OpeningTime;

        switch (m_OpeningMethod) {
            case OpenMethods_t.SMOOTHSTEP: {
                // Normal smoothestep
                t = t * t * (3f - 2f * t);
            } break;

            case OpenMethods_t.SMOOTHERSTEP: {
                t = t * t * t * (t * (6f * t * 15f) + 10f);
            } break;

            case OpenMethods_t.EASE_OUT: {
                t = Mathf.Sin(t * Mathf.PI * 0.5f);
            } break;

            case OpenMethods_t.EASE_IN: {
                t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
            } break;

            case OpenMethods_t.EXPONENTIAL: {
                t = t * t;
            } break;

            default: {
            } break;
        }

        PuzzleLid.transform.position = Vector3.Lerp(m_StartPos, m_TargetPos, t);

        if (!(m_CurrentOpeningTime > m_OpeningTime)) {
            return;
        }

        Destroy(PuzzleLid);
        CancelInvoke("OpenLid");
    }

    private void Spawn()
    {
        if (m_EnemiesSpawned >= m_EnemiesToSpawn || GameController.Instance.PlayerInstances.Length == 0)
        {
            if (m_EnemiesSpawned >= m_EnemiesToSpawn)
            {
                DropRewards();
            }
            CancelInvoke("Spawn");
            return;
        }
        var spawnPointIndex = Random.Range(0, m_SpawnPoints.Length);

        Instantiate(m_Enemy, m_SpawnPoints[spawnPointIndex].position, m_SpawnPoints[spawnPointIndex].rotation);
        m_EnemiesSpawned++;
    }

    private void DropRewards()
    {
        if (m_ItemRewards.Length == 0) {
            return;
        }

        for (var i = 0; i < GameController.Instance.PlayerInstances.Length; i++)
        {
            var itemIndex = Random.Range(0, m_ItemRewards.Length);
            var dropPointIndex = Random.Range(0, m_SpawnPoints.Length);
            var posToSpawn = m_SpawnPoints[dropPointIndex].transform.position;
            posToSpawn.y += 2;
            Instantiate(m_ItemRewards[itemIndex], posToSpawn, Random.rotation);
            Instantiate(rewardParticles, posToSpawn, rewardParticles.transform.rotation); // Instantiate the particle splash effect
        }

        if (audioSource != null) {
            audioSource.PlayOneShot(rewardSound, 0.5f);
        }
    }
}