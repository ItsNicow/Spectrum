using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int wave;
    public int waveCooldown;
    int waveAmount;
    float wCd;
    public TMP_Text waveText;
    public Slider waveSlider;

    public int monstersKilled, coinsEarned, coinsSpent, crystalsEarned, crystalsSpent, tearUses, bladeUses, ringUses, prismUses;
    public int totalSkillUses, totalSkillLevel, brinepiercerUses, ablazeUses, cycloneUses, vanguardUses;
    public float damageDealt, waterDamageDealt, fireDamageDealt, airDamageDealt, earthDamageDealt, damageTaken, waterDamageTaken, fireDamageTaken, airDamageTaken, earthDamageTaken;
    public TMP_Text endGameStatistics;
    public ScrollRect endGameScrollRect;

    public GameStatus status;
    public GameDifficulty difficulty;

    public List<Monster> monsters = new();

    public Slider gemstoneSlider;
    public Transform gemstone;
    public Animator gemstoneAnimator;
    public GameObject gemstoneParticlesPrefab;
    GameObject gemstoneParticles;

    public Transform constructParent, monsterParent, damageIndicatorParent, particlesParent;

    [SerializeField]
    MonsterManager monsterManager;
    [SerializeField]
    MenuManager menuManager;
    [SerializeField]
    InputManager inputManager;
    [SerializeField]
    ArtifactManager artifactManager;
    public PlayerManager playerManager;

    private void Awake()
    {
        wCd = 0;
        waveText.text = wave.ToString();

        status = GameStatus.Ongoing;

        gemstoneParticles = Instantiate(gemstoneParticlesPrefab, particlesParent);
        gemstoneParticles.transform.position = new Vector3(gemstone.position.x, gemstone.position.y + 0.8f);
        gemstoneParticles.SetActive(false);
    }

    private void Update()
    {
        UpdateCooldowns(Time.deltaTime);
        UpdateDisplay();

        gemstoneAnimator.speed = (playerManager.currentHealth >= playerManager.baseHealth / 2) ? 1 : (playerManager.currentHealth >= playerManager.baseHealth / 5) ? 2 : 4 ;
        gemstoneParticles.SetActive(playerManager.currentHealth < playerManager.baseHealth / 5);

        if (status == GameStatus.Ongoing)
        {
            if (wCd <= 0)
            {
                wCd = waveCooldown;
                wave++;
                waveText.text = wave.ToString();
                waveAmount = ((((wave / 10 + 1) * 2) + 1) < 20) ? ((wave / 10 + 1) * 2) + 1 : 20;
                waveAmount = Random.Range(wave / 10 + 1, waveAmount);
                monsterManager.SummonWave(waveAmount);
                waveSlider.maxValue = monsters.Count;
            }

            if (monsters.Count == 0 && wCd > 5)
            {
                wCd = 5;
            }
        }
    }

    private void UpdateCooldowns(float dT)
    {
        wCd -= dT;
    }

    private void UpdateDisplay()
    {
        waveSlider.value = monsters.Count;

        gemstoneSlider.maxValue = playerManager.baseHealth;
        gemstoneSlider.value = playerManager.currentHealth;
    }

    public void EndGame()
    {
        status = GameStatus.Ended;

        inputManager.DisableInputs();
        inputManager.UnsubscribeActions();
        menuManager.background.SetActive(true);
        menuManager.EndGameMenu();

        damageDealt = waterDamageDealt + fireDamageDealt + airDamageDealt + earthDamageDealt;
        damageTaken = waterDamageTaken + fireDamageTaken + airDamageTaken + earthDamageTaken;
        totalSkillUses = brinepiercerUses + ablazeUses + cycloneUses + vanguardUses;
        totalSkillLevel = playerManager.waterLevel + playerManager.fireLevel + playerManager.airLevel + playerManager.earthLevel;

        endGameStatistics.text = $"{wave}\n{monstersKilled}\n\n{Mathf.RoundToInt(damageDealt)}\n{Mathf.RoundToInt(waterDamageDealt)}\n{Mathf.RoundToInt(fireDamageDealt)}\n{Mathf.RoundToInt(airDamageDealt)}\n{Mathf.RoundToInt(earthDamageDealt)}\n\n" +
            $"{Mathf.RoundToInt(damageTaken)}\n{Mathf.RoundToInt(waterDamageTaken)}\n{Mathf.RoundToInt(fireDamageTaken)}\n{Mathf.RoundToInt(airDamageTaken)}\n{Mathf.RoundToInt(earthDamageTaken)}\n\n" +
            $"{coinsEarned}\n{coinsSpent}\n{crystalsEarned}\n{crystalsSpent}\n\n{artifactManager.ownedArtifacts.Count}\n{tearUses}\n{bladeUses}\n{ringUses}\n{prismUses}\n\n" +
            $"{totalSkillUses}\n{totalSkillLevel}\n{brinepiercerUses}\n{playerManager.waterLevel}\n{ablazeUses}\n{playerManager.fireLevel}\n{cycloneUses}\n{playerManager.airLevel}\n{vanguardUses}\n{playerManager.earthLevel}";
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

public enum GameStatus
{
    Ongoing,
    Ended
}

public enum GameDifficulty
{
    Normal,
    Hard
}
