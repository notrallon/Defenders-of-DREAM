using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour {
    private GameObject EndGameFadeCanvas;
    private Canvas EndGameCanvas;

    private RawImage m_Image;

    public float FADE_IN_TIME = 2f;
    private float m_CurrentFadeInTime;

	// Use this for initialization
	void Start () {
	    EndGameFadeCanvas = Instantiate(Resources.Load("UI/EndGameFadeCanvas")) as GameObject;
	    EndGameCanvas = EndGameFadeCanvas.GetComponent<Canvas>();
        m_Image = EndGameCanvas.GetComponentInChildren<RawImage>();
	}
//
//	// Update is called once per frame
//	void Update () {
//
//	}
//
//    private void OnTriggerEnter(Collider col)
//    {
//        StartCoroutine(FadeToBlackCoroutine(FadeRenderer));
//    }

    public void FadeToBlack() {
        StartCoroutine(FadeToBlackCoroutine());
    }

    IEnumerator FadeToBlackCoroutine()
    {
        while(m_CurrentFadeInTime < FADE_IN_TIME)
        {
            m_CurrentFadeInTime += Time.deltaTime;
            var percent = m_CurrentFadeInTime / FADE_IN_TIME;
            var color = m_Image.color;
            color.a = Mathf.Lerp(0f, 1f, percent);
            m_Image.color = color;
            yield return new WaitForSeconds(0.01f);
        }
            
        yield return null;
        
    }
}

