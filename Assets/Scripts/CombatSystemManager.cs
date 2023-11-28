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
    //Variables
    
    //Spawning Characters
    //--Player--
    public GameObject pMonsterPrefab;
    public Transform pMonsterSpawnPos;
    public GameObject pMonster;
    
    //--Enemies
    private const int EMONSTERLISTLENGTH = 3;
    public GameObject[] eMonsterPrefabs = new GameObject[EMONSTERLISTLENGTH];
    public Transform[] eMonsterSpawnPos = new Transform[EMONSTERLISTLENGTH];
    public GameObject[] eMonsters = new GameObject[EMONSTERLISTLENGTH];

    public CombatState combatState;
    
    //HUD
    public TextMeshProUGUI combatStateText;
    
    
    //Method
    
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
        }
    }
    void SpawnEnemyMonsters()
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
            case CombatState.Start:
            {
                SetupHUD();
                
                //Spawn Player Monster
                SpawnPMonster();
                
                //Spawn Enemies Monsters
                SpawnEnemyMonsters();
                
                StartCoroutine(ChangeState(CombatState.PlayerTurn));
                break;
            }
            case CombatState.PlayerTurn:
            {
                UpdateState(CombatState.PlayerTurn);
                break;
            }
            case CombatState.EnemyTurn:
            {
                UpdateState(CombatState.EnemyTurn);
                
                for (int i = 0; i < EMONSTERLISTLENGTH; i++)
                {
                    Debug.Log("EMonster" + i + " is attacking");
                }

                StartCoroutine(ChangeState(CombatState.End));
                
                break;
            }
            case CombatState.End:
            {
                UpdateState(CombatState.End);
                break;
            }
            default:
            {
                Debug.Log("Error Went To Debug State");
                break;
            }
        }
    }
    private void UpdateState(CombatState state)
    {
        combatState = state;
        ChangeStateHUD(state);
    }
    IEnumerator ChangeState(CombatState state)
    {
        yield return new WaitForSeconds(1);
        
        HandleCombatState(state);
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
    
    //--OnClick--
    public void PlayerEndTurn()
    {
        if (combatState == CombatState.PlayerTurn)
        {
            HandleCombatState(CombatState.EnemyTurn);
        }
        
        Debug.Log("End Turn");
    }
}
