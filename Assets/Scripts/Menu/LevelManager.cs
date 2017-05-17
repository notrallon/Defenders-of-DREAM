using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private Canvas m_Canvas;

    [SerializeField] private AudioClip m_MainSong;

    private void Start() {
        Cursor.visible = false;
        m_Canvas.enabled = false;
    }

    public void LoadScene(string name)
    {
        if (name == "MainLevel") {
            var music = GameObject.FindGameObjectWithTag("Music").GetComponent<bg_music>();
            music.SwapMusic(m_MainSong, 3.0f);
            m_Canvas.enabled = true;
        }

        SceneManager.LoadScene(name);
        //Application.LoadLevel(name);//load level were name = the name of the level
    }
    public void QuitGame()
    {
        Application.Quit();//endprogram
    }
}
