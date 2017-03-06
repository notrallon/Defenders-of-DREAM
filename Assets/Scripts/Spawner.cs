using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnLocation;
    public GameObject[] whatToSpawnPrefab;
    public GameObject whatToSpawnClone;
    //public GameObject prefab;
    public KeyCode spawnButton;
    public KeyCode spawnKey;

    public PlayerIndex playerIndex;
    public InputMethod_t InputMethod;

    private void Update() {
        switch (InputMethod) {
            case InputMethod_t.INPUT_MANAGER: {
                //If Player presses Start button, the player gets spawned
                if (Input.GetKeyDown(spawnButton) || Input.GetKeyDown(spawnKey)) {
                    //Only if prefab doesn't exists.
                    if (whatToSpawnClone == null) {
                        SpawnPlayer();
                        GameController.Instance.UpdatePlayers();
                    }
                }
            } break;

            case InputMethod_t.X_INPUT: {
                var state = GamePad.GetState(playerIndex);

                if (state.Buttons.Start == ButtonState.Pressed) {
                    if (whatToSpawnClone == null) {
                        SpawnPlayer();
                        GameController.Instance.UpdatePlayers();
                    }
                }
            } break;

            default: {
                
            } break;
        }


    }
    //Spawn Player prefab at spawnLocation and sets it's rotation.
    void SpawnPlayer()
    {
        whatToSpawnClone = Instantiate(whatToSpawnPrefab[0], spawnLocation[0].transform.position, Quaternion.Euler(0, 0, 0));
    }
}
