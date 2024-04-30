using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{
    public int baseHealth, baseAttack, baseFortune;
    public float currentHealth;
    public int coins, crystals;
    public TMP_Text coinsText, crystalsText;

    public int healthUpgrade, attackUpgrade, fortuneUpgrade;
    public int healthCost, attackCost, fortuneCost;
    int healthPurchases, attackPurchases, fortunePurchases;
    public TMP_Text healthText, attackText, fortuneText;
    public TMP_Text healthUpgradeText, attackUpgradeText, fortuneUpgradeText;
    public TMP_Text healthCostText, attackCostText, fortuneCostText;
    public Button healthButton, attackButton, fortuneButton;

    public float waterBonus, fireBonus, airBonus, earthBonus;

    public bool waterSelected, fireSelected, airSelected, earthSelected;
    public TMP_Text waterCooldownText, fireCooldownText, airCooldownText, earthCooldownText;
    public Button waterButton, fireButton, airButton, earthButton;
    public int waterCooldown, fireCooldown, airCooldown, earthCooldown;
    [HideInInspector]
    public float waterCd, fireCd, airCd, earthCd;

    public int waterLevel, fireLevel, airLevel, earthLevel;
    public int waterCost, fireCost, airCost, earthCost;
    int waterPurchases, firePurchases, airPurchases, earthPurchases;
    public TMP_Text waterLevelText, fireLevelText, airLevelText, earthLevelText;
    public TMP_Text waterDescriptionText, fireDescriptionText, airDescriptionText, earthDescriptionText;
    public TMP_Text waterCostText, fireCostText, airCostText, earthCostText;
    public Button waterUpgradeButton, fireUpgradeButton, airUpgradeButton, earthUpgradeButton;

    public float waterAtkScaling, fireAtkScaling, airAtkScaling, earthAtkScaling;

    public GameObject ablazeFireballPrefab, ablazeExplosionPrefab, cycloneParticlesPrefab, vanguardWallPrefab;

    [SerializeField]
    ArtifactManager artifactManager;
    [SerializeField]
    AudioManager audioManager;
    [SerializeField]
    TilesGenerator tilesGenerator;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    MonsterManager monsterManager;
    public MenuManager menuManager;

    private void Awake()
    {
        currentHealth = baseHealth;

        healthText.text = baseHealth.ToString();
        attackText.text = baseAttack.ToString();
        fortuneText.text = baseFortune.ToString();

        coinsText.text = coins.ToString();
        crystalsText.text = crystals.ToString();

        healthCost = healthUpgrade * 10;
        attackCost = attackUpgrade * 10;
        fortuneCost = fortuneUpgrade * 10;

        waterCost = (waterLevel + 1) * 100;
        fireCost = (fireLevel + 1) * 100;
        airCost = (airLevel + 1) * 100;
        earthCost = (earthLevel + 1) * 100;

        waterPurchases = waterLevel;
        firePurchases = fireLevel;
        airPurchases = airLevel;
        earthPurchases = earthLevel;
    }

    private void Update()
    {
        UpdateCooldowns(Time.deltaTime);
        UpdateDisplay();

        healthButton.interactable = coins >= healthCost;
        attackButton.interactable = coins >= attackCost;
        fortuneButton.interactable = coins >= fortuneCost;

        waterUpgradeButton.interactable = crystals >= waterCost;
        fireUpgradeButton.interactable = crystals >= fireCost;
        airUpgradeButton.interactable = crystals >= airCost;
        earthUpgradeButton.interactable = crystals >= earthCost;

        for (int i = 0; i < artifactManager.artifacts.Count; i++)
        {
            artifactManager.artifactButtons[i].interactable = (coins >= artifactManager.artifacts[i].price && artifactManager.ownedArtifacts.Count < 16);
        }

        waterBonus = 1 + (artifactManager.ownedArtifacts.Where(a => a.artifactName == artifactManager.artifacts[0].name).ToList().Count / 10f);
        fireBonus = 1 + (artifactManager.ownedArtifacts.Where(a => a.artifactName == artifactManager.artifacts[1].name).ToList().Count / 10f);
        airBonus = 1 + (artifactManager.ownedArtifacts.Where(a => a.artifactName == artifactManager.artifacts[2].name).ToList().Count / 10f);
        earthBonus = 1 + (artifactManager.ownedArtifacts.Where(a => a.artifactName == artifactManager.artifacts[3].name).ToList().Count / 10f);

        waterButton.interactable = waterCd <= 0;
        fireButton.interactable = fireCd <= 0;
        airButton.interactable = airCd <= 0;
        earthButton.interactable = earthCd <= 0;
    }

    private void UpdateCooldowns(float dT)
    {
        waterCd -= dT;
        fireCd -= dT;
        airCd -= dT;
        earthCd -= dT;
    }

    private void UpdateDisplay()
    {
        waterCooldownText.text = Mathf.RoundToInt(Mathf.Ceil(waterCd)).ToString();
        fireCooldownText.text = Mathf.RoundToInt(Mathf.Ceil(fireCd)).ToString();
        airCooldownText.text = Mathf.RoundToInt(Mathf.Ceil(airCd)).ToString();
        earthCooldownText.text = Mathf.RoundToInt(Mathf.Ceil(earthCd)).ToString();

        waterCooldownText.enabled = waterCd > 0;
        fireCooldownText.enabled = fireCd > 0;
        airCooldownText.enabled = airCd > 0;
        earthCooldownText.enabled = earthCd > 0;
    }

    public void UpgradeHealth()
    {
        if (coins >= healthCost)
        {
            healthPurchases++;
            baseHealth += healthUpgrade;
            currentHealth += healthUpgrade;
            healthText.text = baseHealth.ToString();

            coins -= healthCost;
            coinsText.text = coins.ToString();
            gameManager.coinsSpent += healthCost;

            if (gameManager.difficulty == GameDifficulty.Normal)
            {
                healthUpgrade = healthPurchases / 10 + 1;
                healthCost = healthUpgrade * 10;
            }
            else
            {
                healthCost = (healthPurchases / 10 + 1) * 10;
            }
            healthUpgradeText.text = "+" + healthUpgrade.ToString();
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
            gameManager.coinsSpent += attackCost;

            if (gameManager.difficulty == GameDifficulty.Normal)
            {
                attackUpgrade = attackPurchases / 10 + 1;
                attackCost = attackUpgrade * 10;
            }
            else
            {
                attackCost = (attackPurchases / 10 + 1) * 10;
            }
            attackUpgradeText.text = "+" + attackUpgrade.ToString();
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
            gameManager.coinsSpent += fortuneCost;

            if (gameManager.difficulty == GameDifficulty.Normal)
            {
                fortuneUpgrade = fortunePurchases / 10 + 1;
                fortuneCost = fortuneUpgrade * 10;
            }
            else
            {
                fortuneCost = (fortunePurchases / 10 + 1) * 10;
            }
            fortuneUpgradeText.text = "+" + fortuneUpgrade.ToString();
            fortuneCostText.text = fortuneCost.ToString();
        }
    }

    public void PurchaseArtifact(ArtifactData artifact)
    {
        if (coins >= artifact.price && artifactManager.ownedArtifacts.Count < 16)
        {
            coins -= artifact.price;
            coinsText.text = coins.ToString();
            gameManager.coinsSpent += artifact.price;

            artifactManager.PurchaseArtifact(artifact);
        }
    }

    public void SelectWater()
    {
        if (!menuManager.waterMenu.activeSelf)
        {
            tilesGenerator.ClearTiles();
            tilesGenerator.size = 50;
            tilesGenerator.type = TilesGenerator.Type.Vertical;
            tilesGenerator.selectHover = true;
            if (tilesGenerator.currentTile != null) tilesGenerator.HoverMultipleTiles(tilesGenerator.currentTile.position);
        }
        else
        {
            tilesGenerator.Default();
            tilesGenerator.ClearTiles();
            if (tilesGenerator.currentTile != null) tilesGenerator.HoverMultipleTiles(tilesGenerator.currentTile.position);
        }
    }

    public void SelectFire()
    {
        if (!menuManager.fireMenu.activeSelf)
        {
            tilesGenerator.ClearTiles();
            tilesGenerator.size = 0;
            tilesGenerator.type = TilesGenerator.Type.Radius;
            tilesGenerator.selectHover = true;
            if (tilesGenerator.currentTile != null) tilesGenerator.HoverMultipleTiles(tilesGenerator.currentTile.position);
        }
        else
        {
            tilesGenerator.Default();
            tilesGenerator.ClearTiles();
            if (tilesGenerator.currentTile != null) tilesGenerator.HoverMultipleTiles(tilesGenerator.currentTile.position);
        }
    }

    public void SelectAir()
    {
        if (!menuManager.airMenu.activeSelf)
        {
            tilesGenerator.ClearTiles();
            tilesGenerator.size = 3;
            tilesGenerator.type = TilesGenerator.Type.Radius;
            tilesGenerator.selectHover = true;
            if (tilesGenerator.currentTile != null) tilesGenerator.HoverMultipleTiles(tilesGenerator.currentTile.position);
        }
        else
        {
            tilesGenerator.Default();
            tilesGenerator.ClearTiles();
            if (tilesGenerator.currentTile != null) tilesGenerator.HoverMultipleTiles(tilesGenerator.currentTile.position);
        }
    }

    public void SelectEarth()
    {
        if (!menuManager.earthMenu.activeSelf)
        {
            tilesGenerator.ClearTiles();
            tilesGenerator.size = 3;
            tilesGenerator.type = TilesGenerator.Type.Horizontal;
            tilesGenerator.selectHover = true;
            if (tilesGenerator.currentTile != null) tilesGenerator.HoverMultipleTiles(tilesGenerator.currentTile.position);
        }
        else
        {
            tilesGenerator.Default();
            tilesGenerator.ClearTiles();
            if (tilesGenerator.currentTile != null) tilesGenerator.HoverMultipleTiles(tilesGenerator.currentTile.position);
        }
    }

    public void UpgradeWater()
    {
        if (crystals >= waterCost)
        {
            waterPurchases++;
            waterLevel++;
            waterLevelText.text = "Level " + waterLevel.ToString();

            crystals -= waterCost;
            crystalsText.text = crystals.ToString();
            gameManager.crystalsSpent += waterCost;

            waterAtkScaling = waterPurchases / 10 + 1;
            waterDescriptionText.text = "Shoot a water beam in a vertical line, dealing " + waterAtkScaling * 100 + "% ATK as <color=#256CCF>Water <color=black>DMG";
            waterCost = (waterLevel + 1) * 100;
            waterCostText.text = waterCost.ToString();
        }
    }

    public void UpgradeFire()
    {
        if (crystals >= fireCost)
        {
            firePurchases++;
            fireLevel++;
            fireLevelText.text = "Level " + fireLevel.ToString();

            crystals -= fireCost;
            crystalsText.text = crystals.ToString();
            gameManager.crystalsSpent += fireCost;

            fireAtkScaling = (firePurchases / 10) * 2 + 2;
            fireDescriptionText.text = "Launch a fireball towards a targeted tile, dealing " + fireAtkScaling * 100 + "% ATK as <color=#DE2E21>Fire <color=black>DMG to the first enemy hit";
            fireCost = (fireLevel + 1) * 100;
            fireCostText.text = fireCost.ToString();
        }
    }

    public void UpgradeAir()
    {
        if (crystals >= airCost)
        {
            airPurchases++;
            airLevel++;
            airLevelText.text = "Level " + airLevel.ToString();

            crystals -= airCost;
            crystalsText.text = crystals.ToString();
            gameManager.crystalsSpent += airCost;

            airAtkScaling = (airPurchases / 10) / 5f + 0.2f;
            airDescriptionText.text = "Invoke a storm, dealing " + airAtkScaling * 100 + "% ATK as <color=#52CCA5>Air <color=black>DMG per second and slowing by 30% for 4 seconds";
            airCost = (airLevel + 1) * 100;
            airCostText.text = airCost.ToString();
        }
    }

    public void UpgradeEarth()
    {
        if (crystals >= earthCost)
        {
            earthPurchases++;
            earthLevel++;
            earthLevelText.text = "Level " + earthLevel.ToString();

            crystals -= earthCost;
            crystalsText.text = crystals.ToString();
            gameManager.crystalsSpent += earthCost;

            earthAtkScaling = (earthPurchases / 10) / 5f * 4f + 0.8f;
            earthDescriptionText.text = "Brandish a wall, blocking any entity for 8 seconds and dealing " + earthAtkScaling * 100 + "% ATK as <color=#805029>Earth <color=black>DMG";
            earthCost = (earthLevel + 1) * 100;
            earthCostText.text = earthCost.ToString();
        }
    }

    public void Skill(List<Tile> targetTiles)
    {
        if (waterSelected && waterCd <= 0)
        {
            HitTiles(targetTiles, Element.Water, "#256CCF", waterAtkScaling * baseAttack * waterBonus);

            SelectWater();
            menuManager.WaterMenu();
            audioManager.water.Play();
            waterCd = waterCooldown;
            gameManager.brinepiercerUses++;
        }
        
        if (fireSelected && fireCd <= 0)
        {
            GameObject f = Instantiate(ablazeFireballPrefab, gameManager.particlesParent);
            f.GetComponent<Projectile>().Init(new Vector3(gameManager.gemstone.position.x, gameManager.gemstone.position.y + 0.8f, -5), new Vector3(tilesGenerator.currentTile.transform.position.x, tilesGenerator.currentTile.transform.position.y, -5), 5, () =>
            {
                HitTiles(targetTiles, Element.Fire, "#DE2E21", fireAtkScaling * baseAttack * fireBonus);
                audioManager.fireExplosion.Play();
                GameObject p = Instantiate(ablazeExplosionPrefab, gameManager.particlesParent);
                p.transform.position = new Vector3(targetTiles.First().transform.position.x, targetTiles.First().transform.position.y, -5);
                p.GetComponent<ParticleSystem>().Play();
            });

            SelectFire();
            menuManager.FireMenu();
            audioManager.fire.Play();
            fireCd = fireCooldown;
            gameManager.ablazeUses++;
        }

        if (airSelected && airCd <= 0)
        {
            StartCoroutine(SkillOverTime(targetTiles, Element.Air, "#52CCA5", airAtkScaling * baseAttack * airBonus, 1, 4, (monster) =>
            {
                StartCoroutine(monster.DecreaseSpeed(false, 0.7f, 1));
            }));

            SelectAir();
            menuManager.AirMenu();
            airCd = airCooldown;
            gameManager.cycloneUses++;

            audioManager.air.Play();
            GameObject p = Instantiate(cycloneParticlesPrefab, gameManager.particlesParent);
            p.transform.position = new Vector3(tilesGenerator.currentTile.transform.position.x, tilesGenerator.currentTile.transform.position.y, -2);
            p.GetComponent<ParticleSystem>().Play();
        }
        if (earthSelected && earthCd <= 0)
        {
            HitTiles(targetTiles, Element.Earth, "#805029", earthAtkScaling * baseAttack * earthBonus);

            SelectEarth();
            menuManager.EarthMenu();
            earthCd = earthCooldown;
            gameManager.vanguardUses++;

            audioManager.earth.Play();
            GameObject w = Instantiate(vanguardWallPrefab, gameManager.constructParent);
            w.transform.position = tilesGenerator.currentTile.transform.position;
            w.GetComponent<SpriteRenderer>().sortingOrder = 200 - (int)(w.transform.position - gameManager.gemstone.position).y * 10 - 5;
            StartCoroutine(DelayedDestroy(w, 10));
        }
    }

    void HitTiles(List<Tile> targetTiles, Element element, string color, float amount, Action<Monster> additionalAction = null)
    {
        List<Monster> hitMonsters = new();
        foreach (Tile tile in targetTiles)
        {
            hitMonsters = hitMonsters.Concat(gameManager.monsters.Where(monster => monster.transform.position.x == tile.transform.position.x && monster.transform.position.y == tile.transform.position.y).ToList()).ToList();
        }

        foreach (Monster hitMonster in hitMonsters)
        {
            hitMonster.TakeDamage(element, color, amount);
            additionalAction?.Invoke(hitMonster);
        }
    }

    public void TakeDamage(Element element, string elementColor, float amount)
    {
        switch (element)
        {
            case Element.Water:
                gameManager.waterDamageTaken += amount;
                break;

            case Element.Fire:
                gameManager.fireDamageTaken += amount;
                break;

            case Element.Air:
                gameManager.airDamageTaken += amount;
                break;

            case Element.Earth:
                gameManager.earthDamageTaken += amount;
                break;
        }

        audioManager.monsterDamage.Play();
        Vector2 indicatorPosition = new Vector2(Random.Range(gameManager.gemstone.position.x + 1.5f, gameManager.gemstone.position.x - 1.5f), Random.Range(gameManager.gemstone.position.y + 1.5f, gameManager.gemstone.position.y + 2f));
        string indicatorText = $"<color={elementColor}>{Mathf.FloorToInt(amount)}";
        DamageIndicator(indicatorPosition, indicatorText);

        if (currentHealth - amount > 0)
        {
            currentHealth -= amount;
        }
        else
        {
            currentHealth = 0;
            Kill();
        }
    }

    void Kill()
    {
        gameManager.EndGame();

        int count = gameManager.monsters.Count;
        for (int i = 0; i < count; i++)
        {
            if (gameManager.monsters.Count >= 1)
            {
                gameManager.monsters[0].Kill(false);
            }
        }
    }

    public void Heal(float amount)
    {
        if (currentHealth + amount <= baseHealth)
        {
            currentHealth += amount;
        }
        else
        {
            currentHealth = baseHealth;
        }
    }

    public void DamageIndicator(Vector2 position, string text, FontStyles fontStyle = FontStyles.Normal)
    {
        GameObject damageIndicator = Instantiate(monsterManager.damageIndicatorPrefab, gameManager.damageIndicatorParent);
        damageIndicator.transform.position = position;
        damageIndicator.GetComponentInChildren<TMP_Text>().text = text;
        damageIndicator.GetComponentInChildren<TMP_Text>().fontStyle = fontStyle;
        StartCoroutine(DelayedDestroy(damageIndicator, 1.5f));
    }

    public IEnumerator SkillOverTime(List<Tile> targetTiles, Element element, string elementColor, float damagePerTick, float delay, int hits, Action<Monster> additionalAction = null)
    {
        int currentHits = 0;
        while (currentHits < hits && !gameObject.IsDestroyed())
        {
            currentHits++;
            HitTiles(targetTiles, element, elementColor, damagePerTick, additionalAction);
            yield return new WaitForSeconds(delay);
        }
    }

    public IEnumerator IncreaseAttack(bool flat, float value, float duration)
    {
        int baseStatistic = baseAttack;
        baseAttack = flat ? Mathf.RoundToInt(baseAttack + value) : Mathf.RoundToInt(baseAttack * value);
        attackText.text = baseAttack.ToString();
        int difference = baseAttack - baseStatistic;

        yield return new WaitForSeconds(duration);

        baseAttack -= difference;
        attackText.text = baseAttack.ToString();
    }

    public IEnumerator IncreaseHealth(bool flat, float value, float duration)
    {
        int baseStatistic = baseHealth;
        baseHealth = flat ? Mathf.RoundToInt(baseHealth + value) : Mathf.RoundToInt(baseHealth * value);
        healthText.text = baseHealth.ToString();
        int difference = baseHealth - baseStatistic;
        currentHealth += difference;

        yield return new WaitForSeconds(duration);

        baseHealth -= difference;
        healthText.text = baseHealth.ToString();
        if (currentHealth - difference > 0)
        {
            currentHealth -= difference;
        }
        else
        {
            currentHealth = 0;
            Kill();
        }
    }

    public IEnumerator DelayedDestroy(GameObject obj, float amount)
    {
        yield return new WaitForSeconds(amount);
        Destroy(obj);
    }
}
