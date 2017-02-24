using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    public Transform canvas;

    
    void Start()
    {
        canvas.gameObject.SetActive(false);//turen off canvas at start of level
    }
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))//if you press escape
        {
            pause();//run pause
        }
       
	}
    public void pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);//make canvas viseble
            Time.timeScale = 0;//stop time
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;//turn on time
        }
    }
    public void LoadScene(string name)
    {
        //load level were name = the name of the level
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        Application.LoadLevel(name);
    }
}
