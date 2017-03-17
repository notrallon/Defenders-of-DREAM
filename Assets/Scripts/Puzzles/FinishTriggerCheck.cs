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
    private Vector3 m_TargetPos;

    void Awake()
    {
        PuzzleIsFinished = false;
        m_TargetPos = wall.transform.position;
        m_TargetPos.y -= 2;
    }
    //When the puzzle cube is in the trigger the PuzzleIsFinished is switched to true 
    void OnTriggerEnter(Collider other)
    {
        //if (other.GetComponent<BoxCollider>() == block.GetComponent<BoxCollider>()) {
            PuzzleIsFinished = true;
            Destroy(other.GetComponent<Rigidbody>());
            GameController.Instance.PuzzlesSolved++;
            PuzzleGUIController.Instance.UpdatePuzzleGUIText();
        //}
    }
    void Update()
    {
        if (PuzzleIsFinished == true)
        {
            //Move the PuzzleLid GameObject downwards
            //(the directions are for some reason scued and needs X to be -15 and Z to be 60 to let the Y axis move straight up/down)
            wall.transform.position = Vector3.Lerp(wall.transform.position, m_TargetPos, Time.deltaTime * speed);
        }
    }
}

