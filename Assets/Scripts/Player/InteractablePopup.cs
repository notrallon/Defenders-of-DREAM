using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePopup : MonoBehaviour {
    private Canvas PopupCanvas;
    private CanvasRenderer m_PopupRenderer;

    private Vector3 m_Scale;
    
    private float m_ScaleMax = 0.4f;

    private Vector3 m_VScaleMin;
    private Vector3 m_VScaleMax;
    
    private float m_PulseTime = 0.15f;
    private float m_ScaleTime = 0.5f;
    private float m_CurrentScaleTime;

    private float m_WaitTime = 0.25f;

    public float MinPulseScale = 0.65f;

    private float m_Smoothing = 2.0f;
    private Vector3 m_LastPosition;

    private Transform m_TargetTransform;

	// Use this for initialization
	void Start () {
	    m_VScaleMin = Vector3.zero;
	    m_VScaleMax = new Vector3(m_ScaleMax, m_ScaleMax, m_ScaleMax);

	    PopupCanvas = Instantiate(Resources.Load("UI/InteractablePopup", typeof(Canvas))) as Canvas;
	    if (PopupCanvas != null) {
	        PopupCanvas.enabled = false;

	        m_LastPosition = Vector3.zero;
	        m_PopupRenderer = PopupCanvas.GetComponentInChildren<CanvasRenderer>();
	    }
	    m_Scale = m_PopupRenderer.transform.localScale;

        m_TargetTransform = transform;
        m_LastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // Check the players position relative to camera screenpoint
        //Vector3 pos = new Vector3();

	    if (m_TargetTransform != null) {
            m_PopupRenderer.transform.position = Camera.main.WorldToScreenPoint(m_LastPosition);
	        m_LastPosition = m_TargetTransform.position;
	    } else {
	        m_PopupRenderer.transform.position = Camera.main.WorldToScreenPoint(m_LastPosition);
	    }
    }

    public void Activate(Transform trans) {
        m_TargetTransform = trans;
        PopupCanvas.enabled = true;

        m_Scale = m_PopupRenderer.transform.localScale = Vector3.zero;
        m_CurrentScaleTime = 0.0f;

        //Debug.Log(m_Scale.magnitude);

        CancelInvoke();
        InvokeRepeating("ScaleUp", 0, 0.01f);
    }

    public void Deactivate() {
        CancelInvoke("ScaleUp");
        CancelInvoke("Pulse");
        m_TargetTransform = null;
        m_CurrentScaleTime = 0f;
        InvokeRepeating("ScaleDown", 0, 0.01f);
    }

    private void Pulse() {
        m_CurrentScaleTime += Time.deltaTime;
        float perc = m_CurrentScaleTime / m_PulseTime;
        float dist = 1f + MinPulseScale * Mathf.Cos(perc);
        m_PopupRenderer.transform.localScale = m_VScaleMax * Mathf.PingPong(dist, 1.5f);
        //m_PopupRenderer.transform.localScale = m_VScaleMax * Mathf.Lerp(m_PopupRenderer.transform.localScale.magnitude, Mathf.Abs(dist), perc);
    }

    private void ScaleUp() {
        if (m_WaitTime > 0) {
            m_WaitTime -= Time.deltaTime;
            return;
        }

        m_CurrentScaleTime += Time.deltaTime;
        float perc = m_CurrentScaleTime / m_ScaleTime;
        float dist = 1f + MinPulseScale * Mathf.Sin(perc);
        
        m_PopupRenderer.transform.localScale = m_VScaleMax * Mathf.Lerp(m_PopupRenderer.transform.localScale.magnitude, Mathf.Abs(dist), perc);

        if (m_CurrentScaleTime > m_ScaleTime) {
            m_CurrentScaleTime = m_PulseTime;
            InvokeRepeating("Pulse", 1.5f, 0.01f);
            CancelInvoke("ScaleUp");
        }
    }

    private void ScaleDown() {
        m_CurrentScaleTime += Time.deltaTime;

        float perc = m_CurrentScaleTime / m_ScaleTime;

        m_Scale = Vector3.Lerp(m_VScaleMax, m_VScaleMin, perc);

        m_PopupRenderer.transform.localScale = m_Scale;

        if (m_CurrentScaleTime > m_ScaleTime) {
            m_CurrentScaleTime = 0f;
            m_WaitTime = 1f;
            PopupCanvas.enabled = false;
            CancelInvoke("ScaleDown");
        }
    }
}
