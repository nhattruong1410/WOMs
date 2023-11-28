using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMonsterStat : MonsterStat
{
    //Variables
    public List<String> skillList;

    private void Start()
    {
        skillList = new List<String>();
    }

    //Methods
    public void AddSkill(String skillName)
    {
        skillList.Add(skillName);
    }
}
