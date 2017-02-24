using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public void LoadScene(string name)
    {
        Application.LoadLevel(name);//load level were name = the name of the level
    }
    public void QuitGame()
    {
        Application.Quit();//endprogram
    }
}
