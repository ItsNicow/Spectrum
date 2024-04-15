using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int baseHealth, baseAttack, baseFortune;
    public int coins, crystals;
    public TMP_Text coinsText, crystalsText;

    public int healthUpgrade, attackUpgrade, fortuneUpgrade;
    int healthCost, attackCost, fortuneCost;
    int healthPurchases, attackPurchases, fortunePurchases, artifactPurchases;
    public TMP_Text healthText, attackText, fortuneText;
    public TMP_Text healthUpgradeText, attackUpgradeText, fortuneUpgradeText;
    public TMP_Text healthCostText, attackCostText, fortuneCostText;
    public Button healthButton, attackButton, fortuneButton;

    public float tearBonus, bladeBonus, ringBonus, prismBonus;

    [SerializeField]
    ArtifactManager artifactManager;

    private void Awake()
    {
        healthText.text = baseHealth.ToString();
        attackText.text = baseAttack.ToString();
        fortuneText.text = baseFortune.ToString();

        coinsText.text = coins.ToString();
        crystalsText.text = crystals.ToString();

        healthCost = healthUpgrade * 10;
        attackCost = attackUpgrade * 10;
        fortuneCost = fortuneUpgrade * 10;
    }

    private void Update()
    {
        healthButton.interactable = coins >= healthCost;
        attackButton.interactable = coins >= attackCost;
        fortuneButton.interactable = coins >= fortuneCost;

        for (int i = 0; i < artifactManager.artifacts.Count; i++)
        {
            artifactManager.artifactButtons[i].interactable = (coins >= artifactManager.artifacts[i].price && artifactManager.ownedArtifacts.Count < 16);
        }

        for (int i = 0; i < artifactManager.ownedArtifacts.Count; i++)
        {
            artifactManager.ownedArtifactButtons[i].interactable = artifactManager.ownedArtifacts[i].cd <= 0;
            artifactManager.ownedArtifactButtons[i].gameObject.GetComponentInChildren<TMP_Text>().enabled = artifactManager.ownedArtifacts[i].cd > 0;
        }

        tearBonus = 1 + (artifactManager.ownedArtifacts.Where(a => a.name == artifactManager.artifacts[0].name).ToList().Count / 10f);
        bladeBonus = 1 + (artifactManager.ownedArtifacts.Where(a => a.name == artifactManager.artifacts[1].name).ToList().Count / 10f);
        ringBonus = 1 + (artifactManager.ownedArtifacts.Where(a => a.name == artifactManager.artifacts[2].name).ToList().Count / 10f);
        prismBonus = 1 + (artifactManager.ownedArtifacts.Where(a => a.name == artifactManager.artifacts[3].name).ToList().Count / 10f);
    }

    public void UpgradeHealth()
    {
        if (coins >= healthCost)
        {
            healthPurchases++;
            baseHealth += healthUpgrade;
            healthText.text = baseHealth.ToString();

            coins -= healthCost;
            coinsText.text = coins.ToString();

            healthUpgrade = healthPurchases / 10 + 1;
            healthUpgradeText.text = "+" + healthUpgrade.ToString();
            healthCost = healthUpgrade * 10;
            healthCostText.text = healthCost.ToString();
        }
    }

    public void UpgradeAttack()
    {
        if (coins >= attackCost)
        {
            attackPurchases++;
            baseAttack += attackUpgrade;
            attackText.text = baseAttack.ToString();

            coins -= attackCost;
            coinsText.text = coins.ToString();

            attackUpgrade = attackPurchases / 10 + 1;
            attackUpgradeText.text = "+" + attackUpgrade.ToString();
            attackCost = attackUpgrade * 10;
            attackCostText.text = attackCost.ToString();
        }
    }

    public void UpgradeFortune()
    {
        if (coins >= fortuneCost)
        {
            fortunePurchases++;
            baseFortune += fortuneUpgrade;
            fortuneText.text = baseFortune.ToString();

            coins -= fortuneCost;
            coinsText.text = coins.ToString();

            fortuneUpgrade = fortunePurchases / 10 + 1;
            fortuneUpgradeText.text = "+" + fortuneUpgrade.ToString();
            fortuneCost = fortuneUpgrade * 10;
            fortuneCostText.text = fortuneCost.ToString();
        }
    }

    public void PurchaseArtifact(Artifact artifact)
    {
        if (coins >= artifact.price && artifactManager.ownedArtifacts.Count < 16)
        {
            artifactPurchases++;
            artifactManager.ownedArtifacts.Add(artifact);

            coins -= artifact.price;
            coinsText.text = coins.ToString();

            artifactManager.PurchaseArtifact(artifact);
        }
    }
}
