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
    public float mMaxHealth = 100f;
    public float mCurrentHealth = 100f;
    public float mDamage = 20f;
    public float mDefence = 10f;
    
    //Skill
    [Header("Skill")]
    [SerializeField] public SkillSystemMangager skillManager;
    public List<SkillSystemMangager.MonsterAttackSkill> skillList = new List<SkillSystemMangager.MonsterAttackSkill>();
    
    
    [Header("HUD")]
    //HUD
    public HealthBar healthBar;
    
    //METHODS
    private void Start()
    {
        mCurrentHealth = mMaxHealth;
        if(healthBar)
         healthBar.SetHealth(NormalizeCurrentHealth());
    }
    
    public float NormalizeCurrentHealth()
    {
        return (mCurrentHealth / mMaxHealth);
    }
}
