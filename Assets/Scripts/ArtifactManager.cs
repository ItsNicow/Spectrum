using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactManager : MonoBehaviour
{
    [Header("Artifacts")]
    public List<ArtifactData> artifacts = new();
    [HideInInspector]
    public List<Artifact> ownedArtifacts = new();
    [HideInInspector]
    public List<Button> artifactButtons;

    [Header("Shop Menu")]
    public GameObject artifactPrefab;
    public Transform artifactParent;

    [Header("UI")]
    public Transform ownedArtifactParent;

    [SerializeField]
    PlayerManager playerManager;
    [SerializeField]
    AudioManager audioManager;
    [SerializeField]
    GameManager gameManager;

    private void Awake()
    {
        for (int i = 0; i < artifacts.Count; i++)
        {
            GameObject a = Instantiate(artifactPrefab, artifactParent);

            TMP_Text[] texts = a.GetComponentsInChildren<TMP_Text>();
            texts[1].text = artifacts[i].name;
            texts[2].text = artifacts[i].passiveName;
            texts[3].text = artifacts[i].passiveDescription;
            texts[4].text = artifacts[i].activeName + " (" + artifacts[i].cooldown + ")";
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
    }

    public void PurchaseArtifact(ArtifactData artifact)
    {
        GameObject a = Instantiate(artifact.artifactPrefab, ownedArtifactParent);

        Artifact artifactComponent = a.GetComponent<Artifact>();
        artifactComponent.Init(artifact, audioManager, this, gameManager);

        ownedArtifacts.Add(artifactComponent);
    }

    public void DisableArtifact(Type type)
    {
        foreach (Artifact artifact in ownedArtifacts)
        {
            if (artifact.GetType() == type)
            {
                artifact.cd = artifact.cooldown;
            }
        }
    }

    public void ReduceCooldowns(bool skills, bool artifacts, float amount)
    {
        if (skills)
        {
            playerManager.waterCd -= amount;
            playerManager.fireCd -= amount;
            playerManager.airCd -= amount;
            playerManager.earthCd -= amount;
        }

        if (artifacts)
        {
            foreach (Artifact artifact in ownedArtifacts)
            {
                artifact.cd -= amount;
            }
        }
    }
}
