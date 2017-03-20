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



	}
	
	void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnClickQuit()
    {
        SceneManager.LoadScene(0);
    }
}
