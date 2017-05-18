using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour {

    [SerializeField]
    private Button m_MainMenu;

    [SerializeField]
    private AudioClip m_MenuClip;

    private void Awake()
    {
        transform.FindChild("YouWon").gameObject.SetActive(false);
        Invoke("ShowScreen", 0.5f);
    }

    // Use this for initialization
    void Start()
    {


    }

    void ShowScreen()
    {
        transform.FindChild("YouWon").gameObject.SetActive(true);
        
        Button quit = m_MainMenu.GetComponent<Button>();
        quit.onClick.AddListener(OnClickRestart);

        quit.Select();
        quit.OnSelect(null);
    }

    void OnClickRestart()
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
