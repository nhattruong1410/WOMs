using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    //Variables
    
    //Skill Spec
    public string skillName;
    public int skillLevel = 1;
    public int skillCost;

    public string SkillName
    {
        get => skillName;
        set => skillName = value; 
    }
    
    public int SkillLevel
    {
        get => skillLevel;
        set => skillLevel = value;
    }

    public int SkillCost
    {
        get => skillCost;
        set => skillCost = value;
    }

    //Method
    public abstract void UseSkill();

    public void IncreaseSkillLevel()
    {
        skillLevel++;
    }
}
