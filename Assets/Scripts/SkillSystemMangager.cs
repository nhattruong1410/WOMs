using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystemMangager : MonoBehaviour
{
    //ENUM
    public enum MonsterSkill
    {
        //A for physical attack, D for defence, El for element, Ef for effect
        ABite,
        DSkinHarden,
        ElFireBall,
        EfMakeItRain
    }
    
    //VARIABLE

    //METHODS
    public void HandleSkill(MonsterSkill skill, MonsterStat caster, MonsterStat target)
    {
        switch (skill)
        {
            //Attack
            case MonsterSkill.ABite:
            {
                if (caster.mDamage > target.mDefence)
                {
                    target.mHealth -= caster.mDamage - target.mDefence;
                }
                else
                {
                    Debug.Log("Your attack is not strong enough");
                }
                break;
            }
            
            //Defence
            case MonsterSkill.DSkinHarden:
            {
                break;
            }
            
            //Element
            case MonsterSkill.ElFireBall:
            {
                break;
            }
            
            //Effect
            case MonsterSkill.EfMakeItRain:
            {
                break;
            }
        }
    }
    void AddSkill(MonsterStat monster, MonsterSkill skill)
    {
        monster.skillList.Add(skill);
    }
    
}
