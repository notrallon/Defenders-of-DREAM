using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour {

    [SerializeField]
    private Button m_MainMenu;

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
        SceneManager.LoadScene(0);
    }

}
