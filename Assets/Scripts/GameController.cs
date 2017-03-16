using UnityEngine;

/*
===============================================================================

	GameController is a singleton class that holds valuable information about
    the game.

===============================================================================
*/
public class GameController : MonoBehaviour {

    public int PuzzlesTotal { get; private set; }
    public int PuzzlesSolved;

    // Return this instance.
    public static GameController Instance {
        get { return m_instance ?? (m_instance = new GameController()); }
    }

    private static GameController m_instance;
    
    // Array of transforms that holds the transform of all player instances in the game
    public Transform[] PlayerInstances { get; private set; }
    
    public void UpdatePlayers() {
        // Look through the scene and find all gameobjects with the player tag and put them in the playerinstances array.
        var allPlayers = GameObject.FindGameObjectsWithTag("Player");
        PlayerInstances = new Transform[allPlayers.Length];

        for (var i = 0; i < allPlayers.Length; i++) {
            PlayerInstances[i] = allPlayers[i].transform;
        }
    }

    public void FindAllPuzzles() {
        PuzzlesTotal = GameObject.FindGameObjectsWithTag("PuzzleFinish").Length;
        PuzzleGUIController.Instance.UpdatePuzzleGUIText();
    }
}
