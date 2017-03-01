using UnityEngine;

public class Interactable : MonoBehaviour {
    private Renderer m_Rend;
    private Color[] m_DefaultColours;

    protected GameObject Player;

	// Use this for initialization
    private void Start () {
		m_Rend = GetComponent<Renderer>();

        // Set up an array of colours that is as big as the amount of materials we got
        m_DefaultColours = new Color[m_Rend.materials.Length];

        // Set up all default colours
        for (var i = 0; i < m_DefaultColours.Length; i++) {
            m_DefaultColours[i] = m_Rend.materials[i].color;
        }
    }

    public virtual void Interact() {
    }

    private void OnTriggerStay(Collider col) {
        // Return if collision is not with the player
        if (!col.CompareTag("Player")) return;

        // Return if the player is already trying to interact with something
        var playerInput = col.GetComponent<PlayerInput>();
        if (playerInput.Interact != null) return;

        // Highlight each colour
        for (var i = 0; i < m_DefaultColours.Length; i++) {
            m_Rend.materials[i].color = m_DefaultColours[i] * 2;
        }
        
        // Set playerinteraction to this component
        playerInput.Interact = this;
        Player = col.gameObject;
    }

    private void OnTriggerExit(Collider col) {
        // Return if not colliding with a player
        if (!col.CompareTag("Player")) return;
        
        // Set back to default colour
        for (var i = 0; i < m_DefaultColours.Length; i++) {
            m_Rend.materials[i].color = m_DefaultColours[i];
        }
        
        // The player is no longer interacting
        col.GetComponent<PlayerInput>().Interact = null;
        Player = null;
    }
}
