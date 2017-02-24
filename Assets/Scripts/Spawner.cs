using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnLocation;
    public GameObject[] whatToSpawnPrefab;
    public GameObject whatToSpawnClone;
    //public GameObject prefab;
    public KeyCode spawnButton;
    public KeyCode spawnKey;

    private void Update()
    {
        //If Player presses Start button, the player gets spawned
        if ((Input.GetKeyDown(spawnButton)) || (Input.GetKeyDown(spawnKey)))
        {
            //Only if prefab doesn't exists.
            if (whatToSpawnClone == null)
            {
                SpawnPlayer();
                GameController.Instance.UpdatePlayers();
            }
        }
    }
    //Spawn Player prefab at spawnLocation and sets it's rotation.
    void SpawnPlayer()
    {
        whatToSpawnClone = Instantiate(whatToSpawnPrefab[0], spawnLocation[0].transform.position, Quaternion.Euler(0, 0, 0));
    }
}
