using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DeathScreen : MonoBehaviour {
    [SerializeField]
    private Button m_RestartButton, m_QuitButton;

    [SerializeField]
    private AudioClip m_MenuClip;

    private void Awake() {
        transform.FindChild("YouDied").gameObject.SetActive(false);
        Invoke("ShowScreen", 0.5f);
    }

    // Use this for initialization
	void Start () {


	}

    void ShowScreen() {
        transform.FindChild("YouDied").gameObject.SetActive(true);
        Button restart = m_RestartButton.GetComponent<Button>();
        restart.onClick.AddListener(OnClickRestart);

        Button quit = m_QuitButton.GetComponent<Button>();
        quit.onClick.AddListener(OnClickQuit);

        restart.Select();
        restart.OnSelect(null);
    }

	void OnClickRestart()
    {
        GameController.Instance.ResetDefault();
        var music = GameObject.FindGameObjectWithTag("Music");
        music.GetComponent<AudioSource>().volume = music.GetComponent<bg_music>().MaxMusicVolume;
        music.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnClickQuit()
    {
        GameController.Instance.ResetDefault();
        var music = GameObject.FindGameObjectWithTag("Music");
        var musicSource = music.GetComponent<AudioSource>();
        musicSource.Stop();
        musicSource.volume = music.GetComponent<bg_music>().MaxMusicVolume;
        musicSource.clip = m_MenuClip;
        musicSource.Play();
        musicSource.loop = true;
        SceneManager.LoadScene(0);
    }
}
