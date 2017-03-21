using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour {

    //private float m_ShakeTime;
    private float m_CurrentShakeTime;
    private Camera m_Camera;

    private bool m_IsShaking;

    private float magnitude = 0.2f;

    private const float DEFAULT_MAG = 0.02f;
    private const float DEFAULT_TIME = 0.5f;

	// Use this for initialization
	private void Start () {
	    m_Camera = GetComponent<Camera>();
	}

    // Shakes the camera with default values
    public void Shake() {
        m_CurrentShakeTime = 0f;
        if (!m_IsShaking) {
            StartCoroutine(ShakeIt(DEFAULT_TIME, DEFAULT_MAG));
        }
    }

    // Shakes the camera a given amount of seconds, with default magnitude
    public void Shake(float duration) {
        m_CurrentShakeTime = 0f;
        if (!m_IsShaking) {
            StartCoroutine(ShakeIt(duration, DEFAULT_MAG));
        }
    }

    // Shakes the camera for a given amount of seconds and magnitude
    public void Shake(float duration, float magnitude) {
        m_CurrentShakeTime = 0f;

        if (!m_IsShaking) {
            StartCoroutine(ShakeIt(duration, magnitude));
        }
    }

    IEnumerator ShakeIt(float seconds, float magnitude) {
        m_IsShaking = true;
        float elapsed = 0.0f;
        while (elapsed < seconds) {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / seconds;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            m_Camera.transform.position = new Vector3(m_Camera.transform.position.x + x, m_Camera.transform.position.y + y, m_Camera.transform.position.z);

            yield return null;
        }
        m_IsShaking = false;
    }
}
