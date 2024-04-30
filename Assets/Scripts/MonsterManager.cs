using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Monster;

public class MonsterManager : MonoBehaviour
{
    [Header("Monsters")]
    public List<MonsterData> monsters = new();
    public GameObject monsterPrefab;
    public GameObject damageIndicatorPrefab;
    public GameObject deathParticlesPrefab;

    [SerializeField]
    AudioManager audioManager;
    [SerializeField]
    PlayerManager playerManager;
    [SerializeField]
    GameManager gameManager;

    public void SummonWave(int amount)
    {
        List<MonsterData> waveMonsters = new(monsters.OrderBy(x => Random.value).Take(amount));

        for (int i = 0; i < waveMonsters.Count; i++)
        {
            GameObject m = Instantiate(waveMonsters[i].monsterPrefab, gameManager.monsterParent);

            Monster monsterComponent = m.GetComponent<Monster>();
            monsterComponent.Init(waveMonsters[i], audioManager, this, playerManager, gameManager);
            //m.transform.position = new Vector2(Random.Range(-8, 9), Random.Range(8, 13));
            m.transform.position = new Vector2(Random.Range(-8, 3), Random.Range(7, -3));

            gameManager.monsters.Add(monsterComponent);
        }
    }
}
