using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    public SkillSystemMangager.MonsterSkill buttonSkill;
    public CombatSystemManager combatSystemManager;

    public void OnClick()
    {
        combatSystemManager.selectedSkill = buttonSkill;
    }
}
