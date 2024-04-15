using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactManager : MonoBehaviour
{
    [Header("Artifacts")]
    public List<Artifact> artifacts = new();
    public List<Artifact> ownedArtifacts = new();
    public List<Button> artifactButtons;
    public List<Button> ownedArtifactButtons;

    [Header("Shop Menu")]
    public GameObject artifactPrefab;
    public Transform artifactParent;

    [Header("UI")]
    public GameObject ownedArtifactPrefab;
    public Transform ownedArtifactParent;

    [SerializeField]
    PlayerManager playerManager;
    [SerializeField]
    AudioManager audioManager;

    private void Awake()
    {
        for (int i = 0; i < artifacts.Count; i++)
        {
            GameObject a = GameObject.Instantiate(artifactPrefab, artifactParent);

            TMP_Text[] texts = a.GetComponentsInChildren<TMP_Text>();
            texts[1].text = artifacts[i].name;
            texts[2].text = artifacts[i].passiveName;
            texts[3].text = artifacts[i].passiveDescription;
            texts[4].text = artifacts[i].activeName + " (" + artifacts[i].cooldown + "s)";
            texts[5].text = artifacts[i].activeDescription;
            texts[6].text = artifacts[i].price.ToString();

            a.GetComponentsInChildren<Image>()[1].sprite = artifacts[i].sprite;
            int iCopy = i;
            a.GetComponent<Button>().onClick.AddListener(() =>
            {
                playerManager.PurchaseArtifact(artifacts[iCopy]);
                audioManager.PurchaseClick();
            });
        }

        artifactButtons = artifactParent.GetComponentsInChildren<Button>().ToList();

        foreach (Artifact artifact in artifacts)
        {
            artifact.cd = 0;
            artifact.active = audioManager.artifacts.Where(source => source.clip.name.ToLower() == artifact.name.Replace(" ", "").Replace("'", "").ToLower()).First();
            artifact.audioManager = audioManager;
        }
    }

    private void Update()
    {
        UpdateCooldowns(Time.deltaTime);
        UpdateDisplay();
    }

    void UpdateCooldowns(float dT)
    {
        foreach (Artifact artifact in artifacts)
        {
            artifact.cd -= dT;
        }
    }

    void UpdateDisplay()
    {
        for (int i = 0; i < ownedArtifacts.Count; i++)
        {
            ownedArtifactButtons[i].gameObject.GetComponentInChildren<TMP_Text>().text = Mathf.RoundToInt(ownedArtifacts[i].cd).ToString();
        }
    }

    public void PurchaseArtifact(Artifact artifact)
    {
        GameObject a = GameObject.Instantiate(ownedArtifactPrefab, ownedArtifactParent);

        a.GetComponent<Image>().sprite = artifact.sprite;
        a.GetComponent<Button>().onClick.AddListener(() =>
        {
            artifact.Active();
        });

        ownedArtifactButtons.Add(a.GetComponent<Button>());
    }
}
