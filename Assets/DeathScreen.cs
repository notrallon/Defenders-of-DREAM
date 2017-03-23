using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DeathScreen : MonoBehaviour {

    [SerializeField]
    private Button restartButton, quitButton;


	// Use this for initialization
	void Start () {

        Button restart = restartButton.GetComponent<Button>();
        restart.onClick.AddListener(OnClickRestart);

        Button quit = quitButton.GetComponent<Button>();
        quit.onClick.AddListener(OnClickQuit);

	    restart.Select();
	    restart.OnSelect(null);
	}
	
	void OnClickRestart()
    {
        GameController.Instance.ResetDefault();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnClickQuit()
    {
        GameController.Instance.ResetDefault();
        SceneManager.LoadScene(0); 
    }
}
