﻿/// <summary>
/// This script checks if the block GameObject has been pushed into the trigger. 
/// Once it's in the trigger the wall that blocks the player's progression
/// will be lowered.
/// </summary>

using UnityEngine;

public class FinishTriggerCheck : MonoBehaviour
{
    //Put the PuzzleCube prefab in this field in the inspector
    [SerializeField]
    private GameObject block;
    //The wall GameObject is the wall behind the puzzle that blocks the player from
    //continuing without having finished the puzzle.
    [SerializeField]
    private GameObject wall;
    //speed decides how fast the wall travel downwards once the puzzle is finished
    [SerializeField]
    private float speed;
    //PuzzleIsFinished is used to tell when the wall should move downwards once the puzzle is finished
    private bool PuzzleIsFinished;

    void Awake()
    {
        PuzzleIsFinished = false;
    }
    //When the puzzle cube is in the trigger the PuzzleIsFinished is switched to true 
    void OnTriggerEnter(Collider block)
    {
        PuzzleIsFinished = true;
    }
    void Update()
    {
        if (PuzzleIsFinished == true)
        {
            //Move the PuzzleLid GameObject downwards
            //(the directions are for some reason scued and needs X to be -15 and Z to be 60 to let the Y axis move straight up/down)
            wall.transform.position = Vector3.MoveTowards(wall.transform.position, new Vector3(-15, -100, 60), Time.deltaTime * speed);
        }
    }
}

