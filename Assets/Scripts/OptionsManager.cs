using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public AudioMixer master;
    public TMP_Text masterText, musicText, sfxText;
    public Slider masterSlider, musicSlider, sfxSlider;
    public TMP_Dropdown theme;

    public Toggle fullscreen;

    public AudioManager audioManager;

    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 0);
        masterText.text = $"{PlayerPrefs.GetFloat("SfxVolume") + 80}";
        musicText.text = $"{PlayerPrefs.GetFloat("MusicVolume") + 80}";
        sfxText.text = $"{PlayerPrefs.GetFloat("MasterVolume") + 80}";

        fullscreen.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;

        theme.value = PlayerPrefs.GetInt("Theme", 0);
        StartCoroutine(audioManager.FadeSound(audioManager.themes[PlayerPrefs.GetInt("Theme", 0)], "in", 0.8f, true));
    }

    public void UpdateMasterVolume(float volume)
    {
        master.SetFloat("masterVolume", volume);
        masterText.text = $"{volume + 80}";
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void UpdateMusicVolume(float volume)
    {
        master.SetFloat("musicVolume", volume);
        musicText.text = $"{volume + 80}";
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void UpdateSfxVolume(float volume)
    {
        master.SetFloat("sfxVolume", volume);
        sfxText.text = $"{volume + 80}";
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    public void SetFullscreen(bool isOn)
    {
        Screen.fullScreen = isOn;
        if (isOn) PlayerPrefs.SetInt("Fullscreen", 1);
        else PlayerPrefs.SetInt("Fullscreen", 0);
    }

    public void SetTheme(int option)
    {
        StartCoroutine(audioManager.FadeSound(audioManager.themes[PlayerPrefs.GetInt("Theme", 0)], "out", 0.8f, end: true));
        StartCoroutine(audioManager.FadeSound(audioManager.themes[option], "in", 0.2f, true));

        PlayerPrefs.SetInt("Theme", option);
    }
}
