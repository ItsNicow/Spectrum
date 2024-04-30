using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;

    InputAction useSkillAction;
    InputAction upgradeSkillAction;

    InputAction selectWaterAction;
    InputAction selectFireAction;
    InputAction selectAirAction;
    InputAction selectEarthAction;

    InputAction upgradeHealthAction;
    InputAction upgradeAttackAction;
    InputAction upgradeFortuneAction;

    InputAction useTearAction;
    InputAction useBladeAction;
    InputAction useRingAction;
    InputAction usePrismAction;

    InputAction shopAction;
    InputAction optionsAction;

    [SerializeField]
    TilesGenerator tilesGenerator;
    [SerializeField]
    PlayerManager playerManager;
    [SerializeField]
    MenuManager menuManager;
    [SerializeField]
    AudioManager audioManager;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        useSkillAction = playerInput.actions.FindAction("useSkill");
        upgradeSkillAction = playerInput.actions.FindAction("upgradeSkill");

        selectWaterAction = playerInput.actions.FindAction("selectWater");
        selectFireAction = playerInput.actions.FindAction("selectFire");
        selectAirAction = playerInput.actions.FindAction("selectAir");
        selectEarthAction = playerInput.actions.FindAction("selectEarth");

        upgradeHealthAction = playerInput.actions.FindAction("upgradeHealth");
        upgradeAttackAction = playerInput.actions.FindAction("upgradeAttack");
        upgradeFortuneAction = playerInput.actions.FindAction("upgradeFortune");

        useTearAction = playerInput.actions.FindAction("useTear");
        useBladeAction = playerInput.actions.FindAction("useBlade");
        useRingAction = playerInput.actions.FindAction("useRing");
        usePrismAction = playerInput.actions.FindAction("usePrism");

        shopAction = playerInput.actions.FindAction("shop");
        optionsAction = playerInput.actions.FindAction("options");

        SubscribeActions();
    }

    void SubscribeActions()
    {
        useSkillAction.performed += UseSkillAction;
        upgradeSkillAction.performed += UpgradeSkillAction;

        selectWaterAction.performed += SelectWaterAction;
        selectFireAction.performed += SelectFireAction;
        selectAirAction.performed += SelectAirAction;
        selectEarthAction.performed += SelectEarthAction;

        upgradeHealthAction.performed += UpgradeHealthAction;
        upgradeAttackAction.performed += UpgradeAttackAction;
        upgradeFortuneAction.performed += UpgradeFortuneAction;

        shopAction.performed += ShopAction;
        optionsAction.performed += OptionsAction;
    }

    public void UnsubscribeActions()
    {
        useSkillAction.performed -= UseSkillAction;
        upgradeSkillAction.performed -= UpgradeSkillAction;

        selectWaterAction.performed -= SelectWaterAction;
        selectFireAction.performed -= SelectFireAction;
        selectAirAction.performed -= SelectAirAction;
        selectEarthAction.performed -= SelectEarthAction;

        upgradeHealthAction.performed -= UpgradeHealthAction;
        upgradeAttackAction.performed -= UpgradeAttackAction;
        upgradeFortuneAction.performed -= UpgradeFortuneAction;

        shopAction.performed -= ShopAction;
        optionsAction.performed -= OptionsAction;
    }

    void UseSkillAction(InputAction.CallbackContext context)
    {
        if (tilesGenerator.currentTile != null) tilesGenerator.currentTile.ClickTile();
    }

    void UpgradeSkillAction(InputAction.CallbackContext context)
    {
        if (playerManager.waterSelected && playerManager.crystals >= playerManager.waterCost)
        {
            audioManager.PurchaseClick();
            playerManager.UpgradeWater();
        }
        if (playerManager.fireSelected && playerManager.crystals >= playerManager.fireCost)
        {
            audioManager.PurchaseClick();
            playerManager.UpgradeFire();
        }
        if (playerManager.airSelected && playerManager.crystals >= playerManager.airCost)
        {
            audioManager.PurchaseClick();
            playerManager.UpgradeAir();
        }
        if (playerManager.earthSelected && playerManager.crystals >= playerManager.earthCost)
        {
            audioManager.PurchaseClick();
            playerManager.UpgradeEarth();
        }
    }

    void SelectWaterAction(InputAction.CallbackContext context)
    {
        audioManager.Click();
        playerManager.SelectWater();
        menuManager.WaterMenu();
    }

    void SelectFireAction(InputAction.CallbackContext context)
    {
        audioManager.Click();
        playerManager.SelectFire();
        menuManager.FireMenu();
    }

    void SelectAirAction(InputAction.CallbackContext context)
    {
        audioManager.Click();
        playerManager.SelectAir();
        menuManager.AirMenu();
    }

    void SelectEarthAction(InputAction.CallbackContext context)
    {
        audioManager.Click();
        playerManager.SelectEarth();
        menuManager.EarthMenu();
    }

    void UpgradeHealthAction(InputAction.CallbackContext context)
    {
        if (playerManager.coins >= playerManager.healthCost)
        {
            audioManager.PurchaseClick();
            playerManager.UpgradeHealth();
        }
    }

    void UpgradeAttackAction(InputAction.CallbackContext context)
    {
        if (playerManager.coins >= playerManager.attackCost)
        {
            audioManager.PurchaseClick();
            playerManager.UpgradeAttack();
        }
    }

    void UpgradeFortuneAction(InputAction.CallbackContext context)
    {
        if (playerManager.coins >= playerManager.fortuneCost)
        {
            audioManager.PurchaseClick();
            playerManager.UpgradeFortune();
        }
    }

    void ShopAction(InputAction.CallbackContext context)
    {
        audioManager.Click();
        menuManager.ShopMenu();
    }

    void OptionsAction(InputAction.CallbackContext context)
    {
        audioManager.Click();
        menuManager.OptionsMenu();
    }

    public void DisableInputs()
    {
        playerInput.DeactivateInput();
    }
}
