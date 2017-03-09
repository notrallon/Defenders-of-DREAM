/// <summary>
/// This script moves the PuzzleLid upwards once the player has moved within the trigger
/// and has pressed the button, allowing the player(s) to take on the puzzle or event
/// that's hidden under the PuzzleLid GameObject.
/// </summary>

using UnityEngine;

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

    //Initialize the button as having not been pressed
    void Awake()
    {
        buttonPressed = false;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Checks if someone enteres the triggerbox for the button
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //If someone presses A on any controller or E on keyboard the button is set to having been pressed (true)
            if (Input.GetKey(KeyCode.JoystickButton0) || (Input.GetKey(KeyCode.E)))
            {
                buttonPressed = true;

                if (SpawnEnemies == true)
                {
                    if (!col.CompareTag("Player")) return;
                    m_EnemiesToSpawn *= GameController.Instance.PlayerInstances.Length;
                    var spawnTime = m_SpawnTime / GameController.Instance.PlayerInstances.Length;
                    InvokeRepeating("Spawn", spawnTime, spawnTime);
                    GetComponent<BoxCollider>().enabled = false;
                }
                if (!col.CompareTag("Player")) return;
                m_PlayersInSpawn--;

                if (m_PlayersInSpawn < 0)
                {
                    m_PlayersInSpawn = 0;
                }
            }
        }
    }

    void Update()
    {
        //When someone has pressed the button
        if (buttonPressed == true)
        {
            //Move the PuzzleLid GameObject upwards
            //(the directions are for some reason scued and need x to be -15 and z to be 60 to let the y axis move straight up/down)
            PuzzleLid.transform.position = Vector3.MoveTowards(PuzzleLid.transform.position, new Vector3(-60, 100, -20), Time.deltaTime * smooth);
            //Get the material on the GameObject
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Standard");
            rend.material.SetColor("_EmissionColor", Color.cyan);

            //Destroy(gameObject, 5f);
        }
    }

    private void Spawn()
    {
        if (m_EnemiesSpawned >= m_EnemiesToSpawn || GameController.Instance.PlayerInstances.Length == 0)
        {
            if (m_EnemiesSpawned >= m_EnemiesToSpawn)
            {
                //DropRewards();
            }
            CancelInvoke();
            return;
        }
        var spawnPointIndex = Random.Range(0, m_SpawnPoints.Length);

        Instantiate(m_Enemy, m_SpawnPoints[spawnPointIndex].position, m_SpawnPoints[spawnPointIndex].rotation);
        m_EnemiesSpawned++;
    }



    private void DropRewards()
    {
        if (m_ItemRewards.Length == 0) return;
        for (var i = 0; i < GameController.Instance.PlayerInstances.Length; i++)
        {
            var itemIndex = Random.Range(0, m_ItemRewards.Length);
            var dropPointIndex = Random.Range(0, m_SpawnPoints.Length);
            var posToSpawn = m_SpawnPoints[dropPointIndex].transform.position;
            posToSpawn.y += 2;
            Instantiate(m_ItemRewards[itemIndex], posToSpawn, Random.rotation);
            GameObject chime = Instantiate(rewardParticles, posToSpawn, rewardParticles.transform.rotation) as GameObject; // Instantiate the particle splash effect
            audioSource.PlayOneShot(rewardSound, 0.5f);
        }
    }
}