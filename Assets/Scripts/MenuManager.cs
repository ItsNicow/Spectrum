using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject shopMenu, optionsMenu;

    private void Awake()
    {
        shopMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void ShopMenu()
    {
        shopMenu.SetActive(!shopMenu.activeSelf);
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
