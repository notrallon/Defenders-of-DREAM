using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePopup : MonoBehaviour {

    public Canvas PopupCanvas;
    private CanvasRenderer m_PopupRenderer;

    private Vector3 m_Scale;
    private float m_ScaleMax = 1f;
    private float m_ScaleMin = 0.1f;

    private float m_Smoothing = 2.0f;
    private Vector3 m_LastPosition;

    private Transform m_TargetTransform;

	// Use this for initialization
	void Start () {
	    PopupCanvas = Instantiate(PopupCanvas);
	    PopupCanvas.enabled = false;

	    m_LastPosition = Vector3.zero;
	    m_PopupRenderer = PopupCanvas.GetComponentInChildren<CanvasRenderer>();
	    m_Scale = m_PopupRenderer.transform.localScale;

        m_TargetTransform = transform;
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

        //Debug.Log(m_Scale.magnitude);

        CancelInvoke();
        InvokeRepeating("ScaleUp", 0, 0.01f);
    }

    public void Deactivate() {
        CancelInvoke();
        m_TargetTransform = null;
        InvokeRepeating("ScaleDown", 0, 0.01f);
    }

    private void ScaleUp() {
        if (m_Scale.magnitude > m_ScaleMax) {
            CancelInvoke("ScaleUp");
            return;
        }

        m_Scale = Vector3.Lerp(m_PopupRenderer.transform.localScale, new Vector3(m_ScaleMax, m_ScaleMax, m_ScaleMax),
            m_Smoothing * Time.deltaTime);

        m_PopupRenderer.transform.localScale = m_Scale;
    }

    private void ScaleDown() {
        if (m_Scale.magnitude < 0.4f) {
            PopupCanvas.enabled = false;
            CancelInvoke("ScaleDown");
            return;
        }

        m_Scale = Vector3.Lerp(m_PopupRenderer.transform.localScale, new Vector3(m_ScaleMin, m_ScaleMin, m_ScaleMin),
            m_Smoothing * Time.deltaTime);

        m_PopupRenderer.transform.localScale = m_Scale;
    }
}
