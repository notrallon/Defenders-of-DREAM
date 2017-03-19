using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {

    public Transform PauseMenu;

    
    void Start() {
        Time.timeScale = 1;
        PauseMenu.gameObject.SetActive(false);//turen off canvas at start of level
    }
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))//if you press escape
        {
            pause();//run pause
        }
        if (Input.GetButtonDown("joybutton7"))
        {
            pause();//run pause
        }
       
	}
    public void pause()
    {
        if (PauseMenu.gameObject.activeInHierarchy == false) {
            PauseMenu.FindChild("Resume").GetComponent<Button>().Select();
            PauseMenu.FindChild("Resume").GetComponent<Button>().OnSelect(null);
            PauseMenu.gameObject.SetActive(true);//make canvas viseble
            Time.timeScale = 0;//stop time
        }
        else
        {
            PauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;//turn on time
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
}
