using Boo.Lang;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController Instance {
        get { return m_instance ?? (m_instance = new GameController()); }
    }

    private static GameController m_instance;

    public Transform[] PlayerInstances { get; private set; }
    
    public void UpdatePlayers() {
        var allPlayers = GameObject.FindGameObjectsWithTag("Player");
        PlayerInstances = new Transform[allPlayers.Length];

        for (var i = 0; i < allPlayers.Length; i++) {
            PlayerInstances[i] = allPlayers[i].transform;
        }
    }
}
