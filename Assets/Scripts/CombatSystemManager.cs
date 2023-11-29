using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CombatState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    End
}
public class CombatSystemManager : MonoBehaviour
{
    //VARIABLES
    //Spawning Characters
    //--Player--
    public GameObject pMonsterPrefab;
    public Transform pMonsterSpawnPos;
    public GameObject pMonster;
    public PMonsterStat pMonsterStat;
    
    //--Enemies--
    private const int EMONSTERLISTLENGTH = 3;
    public GameObject[] eMonsterPrefabs = new GameObject[EMONSTERLISTLENGTH];
    public Transform[] eMonsterSpawnPos = new Transform[EMONSTERLISTLENGTH];
    public GameObject[] eMonsters = new GameObject[EMONSTERLISTLENGTH];

    //--State--
    public CombatState combatState;
    
    //--Skill--
    public  SkillSystemMangager skillSystemMangager;
    
    //--HUD--
    public TextMeshProUGUI combatStateText;
    
    //METHODS
    void Start()
    {
        HandleCombatState(CombatState.Start);
        
    }
    
    //--Spawn Monster--
    void SpawnPMonster()
    {
        if (pMonsterPrefab)
        {
            GameObject pMonsterTemp = Instantiate(pMonsterPrefab, pMonsterSpawnPos.position, Quaternion.identity);
            pMonster = pMonsterTemp;
            if (pMonster) pMonsterStat = pMonster.GetComponent<PMonsterStat>(); 
            if (pMonsterStat)
            {
                pMonsterStat.skillList.Add(SkillSystemMangager.MonsterSkill.ABite);
                Debug.Log("Add Bite skill");
            }
        }
    }
    void SpawnEMonsters()
    {
        for(int i = 0; i < EMONSTERLISTLENGTH; i++)
        {   
            if (eMonsterPrefabs[i])
            {
                GameObject eMonsterTemp = Instantiate(eMonsterPrefabs[i], eMonsterSpawnPos[i].position, Quaternion.identity);
                eMonsters[i] = eMonsterTemp;
            }
        }
    }
    
    //--State Handling--
    void HandleCombatState(CombatState state)
    {
        switch (state)
        {
            //Combat Setup
            case CombatState.Start:
            {
               HandleCombatStart();
                break;
            }
            
            //Player Turn
            case CombatState.PlayerTurn:
            {
                HandlePlayerTurn();
                break;
            }
            
            //Enemy Turn
            case CombatState.EnemyTurn:
            {
                HandleEnemyTurn();
                break;
            }
            
            //Combat End
            case CombatState.End:
            {
                HandleCombatEnd();
                break;
            }
            default:
            {
                Debug.Log("Error Went To Debug State");
                break;
            }
        }
    }
    
    //--Combat State Handle--
    private void UpdateCombatState(CombatState state)
    {
        combatState = state;
        ChangeStateHUD(state);
    }
    IEnumerator ChangeCombatState(CombatState state)
    {
        yield return new WaitForSeconds(1);
        
        HandleCombatState(state);
    }
    private void HandleCombatStart()
    {
        SetupHUD();
                
        //Spawn Player Monster
        SpawnPMonster();
        
        //Spawn Enemies Monsters
        SpawnEMonsters();
                
        StartCoroutine(ChangeCombatState(CombatState.PlayerTurn));
       
    }
    private void HandlePlayerTurn()
    {
        UpdateCombatState(CombatState.PlayerTurn);
        skillSystemMangager.HandleSkill(pMonsterStat.skillList[0], pMonsterStat, eMonsters[0].GetComponent<MonsterStat>());
    }
    private void HandleEnemyTurn()
    {
        UpdateCombatState(CombatState.EnemyTurn);
                
        for (int i = 0; i < EMONSTERLISTLENGTH; i++)
        {
            Debug.Log("EMonster" + i + " is attacking");
        }

        StartCoroutine(ChangeCombatState(CombatState.End));
    }
    private void HandleCombatEnd()
    {
        UpdateCombatState(CombatState.End);
    }
    
    //--OnClick--
    public void PlayerEndTurn()
    {
        if (combatState == CombatState.PlayerTurn)
        {
            HandleCombatState(CombatState.EnemyTurn);
        }
        
        Debug.Log("End Turn");
    }
  
    //--HUD--
    void SetupHUD()
    {
        ChangeStateHUD(CombatState.Start);
    }
    void ChangeStateHUD(CombatState state)
    {
        combatStateText.SetText("CombatState: " + state.ToString());
    }
    
 
    
}
