using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CenterBG : MonoBehaviour, IPointerClickHandler
{
    //VARIABLES
    public CombatSystemManager combatSystemManager;
    // Start is called before the first frame update
    public float distanceFromCamera = 5f;
    
    //METHODS
    void Start()
    {
        Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distanceFromCamera));
        gameObject.transform.position = centerPos;

        var topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        var worldSpaceWidth = topRightCorner.x * 2;
        var worldSpaceHeight = topRightCorner.y * 2;

        var scaleFactorX = worldSpaceWidth / transform.localScale.x;
        var scaleFactorY = worldSpaceHeight / transform.localScale.y;

        transform.localScale = new Vector3(scaleFactorX, scaleFactorY, 1);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (combatSystemManager)
        {
            combatSystemManager.selectedSkill = SkillSystemMangager.MonsterSkill.Default;
        }
        Debug.Log("Click InvinsibleBG");
    }
}
