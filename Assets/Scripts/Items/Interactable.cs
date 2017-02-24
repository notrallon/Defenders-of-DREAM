using UnityEngine;

public class Interactable : MonoBehaviour {
    private Renderer m_Rend;
    private Color m_DefaultColor;

    protected GameObject Player;

	// Use this for initialization
    private void Start () {
		m_Rend = GetComponent<Renderer>();
        m_DefaultColor = m_Rend.material.color;
    }

    public virtual void Interact() {
    }

    private void OnTriggerStay(Collider col) {
        if (!col.CompareTag("Player")) return;
        var playerInput = col.GetComponent<PlayerInput>();

        if (playerInput.Interact != null) return;
        m_Rend.material.color = m_DefaultColor * 3;
        playerInput.Interact = this;
        Player = col.gameObject;
    }

    private void OnTriggerExit(Collider col) {
        if (!col.CompareTag("Player")) return;

        m_Rend.material.color = m_DefaultColor;
        col.GetComponent<PlayerInput>().Interact = null;
        Player = null;
    }
}
