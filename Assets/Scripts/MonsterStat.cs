using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    //VARIABLES
    //Stats
    public string mName = "Monster";
    public float mHealth = 100.0f;
    public float mDamage = 20.0f;
    public float mDefence = 10.0f;
    //Skill
    [SerializeField] private SkillSystemMangager skillManager;
    public List<SkillSystemMangager.MonsterSkill> skillList = new List<SkillSystemMangager.MonsterSkill>();
}
