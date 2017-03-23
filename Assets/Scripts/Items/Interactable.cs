using UnityEngine;

public class Interactable : MonoBehaviour {
    private Renderer m_Rend;
    protected Color[] m_DefaultColours;
    protected Color[] m_DefaultEmissionColours;
    protected Material[] m_DefaultMaterials;

    [SerializeField][Range(1f, 5f)]
    private float m_ColorMultiplier = 2.5f;
    [SerializeField][Range(1f, 5f)]
    private float m_EmissionMultiplier = 2.5f;

    protected GameObject Player;

	// Use this for initialization
    protected virtual void Start () {
		m_Rend = GetComponent<Renderer>();

        // Set up an array of colours that is as big as the amount of materials we got
        m_DefaultColours = new Color[m_Rend.materials.Length];
        m_DefaultEmissionColours = new Color[m_Rend.materials.Length];

        // Set up all default colours
        for (var i = 0; i < m_DefaultColours.Length; i++) {
            m_DefaultColours[i] = m_Rend.materials[i].color;
            m_DefaultEmissionColours[i] = m_Rend.materials[i].GetColor("_EmissionColor");
            //m_DefaultMaterials[i] = m_Rend.materials[i];
        }
    }

    public virtual void Interact() {
    }

    public void DisableInteraction() {
        Player.GetComponent<InteractablePopup>().Deactivate();
    }

    private void OnTriggerStay(Collider col) {
        // Return if collision is not with the player
        if (!col.CompareTag("Player")) return;

        // Return if the player is already trying to interact with something
        var playerInput = col.GetComponent<PlayerInput>();
        if (playerInput.Interact != null) return;

        col.GetComponent<InteractablePopup>().Activate(transform);

        // Highlight each colour
        for (var i = 0; i < m_DefaultColours.Length; i++) {
            m_Rend.materials[i].color = m_DefaultColours[i] * m_ColorMultiplier;
            m_Rend.materials[i].SetColor("_EmissionColor", m_DefaultEmissionColours[i] * m_EmissionMultiplier);
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
            m_Rend.materials[i].SetColor("_EmissionColor", m_DefaultEmissionColours[i]);
            m_Rend.materials[i].color = m_DefaultColours[i];
        }
        
        col.GetComponent<InteractablePopup>().Deactivate();

        // The player is no longer interacting
        col.GetComponent<PlayerInput>().Interact = null;
        Player = null;
    }

    public virtual void SetPickupPlayerColor(Material playerColorMaterial) {
        
    }
}
