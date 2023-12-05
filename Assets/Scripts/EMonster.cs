using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EMonster : Monster, IPointerClickHandler
{
    public CombatSystemManager csManager;
    public SkillSystemMangager ssMangager;

    public void OnPointerClick()
    {
        Debug.Log("Monster Click!");
        if (csManager.selectedSkill != SkillSystemMangager.MonsterSkill.Default)
        {
            ssMangager.HandleSkill(csManager.selectedSkill, csManager.pMonster, this);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
    }
}
