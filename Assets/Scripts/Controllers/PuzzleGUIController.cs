using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGUIController : MonoBehaviour {

    public static PuzzleGUIController Instance { get { return m_Instance ?? (m_Instance = new PuzzleGUIController()); } }

    private static PuzzleGUIController m_Instance;

    public static string PuzzleGUIString;
    private static Text PuzzleGUIText;

    // Update is called once per frame
	void Update () {
		
	}

    public void UpdatePuzzleGUIText() {
        PuzzleGUIString = "Puzzles solved: " + GameController.Instance.PuzzlesSolved + " / " +
                          GameController.Instance.PuzzlesTotal;

        if (PuzzleGUIText == null) {
            PuzzleGUIText = GameObject.FindGameObjectWithTag("PuzzleGUI").GetComponentInChildren<Text>();
        }

        PuzzleGUIText.text = PuzzleGUIString;
    }
}
