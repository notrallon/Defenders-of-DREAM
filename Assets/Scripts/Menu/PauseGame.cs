using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

    public Transform PauseMenu;

    
    void Start()
    {
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
        if (PauseMenu.gameObject.activeInHierarchy == false)
        {
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
        Destroy(GameController.Instance);
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        Application.LoadLevel(name);
    }

    public void RestartScene() {
        GameController.Instance.ResetDefault();

        // Load current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
