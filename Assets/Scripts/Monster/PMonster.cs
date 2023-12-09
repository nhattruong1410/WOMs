using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMonster : Monster
{
    //VARIABLES
    

    //METHODS
    public void AddSkill(SkillSystemMangager.MonsterSkill skill)
    {
        skillList.Add(skill);
    }
}
