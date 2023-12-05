using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
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
    [Header("Spawning")]
    //--Player--
    public GameObject pMonsterPrefab;
    public Transform pMonsterSpawnPos;
    public PMonster pMonsterStat;
    //--Enemies--
    [SerializeField] private const int EMONSTERLISTLENGTH = 3;
    public Transform[] eMonsterSpawnPos = new Transform[EMONSTERLISTLENGTH];
    public GameObject[] eMonsterPrefabs = new GameObject[EMONSTERLISTLENGTH];
    
    //--Monster
    [Header("Monster")]
    public GameObject pMonster;
    public GameObject[] eMonsters = new GameObject[EMONSTERLISTLENGTH];
    
    //--State--
    [Header("State")]
    public CombatState combatState;
    
    //--PlayerState--
    [Header("PlayerState")] 
    public bool isPlayerTurn = false;
    public SkillSystemMangager.MonsterSkill selectedSkill = SkillSystemMangager.MonsterSkill.Default;
    public Monster monsterSelected;


    //--Skill--
    [FormerlySerializedAs("skillSystemMangager")] public  SkillSystemMangager skillMangager;
    
    //--HUD--
    [Header("HUD")]
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
            if (pMonster) pMonsterStat = pMonster.GetComponent<PMonster>(); 
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
        
        if(selectedSkill != SkillSystemMangager.MonsterSkill.Default)
        {
            StartCoroutine(RemindPlayerTurn());
            Debug.Log("Please select a skill");
        }
    }

    IEnumerator RemindPlayerTurn()
    {
        yield return new WaitForSeconds(5);
        
        HandlePlayerTurn();
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
    
    //--PlayerState--
    public void PlayerSelectedSkill(SkillSystemMangager.MonsterSkill skill)
    {
        selectedSkill = skill;
    }
    public void PlayerSelectedEMonsters(Monster monster) 
    {
       
    }
    public void PlayerEndTurn()
    {
        if (combatState == CombatState.PlayerTurn)
        {
            HandleCombatState(CombatState.EnemyTurn);
        }
        
        Debug.Log("End Turn");
    }//OnClick
  
    //--HUD--
    void SetupHUD()
    {
        ChangeStateHUD(CombatState.Start);
    }
    void ChangeStateHUD(CombatState state)
    {
        combatStateText.SetText("CombatState: " + state);
    }
    
 
    
}
