using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Monster")]
public class MonsterData : ScriptableObject
{
    [Header("Monster Information")]
    public float health;
    public int attack;
    public float speed;
    public float attackSpeed;
    public int range;
    public Element element;
    public Element weakness;
    public GameObject monsterPrefab;

    [Header("Visual")]
    public Sprite sprite;
}

public enum Element
{
    Water = 0,
    Fire = 1,
    Air = 3,
    Earth = 4
}
