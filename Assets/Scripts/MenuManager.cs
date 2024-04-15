using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject shopMenu, optionsMenu, creditsMenu;

    private void Awake()
    {
        shopMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void ShopMenu()
    {
        shopMenu.SetActive(!shopMenu.activeSelf);

        if (optionsMenu.activeSelf) optionsMenu.SetActive(false);
        if (creditsMenu.activeSelf) creditsMenu.SetActive(false);
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);

        if (shopMenu.activeSelf) shopMenu.SetActive(false);
        if (creditsMenu.activeSelf) creditsMenu.SetActive(false);
    }

    public void CreditsMenu()
    {
        creditsMenu.SetActive(!creditsMenu.activeSelf);

        if (optionsMenu.activeSelf) optionsMenu.SetActive(false);
    }
}
