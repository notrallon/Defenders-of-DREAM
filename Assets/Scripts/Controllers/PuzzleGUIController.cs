using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGUIController : MonoBehaviour {

    public static PuzzleGUIController Instance { get { return m_Instance ?? (m_Instance = new PuzzleGUIController()); } }

    private static PuzzleGUIController m_Instance;

    public static string PuzzleGUIString;
    private static Text PuzzleGUIText;

    private GameObject m_ObjectivesDone;

    // Update is called once per frame
	void Update () {
		
	}

    public void UpdatePuzzleGUIText() {
        PuzzleGUIString = "Puzzles: " + GameController.Instance.PuzzlesSolved + " / " +
                          GameController.Instance.PuzzlesTotal;

        if (PuzzleGUIText == null) {
            PuzzleGUIText = GameObject.FindGameObjectWithTag("PuzzleGUI").GetComponentInChildren<Text>();
        }

        PuzzleGUIText.text = PuzzleGUIString;

        if (m_ObjectivesDone == null) {
            m_ObjectivesDone = GameObject.FindGameObjectWithTag("ObjectivesDone");
        }

        if (GameController.Instance.PuzzlesSolved != GameController.Instance.PuzzlesTotal ||
            m_ObjectivesDone.GetComponent<LerpSignsUpwards>().activate) {
            return;
        }
        m_ObjectivesDone.GetComponent<LerpSignsUpwards>().ActivateSigns();
        m_ObjectivesDone.GetComponent<LerpSignsUpwards>().activate = true;
    }
}
