using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EMonsterStat : MonsterStat, IPointerClickHandler
{
    public CombatSystemManager csManager;
    public SkillSystemMangager ssMangager;

    public void OnPointerClick()
    {
        Debug.Log("Monster Click!");
        if (csManager.selectedSkill != SkillSystemMangager.MonsterSkill.Default)
        {
            ssMangager.HandleSkill(csManager.selectedSkill, csManager.pMonsterStat, this);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
    }
}
