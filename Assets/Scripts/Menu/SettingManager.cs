using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingManager : MonoBehaviour
{

    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown vSyncDropdown;
    public Slider musicVolumeSlider;
    public Button applyButton;
   

    public AudioSource musicSource;

    public Resolution[] resolutions;
    public GameSettings gameSettings;


    void Start()
    {
        
        LoadSettings();
    }

    void OnEnable()
    {

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreeenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextireQualityChange(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        applyButton.onClick.AddListener(delegate { onApplyButtonClick();  });

        resolutions = Screen.resolutions;
        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
    }

    public void OnFullscreeenToggle()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void OnTextireQualityChange()
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.value;

    }

    public void OnAntialiasingChange()
    {
        QualitySettings.antiAliasing = gameSettings.antialiasing = (int)Mathf.Pow(antialiasingDropdown.value,2);
    }

    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdown.value;
    }

    public void OnMusicVolumeChange()
    {
        AudioListener.volume = gameSettings.musicVolume = musicVolumeSlider.value;
        
    }
    public void onApplyButtonClick()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
        LoadSettings();
    }
    public void LoadSettings()
    {
        if (!File.Exists(Application.persistentDataPath + "/gamesettings.json"))
        {
            gameSettings.fullscreen = Screen.fullScreen;
            gameSettings.textureQuality = QualitySettings.masterTextureLimit;
            gameSettings.antialiasing = QualitySettings.antiAliasing;
            gameSettings.vSync = QualitySettings.vSyncCount;
            gameSettings.musicVolume = AudioListener.volume;
            SaveSettings();
        }
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

        musicVolumeSlider.value = AudioListener.volume = gameSettings.musicVolume;
        antialiasingDropdown.value = gameSettings.antialiasing;
        vSyncDropdown.value = gameSettings.vSync;
        textureQualityDropdown.value = gameSettings.textureQuality;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        fullscreenToggle.isOn = gameSettings.fullscreen;

        resolutionDropdown.RefreshShownValue();
        
    }
}
