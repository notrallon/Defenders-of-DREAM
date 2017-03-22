using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour {
    public Canvas EndGameFadeCanvas;
    public CanvasRenderer FadeRenderer;

    private RawImage image;

    public float FADE_IN_TIME = 2f;
    private float m_CurrentFadeInTime;

	// Use this for initialization
	void Start () {
        image = EndGameFadeCanvas.GetComponentInChildren<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerEnter(Collider col)
    {
        StartCoroutine(FadeToBlackCoroutine(FadeRenderer));
    }

    IEnumerator FadeToBlackCoroutine(CanvasRenderer FadeRenderer)
    {
        while(m_CurrentFadeInTime < FADE_IN_TIME)
        {
            m_CurrentFadeInTime += Time.deltaTime;
            var percent = m_CurrentFadeInTime / FADE_IN_TIME;
            var color = image.color;
            color.a = Mathf.Lerp(0f, 1f, percent);
            image.color = color;
            yield return new WaitForSeconds(0.01f);
        }
            
        yield return null;
        
    }
}

