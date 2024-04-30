using System.Collections;
using System.Globalization;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    int level;
    float health;
    int attack;
    float speed;
    float attackSpeed;
    int range;
    int coins;
    int crystals;
    string color, elementColor;
    [HideInInspector]
    public Element element, weakness;
    [HideInInspector]
    public float basicAttackCooldown, bACd, moveCooldown, mCd;

    public SpriteRenderer shadow;
    public SpriteRenderer icon;
    public Slider healthBar;
    Transform gemstone;
    Vector3 gemstoneDistance;

    [SerializeField]
    AudioSource basicAttack;

    AudioManager audioManager;
    MonsterManager monsterManager;
    PlayerManager playerManager;
    GameManager gameManager;

    public void Init(MonsterData data, AudioManager audioManager, MonsterManager monsterManager, PlayerManager playerManager, GameManager gameManager)
    {
        level = gameManager.wave / 5 + 1; //1 Base +1 Level per 5 Waves
        health = level * 2 + 8; //+2 per Level
        attack = level + 4; //+1 per Level
        speed = level / 10f + 0.4f; //+0.1 per Level
        attackSpeed = level / 20f + 0.15f; //+0.05 per Level
        range = data.range;

        coins = Random.Range(level * 5, (level * 10) + 1);
        crystals = Random.Range(level * 2, (level * 5) + 1);

        element = data.element;
        weakness = data.weakness;
        switch (data.element)
        {
            case Element.Water:
                color = "#256CCF";
                elementColor = "#256CCF";
                break;

            case Element.Fire:
                color = "#C82E07";
                elementColor = "#DE2E21";
                break;

            case Element.Air:
                color = "#556D65";
                elementColor = "#52CCA5";
                break;

            case Element.Earth:
                color = "#432911";
                elementColor = "#805029";
                break;
        }

        basicAttackCooldown = 1f / attackSpeed;
        bACd = basicAttackCooldown;
        moveCooldown = 1f / speed;
        mCd = moveCooldown;

        icon.sprite = data.sprite;
        healthBar.maxValue = health;
        healthBar.value = health;
        gemstone = gameManager.gemstone;

        this.audioManager = audioManager;
        this.monsterManager = monsterManager;
        this.playerManager = playerManager;
        this.gameManager = gameManager;
    }

    void Update()
    {
        if (gemstoneDistance.magnitude > range)
        {
            mCd -= Time.deltaTime;
            Move();
        }
        else
        {
            bACd -= Time.deltaTime;
            BasicAttack();
        }
        gemstoneDistance = transform.position - gemstone.position;
        //Debug.Log($"{name} is {gemstoneDistance.y} away from the Gemstone on Y, so shadow layer = {shadow.sortingOrder} and icon layer = {icon.sortingOrder}");
    }

    public virtual void BasicAttack()
    {
        if (bACd <= 0)
        {
            bACd = basicAttackCooldown;
            basicAttack.Play();
            playerManager.TakeDamage(element, elementColor, attack);
        }
    }

    public void Move()
    {
        if (mCd <= 0)
        {
            mCd = moveCooldown;
            Vector3 newGemstoneDistance;
            if (Mathf.Abs(gemstoneDistance.x) > Mathf.Abs(gemstoneDistance.y))
            {
                newGemstoneDistance = new Vector3(gemstoneDistance.x / Mathf.Abs(gemstoneDistance.x) * (Mathf.Abs(gemstoneDistance.x) - 1), gemstoneDistance.y);
            }
            else
            {
                newGemstoneDistance = new Vector3(gemstoneDistance.x, gemstoneDistance.y - 1f);
            }
            transform.position = gemstone.position + newGemstoneDistance;
            shadow.sortingOrder = 200 - (int)gemstoneDistance.y * 10;
            icon.sortingOrder = shadow.sortingOrder + 1;
        }
    }

    public void TakeDamage(Element damageElement, string elementColor, float amount)
    {
        FontStyles indicatorFontStyle = FontStyles.Normal;
        if (damageElement == weakness)
        {
            amount *= 1.5f;
            indicatorFontStyle = FontStyles.Bold;
            audioManager.monsterDamageCritical.Play();
        }
        else if (damageElement == element)
        {
            amount /= 2f;
            indicatorFontStyle = FontStyles.Italic;
            audioManager.monsterDamageWeak.Play();
        }
        else
        {
            audioManager.monsterDamage.Play();
        }
        switch (damageElement)
        {
            case Element.Water:
                gameManager.waterDamageDealt += amount;
                break;

            case Element.Fire:
                gameManager.fireDamageDealt += amount;
                break;

            case Element.Air:
                gameManager.airDamageDealt += amount;
                break;

            case Element.Earth:
                gameManager.earthDamageDealt += amount;
                break;
        }

        Vector2 indicatorPosition = new Vector2(Random.Range(transform.position.x + 0.5f, transform.position.x - 0.5f), Random.Range(transform.position.y + 1.5f, transform.position.y + 2f));
        string indicatorText = $"<color={elementColor}>{(damageElement == weakness ? "*" : "")}{Mathf.FloorToInt(amount)}";
        playerManager.DamageIndicator(indicatorPosition, indicatorText, indicatorFontStyle);

        if (health - amount > 0)
        {
            health -= amount;
            healthBar.value = health;
        }
        else
        {
            Kill();
        }
    }

    public void Kill(bool drops = true, bool effects = true)
    {
        if (drops)
        {
            DropLoot();
            gameManager.monstersKilled++;
        }

        Destroy(gameObject);
        gameManager.monsters.Remove(this);

        if (effects)
        {
            GameObject p = Instantiate(monsterManager.deathParticlesPrefab, gameManager.particlesParent);
            var pSystem = p.GetComponentsInChildren<ParticleSystem>().Last().main;
            pSystem.startColor = new Color(int.Parse(color.Substring(1, 2), NumberStyles.HexNumber) / 255f, int.Parse(color.Substring(3, 2), NumberStyles.HexNumber) / 255f, int.Parse(color.Substring(5, 2), NumberStyles.HexNumber) / 255f);
            p.transform.position = transform.position;
            p.GetComponent<ParticleSystem>().Play();
            audioManager.monsterDeath.Play();
        }
    }

    void DropLoot()
    {
        int coinsAmount = Mathf.FloorToInt(coins * (1f + playerManager.baseFortune / 100f));
        playerManager.coins += coinsAmount;
        playerManager.coinsText.text = playerManager.coins.ToString();
        gameManager.coinsEarned += coinsAmount;

        int crystalsAmount = Mathf.FloorToInt(crystals * (1f + playerManager.baseFortune / 100f));
        playerManager.crystals += crystalsAmount;
        playerManager.crystalsText.text = playerManager.crystals.ToString();
        gameManager.crystalsEarned += crystalsAmount;
    }

    public IEnumerator DecreaseSpeed(bool flat, float value, float duration)
    {
        float baseStatistic = speed;
        speed = flat ? speed + value : speed * value;
        moveCooldown = 1f / speed;
        float difference = speed - baseStatistic;

        yield return new WaitForSeconds(duration);

        speed -= difference;
        moveCooldown = 1f / speed;
    }
}
