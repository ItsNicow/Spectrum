using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField]
    AudioSource click, theme;

    [SerializeField]
    GameObject optionsMenu, creditsMenu;

    private void Awake()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Click()
    {
        click.Play();
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);

        if (creditsMenu.activeSelf) creditsMenu.SetActive(false);
    }

    public void CreditsMenu()
    {
        creditsMenu.SetActive(!creditsMenu.activeSelf);

        if (optionsMenu.activeSelf) optionsMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
