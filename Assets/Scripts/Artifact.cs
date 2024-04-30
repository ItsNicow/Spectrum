using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Artifact : MonoBehaviour
{
    [HideInInspector]
    public string artifactName;
    string passiveName;
    string passiveDescription;
    string activeName;
    string activeDescription;
    int price;
    [HideInInspector]
    public float cooldown, cd;

    public Image icon;
    public Button button;
    public TMP_Text cooldownText;

    [SerializeField]
    AudioSource active;

    AudioManager audioManager;
    protected ArtifactManager artifactManager;
    protected GameManager gameManager;

    public void Init(ArtifactData data, AudioManager audioManager, ArtifactManager artifactManager, GameManager gameManager)
    {
        artifactName = data.name;
        passiveName = data.passiveName;
        passiveDescription = data.passiveDescription;
        activeName = data.activeName;
        activeDescription = data.activeDescription;
        price = data.price;
        cooldown = data.cooldown;
        cd = 0;
        
        icon.sprite = data.sprite;
        button.onClick.AddListener(() =>
        {
            Active();
        });

        this.audioManager = audioManager;
        this.artifactManager = artifactManager;
        this.gameManager = gameManager;
    }

    void Update()
    {
        cd -= Time.deltaTime;

        cooldownText.enabled = cd > 0;
        cooldownText.text = Mathf.RoundToInt(Mathf.Ceil(cd)).ToString();

        button.interactable = cd <= 0;
    }

    public virtual void Passive()
    {

    }

    public virtual void Active()
    {
        if (cd <= 0)
        {
            cd = cooldown;
            active.Play();
        }
    }
}
