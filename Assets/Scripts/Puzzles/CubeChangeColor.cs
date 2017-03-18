/// <summary>
/// This script makes the PuzzleCube glow by changing it's material
/// once it has been pushed into the PuzzleFinish trigger. 
/// </summary>

using UnityEngine;

public class CubeChangeColor : MonoBehaviour {
    private Renderer m_Renderer;
    private Color m_DefaultColor;
    private Color m_FinishedColor;

    private float m_FadeInTime = 2f;
    private float m_CurrentFadeInTime;

    private void Start() {
        m_Renderer  = GetComponent<Renderer>();
        m_DefaultColor = m_Renderer.material.GetColor("_EmissionColor");
        m_FinishedColor = Color.cyan;
    }

    public void ChangeColor() {
        m_Renderer.material.shader = Shader.Find("Standard");
        //rend.material.SetColor("_EmissionColor", Color.cyan);

        InvokeRepeating("FadeInColor", 0, 0.01f);
    }

    private void FadeInColor() {
        m_CurrentFadeInTime += Time.deltaTime;
        var t = m_CurrentFadeInTime / m_FadeInTime;

        t = Mathf.Sin(t * Mathf.PI * 0.5f);

        m_Renderer.material.SetColor("_EmissionColor", Color.Lerp(m_DefaultColor, m_FinishedColor, t));

        if (m_CurrentFadeInTime > m_FadeInTime) {
            CancelInvoke("FadeInColor");
        }
    }
}
