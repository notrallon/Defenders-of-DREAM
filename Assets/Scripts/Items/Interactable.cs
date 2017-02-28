using UnityEngine;

public class Interactable : MonoBehaviour {
    private Renderer m_Rend;
    private Color[] m_DefaultColours;

    protected GameObject Player;

	// Use this for initialization
    private void Start () {
		m_Rend = GetComponent<Renderer>();
        m_DefaultColours = new Color[m_Rend.materials.Length];

        for (var i = 0; i < m_DefaultColours.Length; i++) {
            m_DefaultColours[i] = m_Rend.materials[i].color;
        }
    }

    public virtual void Interact() {
    }

    private void OnTriggerStay(Collider col) {
        if (!col.CompareTag("Player")) return;
        var playerInput = col.GetComponent<PlayerInput>();

        if (playerInput.Interact != null) return;

        for (var i = 0; i < m_DefaultColours.Length; i++) {
            m_Rend.materials[i].color = m_DefaultColours[i] * 3;
        }

        //m_Rend.material.color = m_DefaultColor * 3;
        playerInput.Interact = this;
        Player = col.gameObject;
    }

    private void OnTriggerExit(Collider col) {
        if (!col.CompareTag("Player")) return;

        for (var i = 0; i < m_DefaultColours.Length; i++) {
            m_Rend.materials[i].color = m_DefaultColours[i];
        }

        // m_Rend.material.color = m_DefaultColor;
        col.GetComponent<PlayerInput>().Interact = null;
        Player = null;
    }
}
