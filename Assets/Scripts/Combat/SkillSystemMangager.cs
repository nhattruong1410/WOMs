using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystemMangager : MonoBehaviour
{
    //ENUM
    public enum MonsterAttackSkill
    {
        //A for physical attack, D for defence, El for element, Ef for effect
        Default,
        ABite,
        ElFireBall
    }
    
    //VARIABLEQ

    //METHODS
    public void HandleAttackSkill(MonsterAttackSkill skill, GameObject casterGo, Monster target)
    {
      
        
        if (casterGo == null || target == null)
        {
            Debug.LogError("Invalid caster or target object.");
            return;
        }

        Monster caster = casterGo.GetComponent<Monster>();
        if (caster == null)
        {
            Debug.LogError("Caster does not have Monster component.");
            return;
        }
        
        if (!target)
        {
            Debug.Log("Please Choose Enemy Monster");
            return;
        }

        switch (skill) 
        {
            //Attack
            case MonsterAttackSkill.ABite:
            {
                if (caster.mDamage > target.mDefence)
                {
                    float damage = CalculateBiteDamage(caster, target);
                    ApplyBiteDamage(target, damage);
                }
                else
                {
                    Debug.Log("The attack is not strong enough to pierce the target's defense.");
                }
                break;
            }

            //Element
            case MonsterAttackSkill.ElFireBall:
            {
                
                if (caster.mDamage > target.mDefence)
                {
                    float damage = CalculateFireBallDamage(caster, target);
                    ApplyFireBallDamage(target, damage);
                }
                else
                {
                    Debug.Log("The fireball is not strong enough to overcome the target's elemental resistance.");
                }
                break;
            }
        }
    }

    void AddAttackSkill(Monster monster, MonsterAttackSkill skill)
    {
        monster.skillList.Add(skill);
    }
    
    
    //Bite Skill
    private float CalculateBiteDamage(Monster caster, Monster target)
    {
        // Calculate the bite damage based on caster's damage and target's defense
        float biteDamage = caster.mDamage - target.mDefence;
        return Mathf.Max(biteDamage, 0); // Ensure damage doesn't go below zero
    }

    private void ApplyBiteDamage(Monster target, float damage)
    {
        if (damage > 0)
        {
            target.mCurrentHealth -= damage;
            target.healthBar.SetHealth(target.NormalizeCurrentHealth());

            if (target.mCurrentHealth <= 0)
            {
                Debug.Log("Target destroyed.");
                Destroy(target.gameObject);
            }
        }
        else
        {
            Debug.Log("The target's defense nullified the bite damage.");
        }
    }
    
    //Fireball
    private float CalculateFireBallDamage(Monster caster, Monster target)
    {
        // Calculate the fireball damage based on caster's elemental damage and target's resistance
        float fireBallDamage = caster.mDamage - target.mDefence;
        return Mathf.Max(fireBallDamage, 0); // Ensure damage doesn't go below zero
    }

    private void ApplyFireBallDamage(Monster target, float damage)
    {
        if (damage > 0)
        {
            target.mCurrentHealth -= damage;
            target.healthBar.SetHealth(target.NormalizeCurrentHealth());

            if (target.mCurrentHealth <= 0)
            {
                Debug.Log("Target destroyed.");
                Destroy(target.gameObject);
            }
        }
        else
        {
            Debug.Log("The target's elemental resistance nullified the fireball damage.");
        }
    }

}
