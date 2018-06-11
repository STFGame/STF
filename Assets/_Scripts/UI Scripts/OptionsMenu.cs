using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer m_audioMixer = null;

    [SerializeField] private Slider m_masterVolumeSlider = null;

    [SerializeField] private Text m_musicVolumeText = null;
    [SerializeField] private Slider m_musicVolumeSlider = null;

    [SerializeField] private Dropdown m_resolutionDropdown = null;
    [SerializeField] private Dropdown m_qualityDropdown = null;
    [SerializeField] private Dropdown m_vSyncDropdown = null;
    [SerializeField] private Dropdown m_antialiasingDropdown = null;

    [SerializeField] private Button m_applyButton = null;
    [SerializeField] private Toggle m_fullscreenToggle = null;

    private Resolution[] m_resolutions;

    private GameSettings m_gameSettings;

    private void Awake()
    {
        m_gameSettings = new GameSettings();

        InitialiseResolutions();

        InitialiseListeners();

        Initialise();
    }

    private void Initialise()
    {
        LoadSettings();

        OnFullScreenToggle();
        OnResolutionChange();
        OnTextureQualityChange();
        OnVSyncChange();
        OnAntialiasingChange();
        OnMusicSliderChange();
        SaveSettings();
    }

    private void InitialiseListeners()
    {
        m_fullscreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        m_resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        m_qualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        m_vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        m_antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        m_musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicSliderChange(); });
        m_applyButton.onClick.AddListener(delegate { SaveSettings(); });
    }

    private void InitialiseResolutions()
    {
        m_resolutions = Screen.resolutions;

        m_resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < m_resolutions.Length; i++)
        {
            string option = m_resolutions[i].width + " x " + m_resolutions[i].height;
            options.Add(option);
        }

        m_resolutionDropdown.AddOptions(options);
        m_resolutionDropdown.RefreshShownValue();
    }

    public void OnResolutionChange()
    {
        int resolutionIndex = 0;

        m_gameSettings.Resolution = resolutionIndex = m_resolutionDropdown.value;

        Resolution resolution = m_resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void OnTextureQualityChange()
    {
        int textureQuality = 0;
        m_gameSettings.TextureQuality = textureQuality = m_qualityDropdown.value;

        QualitySettings.SetQualityLevel(textureQuality);
    }

    public void OnAntialiasingChange()
    {
        int antialiasing = 0;
        m_gameSettings.Antialiasing = antialiasing = m_antialiasingDropdown.value;

        QualitySettings.antiAliasing = (int)Mathf.Pow(2f, antialiasing);
    }

    public void OnVSyncChange()
    {
        int vSync = 0;
        m_gameSettings.vSync = vSync = m_vSyncDropdown.value;

        QualitySettings.vSyncCount = vSync;
    }

    public void OnMusicSliderChange()
    {
        float musicVolume = 0f;
        m_gameSettings.MusicVolume = musicVolume = m_musicVolumeSlider.value;

        m_musicVolumeText.text = string.Format("{0:0}", musicVolume);
    }

    public void OnFullScreenToggle()
    {
        bool fullScreen = false;
        m_gameSettings.FullScreen = fullScreen = m_fullscreenToggle.isOn;

        Screen.SetResolution(Screen.width, Screen.height, fullScreen);
    }

    //public void SaveSettings()
    //{
    //    string xmlPath = System.IO.Path.Combine(Application.streamingAssetsPath, "MenuSettings.xml");

    //    XmlDocument xmlDoc = new XmlDocument();

    //    xmlDoc.Load(xmlPath);

    //    XmlElement resolutionNode = xmlDoc.SelectSingleNode("MenuSettings/Resolution") as XmlElement;
    //    XmlElement textureQualityNode = xmlDoc.SelectSingleNode("MenuSettings/TextureQuality") as XmlElement;
    //    XmlElement antialiasingNode = xmlDoc.SelectSingleNode("MenuSettings/Antialiasing") as XmlElement;
    //    XmlElement vSyncNode = xmlDoc.SelectSingleNode("MenuSettings/vSync") as XmlElement;
    //    XmlElement musicVolumeNode = xmlDoc.SelectSingleNode("MenuSettings/MusicVolume") as XmlElement;
    //    XmlElement fullScreenNode = xmlDoc.SelectSingleNode("MenuSettings/FullScreen") as XmlElement;

    //    resolutionNode.InnerText = GameSettings.Resolution.ToString();
    //    textureQualityNode.InnerText = GameSettings.TextureQuality.ToString();
    //    antialiasingNode.InnerText = GameSettings.Antialiasing.ToString();
    //    vSyncNode.InnerText = GameSettings.vSync.ToString();
    //    musicVolumeNode.InnerText = GameSettings.MusicVolume.ToString();
    //    fullScreenNode.InnerText = GameSettings.FullScreen.ToString();

    //    xmlDoc.Save(xmlPath);
    //}


    public void SaveSettings()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameSettings));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/OptionsSettings.xml", FileMode.Create);
        xmlSerializer.Serialize(stream, m_gameSettings);
        stream.Close();
    }

    private void LoadSettings()
    {
        try
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameSettings));
            FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/OptionsSettings.xml", FileMode.Open);

            m_gameSettings = xmlSerializer.Deserialize(stream) as GameSettings;
            stream.Close();

            m_resolutionDropdown.value = m_gameSettings.Resolution;
            m_qualityDropdown.value = m_gameSettings.TextureQuality;
            m_vSyncDropdown.value = m_gameSettings.vSync;
            m_musicVolumeSlider.value = m_gameSettings.MusicVolume;
            m_fullscreenToggle.isOn = m_gameSettings.FullScreen;
            m_antialiasingDropdown.value = m_gameSettings.Antialiasing;
        }
        catch
        {
            return;
        }
    }
}
