using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int baseHealth, baseAttack, baseFortune;
    public float healthUpgrade, attackUpgrade, fortuneUpgrade;
    public float healthCost, attackCost, fortuneCost;

    public int coins, crystals;

    public TMP_Text healthText, attackText, fortuneText;
    public TMP_Text coinsText, crystalsText;

    private void Awake()
    {
        healthText.text = baseHealth.ToString();
        attackText.text = baseAttack.ToString();
        fortuneText.text = baseFortune.ToString();

        coinsText.text = coins.ToString();
        crystalsText.text = crystals.ToString();
    }

    public void UpgradeHealth()
    {
        baseHealth += Mathf.FloorToInt(healthUpgrade);
        healthText.text = baseHealth.ToString();

        coins -= Mathf.FloorToInt(healthCost);
        coinsText.text = coins.ToString();
    }

    public void UpgradeAttack()
    {
        baseAttack += Mathf.FloorToInt(attackUpgrade);
        attackText.text = baseAttack.ToString();

        coins -= Mathf.FloorToInt(attackCost);
        coinsText.text = coins.ToString();
    }

    public void UpgradeFortune()
    {
        baseFortune += Mathf.FloorToInt(fortuneUpgrade);
        fortuneText.text = baseFortune.ToString();

        crystals -= Mathf.FloorToInt(fortuneCost);
        crystalsText.text = crystals.ToString();
    }
}
