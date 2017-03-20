using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {

    public Transform PauseMenu;

    private const float SLIDE_TIME = 0.25f;
    private float m_CurrentSlideTime = 0f;

    private const float X_SLIDE_OUT_TARGET = -1300f;
    private const float X_SLIDE_IN_START = 1300f;
    private const float X_SLIDE_IN_TARGET = 0f;
    
    void Start() {
        Time.timeScale = 1;
        PauseMenu.gameObject.SetActive(false);//turen off canvas at start of level
        var pos = PauseMenu.GetComponent<RectTransform>().anchoredPosition;

        pos.x = X_SLIDE_OUT_TARGET;
        PauseMenu.GetComponent<RectTransform>().localPosition = pos;
    }

    void Update () {
        //if you press escape on keyboard or start on gamepad
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("joybutton7")) {
            pause(); //run pause
        }


        /*if (Input.GetButtonDown("joybutton7"))
        {
            pause();//run pause
        }*/
	}

    public void pause()
    {
        if (PauseMenu.gameObject.activeInHierarchy == false) {
            PauseMenu.FindChild("Resume").GetComponent<Button>().Select();
            PauseMenu.FindChild("Resume").GetComponent<Button>().OnSelect(null);
            PauseMenu.gameObject.SetActive(true);//make canvas viseble
            //InvokeRepeating("PauseSlideIn", 0, 0.01f);
            StartCoroutine(PauseSlideIn(X_SLIDE_IN_START, X_SLIDE_IN_TARGET, 0.01f));
            Time.timeScale = 0;//stop time
        }
        else
        {
            //InvokeRepeating("PauseSlideOut", 0, 0.01f);
            StartCoroutine(PauseSlideOut());
            //PauseMenu.gameObject.SetActive(false);
            //Time.timeScale = 1;//turn on time
        }
    }
    
    public void LoadScene(string name)
    {
        //load level were name = the name of the level
        GameController.Instance.ResetDefault();
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        Application.LoadLevel(name);
    }

    public void RestartScene() {
        GameController.Instance.ResetDefault();

        PauseMenu.gameObject.SetActive(false);
        // Load current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    private IEnumerator PauseSlideIn(float startPos, float targetPos, float time) {
        while (m_CurrentSlideTime < SLIDE_TIME) {
            m_CurrentSlideTime += Time.unscaledDeltaTime;
            var t = m_CurrentSlideTime / SLIDE_TIME;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            var pos = PauseMenu.GetComponent<RectTransform>().anchoredPosition;

            pos.x = Mathf.Lerp(startPos, targetPos, t);
            PauseMenu.GetComponent<RectTransform>().anchoredPosition = pos;
            
            //CancelInvoke("PauseSlideIn");
            yield return new WaitForSecondsRealtime(time);
        }
        m_CurrentSlideTime = 0f;
        //StopCoroutine(PauseSlideIn());
    }

    private IEnumerator PauseSlideOut() {
        while (m_CurrentSlideTime < SLIDE_TIME) {
            m_CurrentSlideTime += Time.unscaledDeltaTime;
            var t = m_CurrentSlideTime / SLIDE_TIME;
            
            t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
            var pos = PauseMenu.GetComponent<RectTransform>().anchoredPosition;

            pos.x = Mathf.Lerp(X_SLIDE_IN_TARGET, X_SLIDE_OUT_TARGET, t);
            PauseMenu.GetComponent<RectTransform>().anchoredPosition = pos;


            //CancelInvoke("PauseSlideOut");

            yield return new WaitForSecondsRealtime(0.01f);
        }
        m_CurrentSlideTime = 0f;
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        //StopCoroutine(PauseSlideOut());

    }
    
        /*
    private void PauseSlideIn() {
        Debug.Log("YO");
        m_CurrentSlideTime += Time.unscaledDeltaTime;
        var t = m_CurrentSlideTime / SLIDE_TIME;
        var pos = PauseMenu.GetComponent<RectTransform>().anchoredPosition;

        pos.x = Mathf.Lerp(X_SLIDE_OUT_TARGET, X_SLIDE_IN_TARGET, t);
        PauseMenu.GetComponent<RectTransform>().anchoredPosition = pos;

        if (m_CurrentSlideTime > SLIDE_TIME) {
            m_CurrentSlideTime = 0f;
            CancelInvoke("PauseSlideIn");
        }
    }

    private void PauseSlideOut() {
        m_CurrentSlideTime += Time.unscaledDeltaTime;
        var t = m_CurrentSlideTime / SLIDE_TIME;
        var pos = PauseMenu.GetComponent<RectTransform>().anchoredPosition;

        pos.x = Mathf.Lerp(X_SLIDE_IN_TARGET, X_SLIDE_OUT_TARGET, t);
        PauseMenu.GetComponent<RectTransform>().anchoredPosition = pos;

        if (m_CurrentSlideTime > SLIDE_TIME) {
            m_CurrentSlideTime = 0f;
            PauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1f;
            CancelInvoke("PauseSlideOut");
        }
    }*/
}
