using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField] private GameObject m_Enemy;
    [SerializeField] private float m_SpawnTime = 5.0f;
    [SerializeField] private Transform[] m_SpawnPoints;
    [SerializeField] private int m_EnemiesToSpawn;

    private int m_EnemiesSpawned = 0;

    private int m_PlayersInSpawn = 0;

	// Use this for initialization
    private void Start () {
		//InvokeRepeating("Spawn", m_SpawnTime, m_SpawnTime);
	}

    private void Spawn() {
        if (m_EnemiesSpawned >= m_EnemiesToSpawn || GameController.Instance.PlayerInstances.Length == 0) return;

        var spawnPointIndex = Random.Range(0, m_SpawnPoints.Length);

        Instantiate(m_Enemy, m_SpawnPoints[spawnPointIndex].position, m_SpawnPoints[spawnPointIndex].rotation);
        m_EnemiesSpawned++;
    }

    private void OnTriggerEnter(Collider col) {
        if (!col.CompareTag("Player")) return;
        m_EnemiesToSpawn *= GameController.Instance.PlayerInstances.Length;
        var spawnTime = m_SpawnTime / GameController.Instance.PlayerInstances.Length;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        Destroy(GetComponent<BoxCollider>());
    }

    private void OnTriggerExit(Collider col) {
        if (!col.CompareTag("Player")) return;
        m_PlayersInSpawn--;

        if (m_PlayersInSpawn < 0) {
            m_PlayersInSpawn = 0;
        }
    }

}
