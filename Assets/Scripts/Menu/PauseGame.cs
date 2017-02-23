using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    public Transform canvas;


    // Update is called once per frame
    void Start()
    {
        canvas.gameObject.SetActive(false);
    }
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
       
	}
    public void pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void LoadScene(string name)
    {
        Application.LoadLevel(name);
    }
}
