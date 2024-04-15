using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Artifact")]
public class Artifact : ScriptableObject
{
    [Header("Artifact Information")]
    new public string name;
    public string passiveName;
    public string passiveDescription;
    public string activeName;
    public string activeDescription;
    public int price;
    public float cooldown;
    public float cd;

    [Header("Visual")]
    public Sprite sprite;

    [Header("Audio")]
    public AudioSource active;
    public AudioManager audioManager;

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
