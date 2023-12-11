using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;
using UnityEngine.EventSystems;

public class EMonster : Monster, IPointerClickHandler
{
    //VARIABLES
    [Header("Combat")]
    public CombatSystemManager combatManager;

    //METHODS
    private void Start()
    {
        skillManager = FindObjectOfType<SkillSystemMangager>();
        combatManager = FindObjectOfType<CombatSystemManager>();

    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Monster Click!");
        combatManager.monsterSelected = this;
        
        //Change Selected Monster to this Monster
        if (combatManager.selectedSkill != SkillSystemMangager.MonsterSkill.Default)
        {
            skillManager.HandleSkill(combatManager.selectedSkill, combatManager.pMonsterGO, this);
            combatManager.PlayerEndTurn();
        }
    }

}
