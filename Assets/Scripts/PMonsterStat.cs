using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMonsterStat : MonsterStat
{
    //VARIABLES
    

    //METHODS
    public void AddSkill(SkillSystemMangager.MonsterSkill skill)
    {
        skillList.Add(skill);
    }
}
