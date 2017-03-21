using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField] private GameObject m_Enemy;
    [SerializeField] private float m_SpawnTime = 5.0f;
    [SerializeField] private Transform[] m_SpawnPoints;
    [SerializeField] private int m_EnemiesToSpawn;

    private int m_EnemiesSpawned = 0;
    public int EnemiesAlive = 0;
    private int m_PlayersInSpawn = 0;

    [SerializeField] private GameObject[] m_ItemRewards;
    [SerializeField] public GameObject rewardParticles;

    protected AudioSource audioSource;
    public AudioClip rewardSound;

    // Use this for initialization
    private void Start () {
        //InvokeRepeating("Spawn", m_SpawnTime, m_SpawnTime);
        audioSource = GetComponent<AudioSource>();
    }

    private void Spawn() {
        if (m_EnemiesSpawned >= m_EnemiesToSpawn || GameController.Instance.PlayerInstances.Length == 0) {
            if (m_EnemiesSpawned >= m_EnemiesToSpawn && EnemiesAlive == 0) {
                DropRewards();
                CancelInvoke();
            }
            return;
        }

        var spawnPointIndex = Random.Range(0, m_SpawnPoints.Length);

        var enemy = Instantiate(m_Enemy, m_SpawnPoints[spawnPointIndex].position, m_SpawnPoints[spawnPointIndex].rotation);
        enemy.GetComponent<EnemyBase>().SetEnemyManagerIndex(this);
        m_EnemiesSpawned++;
        EnemiesAlive++;
    }

    private void OnTriggerEnter(Collider col) {
        if (!col.CompareTag("Player")) return;
        m_EnemiesToSpawn *= GameController.Instance.TotalPlayersSpawned;
        var spawnTime = m_SpawnTime / GameController.Instance.TotalPlayersSpawned;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        Camera.main.GetComponent<CameraShaker>().Shake(1f);
        GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerExit(Collider col) {
        if (!col.CompareTag("Player")) return;
        m_PlayersInSpawn--;

        if (m_PlayersInSpawn < 0) {
            m_PlayersInSpawn = 0;
        }
    }

    private void DropRewards() {
        if (m_ItemRewards.Length == 0) return;
        for (var i = 0; i < GameController.Instance.TotalPlayersSpawned; i++) {
            var itemIndex = Random.Range(0, m_ItemRewards.Length);
            var dropPointIndex = Random.Range(0, GameController.Instance.PlayerInstances.Length);
            var posToSpawn = GameController.Instance.PlayerInstances[dropPointIndex].transform.position;
            posToSpawn.y += 2;
            posToSpawn.z += Random.Range(-2f, 2f);
            posToSpawn.x += Random.Range(-2f, 2f);
            Instantiate(m_ItemRewards[itemIndex], posToSpawn, Random.rotation);
            GameObject chime = Instantiate(rewardParticles, posToSpawn, rewardParticles.transform.rotation) as GameObject; // Instantiate the particle splash effect
            audioSource.PlayOneShot(rewardSound, 0.5f);
        }
    }

}
