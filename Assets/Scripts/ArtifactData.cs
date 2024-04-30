using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Artifact")]
public class ArtifactData : ScriptableObject
{
    [Header("Artifact Information")]
    new public string name;
    public string passiveName;
    public string passiveDescription;
    public string activeName;
    public string activeDescription;
    public int price;
    public float cooldown;
    public GameObject artifactPrefab;

    [Header("Visual")]
    public Sprite sprite;
}
