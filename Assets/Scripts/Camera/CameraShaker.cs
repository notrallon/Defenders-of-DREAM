using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour {

    private float m_ShakeTime;
    private float m_CurrentShakeTime;

    private Camera m_Camera;

    private Quaternion m_DefaultRot;

    private bool m_IsShaking = false;

    private float magnitude = 0.2f;

	// Use this for initialization
	void Start () {
	    m_Camera = GetComponent<Camera>();
	    m_DefaultRot = m_Camera.transform.localRotation;
	}

	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.K)) {
	        Shake(1f);
	    }
	}

    public void Shake(float seconds) {
        m_ShakeTime = seconds;
        m_CurrentShakeTime = 0f;
        //InvokeRepeating("ShakeCamera", 0, 0.01f);
        if (!m_IsShaking) {
            StartCoroutine(ShakeIt());
        }
    }

    IEnumerator ShakeIt() {
        m_IsShaking = true;
        float elapsed = 0.0f;
        while (elapsed < m_ShakeTime) {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / m_ShakeTime;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + x, Camera.main.transform.position.y + y, Camera.main.transform.position.z);

            yield return null;
        }

        m_IsShaking = false;
        //Camera.main.transform.position = originalCamPos;
    }
/*
    private void ShakeCamera() {
        m_CurrentShakeTime += Time.deltaTime;

        var t = m_ShakeTime / m_CurrentShakeTime;

        var ShakeX = Random.Range(-0.5f, 0.5f);
        var ShakeY = Random.Range(-0.5f, 0.5f);

        var targetPos = m_Camera.transform.localRotation;

        targetPos.x = ShakeX;
        targetPos.y = ShakeY;
        targetPos.z = 0f;

        m_Camera.transform.localRotation = Quaternion.Lerp(m_Camera.transform.localRotation, targetPos,
            Time.deltaTime);

        if (m_CurrentShakeTime > m_ShakeTime) {
            m_CurrentShakeTime = 0f;
            CancelInvoke("ShakeCamera");
            m_Camera.transform.localRotation = m_DefaultRot;
        }
    }*/
}
