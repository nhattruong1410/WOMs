using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Move", menuName = "Monster/Create new move")]
public class MonterMove : ScriptableObject
{
    [SerializeField] string name;
    [TextArea]
    [SerializeField] string description;

    //Loại Monster
    [SerializeField] MonsterType type;

    //Chỉ số
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int pp;

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

     public int Power
    {
        get { return power; }
    }

    public int Accuracy
    {
        get { return accuracy; }
    }
    public int PP
    {
        get { return pp; }
    }
}
