using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject shopMenu, optionsMenu, creditsMenu, endGameMenu;

    public GameObject waterMenu, fireMenu, airMenu, earthMenu, background;

    [SerializeField]
    PlayerManager playerManager;
    [SerializeField]
    GameManager gameManager;

    private void Awake()
    {
        shopMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        endGameMenu.SetActive(false);
        background.SetActive(false);

        waterMenu.SetActive(false);
        fireMenu.SetActive(false);
        airMenu.SetActive(false);
        earthMenu.SetActive(false);
    }

    public void ShopMenu()
    {
        shopMenu.SetActive(!shopMenu.activeSelf);

        if (optionsMenu.activeSelf) optionsMenu.SetActive(false);
        if (creditsMenu.activeSelf) creditsMenu.SetActive(false);
    }

    public void OptionsMenu()
    {
        if (gameManager.status == GameStatus.Ended)
        {
            EndGameMenu();
        }
        else
        {
            optionsMenu.SetActive(!optionsMenu.activeSelf);

            if (shopMenu.activeSelf) shopMenu.SetActive(false);
            if (creditsMenu.activeSelf) creditsMenu.SetActive(false);
        }
    }

    public void CreditsMenu()
    {
        creditsMenu.SetActive(!creditsMenu.activeSelf);

        if (optionsMenu.activeSelf) optionsMenu.SetActive(false);
        if (endGameMenu.activeSelf) endGameMenu.SetActive(false);
    }

    public void EndGameMenu()
    {
        endGameMenu.SetActive(true);

        if (shopMenu.activeSelf) shopMenu.SetActive(false);
        if (optionsMenu.activeSelf) optionsMenu.SetActive(false);
        if (creditsMenu.activeSelf) creditsMenu.SetActive(false);
        if (waterMenu.activeSelf) waterMenu.SetActive(false);
        if (fireMenu.activeSelf) fireMenu.SetActive(false);
        if (airMenu.activeSelf) airMenu.SetActive(false);
        if (earthMenu.activeSelf) earthMenu.SetActive(false);
    }

    public void WaterMenu()
    {
        playerManager.waterSelected = !waterMenu.activeSelf;
        waterMenu.SetActive(!waterMenu.activeSelf);

        playerManager.fireSelected = false;
        playerManager.airSelected = false;
        playerManager.earthSelected = false;
        if (fireMenu.activeSelf) fireMenu.SetActive(false);
        if (airMenu.activeSelf) airMenu.SetActive(false);
        if (earthMenu.activeSelf) earthMenu.SetActive(false);
    }

    public void FireMenu()
    {
        playerManager.fireSelected = !fireMenu.activeSelf;
        fireMenu.SetActive(!fireMenu.activeSelf);

        playerManager.waterSelected = false;
        playerManager.airSelected = false;
        playerManager.earthSelected = false;
        if (waterMenu.activeSelf) waterMenu.SetActive(false);
        if (airMenu.activeSelf) airMenu.SetActive(false);
        if (earthMenu.activeSelf) earthMenu.SetActive(false);
    }

    public void AirMenu()
    {
        playerManager.airSelected = !airMenu.activeSelf;
        airMenu.SetActive(!airMenu.activeSelf);

        playerManager.waterSelected = false;
        playerManager.fireSelected = false;
        playerManager.earthSelected = false;
        if (waterMenu.activeSelf) waterMenu.SetActive(false);
        if (fireMenu.activeSelf) fireMenu.SetActive(false);
        if (earthMenu.activeSelf) earthMenu.SetActive(false);
    }

    public void EarthMenu()
    {
        playerManager.earthSelected = !earthMenu.activeSelf;
        earthMenu.SetActive(!earthMenu.activeSelf);

        playerManager.waterSelected = false;
        playerManager.fireSelected = false;
        playerManager.airSelected = false;
        if (waterMenu.activeSelf) waterMenu.SetActive(false);
        if (fireMenu.activeSelf) fireMenu.SetActive(false);
        if (airMenu.activeSelf) airMenu.SetActive(false);
    }
}
