using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLinesChangeColor : MonoBehaviour {

    private Color m_DefaultColor;
    private Color m_TargetColor;
    private Color m_DefaultEmissionColor;

    private float m_FadeTime;
    private float m_CurrentFadeTime;

	// Use this for initialization
	void Start () {
	    m_DefaultColor = GetComponent<Renderer>().material.color;
	    m_DefaultEmissionColor = GetComponent<Renderer>().material.GetColor("_EmissionColor");
	    m_TargetColor = Color.cyan;
	}

    public void Activate(float duration) {
        m_FadeTime = duration;
        InvokeRepeating("ChangeColor", 0f, 0.01f);
    }

    private void ChangeColor() {
        m_CurrentFadeTime += Time.deltaTime;
        var t = m_CurrentFadeTime / m_FadeTime;

        GetComponent<Renderer>().material.color = Color.Lerp(m_DefaultColor, m_TargetColor, t);
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(m_DefaultEmissionColor, m_TargetColor, t));

        if (m_CurrentFadeTime > m_FadeTime) {
            CancelInvoke("ChangeColor");
        }
    }
}
