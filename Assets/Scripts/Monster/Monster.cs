using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //VARIABLES
    //Stats
    [Header("Monster Stats")]
    public string mName = "Monster";
    public int mMaxHealth = 100;
    public int mCurrentHealth = 100;
    public int mDamage = 20;
    public int mDefence = 10;
    
    //Skill
    [Header("Skill")]
    [SerializeField] public SkillSystemMangager skillManager;
    public List<SkillSystemMangager.MonsterSkill> skillList = new List<SkillSystemMangager.MonsterSkill>();
    
    //HUD
    public HealthBar healthBar;
    
    //METHODS
    private void Start()
    {
        mCurrentHealth = mMaxHealth;
        healthBar.SetHealth(mMaxHealth);
    }
}
