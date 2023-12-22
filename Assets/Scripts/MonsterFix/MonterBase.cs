using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Monster", menuName = "Monster/Create new Monster")]
public class MonterBase : ScriptableObject
{
    //Tên và giới thiệu về Monster
    [SerializeField] string name;
    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite monsterSprite;

    //Loại Monster
    [SerializeField] MonsterType type;

    //Chỉ số
    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defence;

    [SerializeField] List<LearnableMove> learnableMoves;

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }   
    
    public Sprite MonsterSprite
    {
        get { return monsterSprite; }
    }

    public MonsterType Type { get { return type; } }

    
    public int MaxHP
    {
        get { return maxHP; }
    }

    public int Attack
    {
        get { return attack; }
    }
    public int Defence
    {
        get { return defence; }
    }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }
}
[System.Serializable]
public class LearnableMove
{
    [SerializeField] MonterMove monterMove;
    [SerializeField] int level;
    public MonterMove Base
    {
        get
        {
            return monterMove;
        }
    }
    public int Level
    {
        get { return level; }   
    }
}
public enum MonsterType
{
    Fire,
    Water,
    Leaf,
    Ice,
    Earth,
}
