using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Combat
{
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
        public PMonster pMonster;
    
        //--Monster--
        [Header("Monster")]
        public const int EMONSTERLISTLENGTH = 3;
        public Transform[] eMonsterSpawnPos = new Transform[EMONSTERLISTLENGTH];
        public GameObject[] eMonsterPrefabs = new GameObject[EMONSTERLISTLENGTH];
        private int deathMonstCounter = 0;
        
        [Header("EMonsterList")]
        public List<EMonster> eMonsters = new List<EMonster>(3);
    
        //--MonsterGO--
        [Header("MonsterGO")]
        public GameObject pMonsterGO;
        public GameObject[] eMonsterGOs;

        //--State--
        [Header("State")]
        public CombatState combatState;
    
        //--PlayerState--
        [Header("PlayerState")] 
        public bool isPlayerTurn = false;
        public SkillSystemMangager.MonsterSkill selectedSkill = SkillSystemMangager.MonsterSkill.Default;
        public Monster monsterSelected;


        //--Skill--
        [FormerlySerializedAs("skillSystemManager")] public  SkillSystemMangager skillMangager;
    
        //--HUD--
        [Header("HUD")]
        public TextMeshProUGUI combatStateText;
    
        //METHODS
        void Start()
        {
            HandleCombatState(CombatState.Start);
            for (int i = 0; i < EMONSTERLISTLENGTH; i++)
            {
                EMonster monster = eMonsterGOs[i].GetComponent<EMonster>();
                if(monster)
                    eMonsters[i] = monster;
            }
        }

        //--Spawn Monster--
        void SpawnPMonster()
        {
            if (pMonsterPrefab)
            {
                GameObject pMonsterTemp = Instantiate(pMonsterPrefab, pMonsterSpawnPos.position, Quaternion.identity);
                pMonsterGO = pMonsterTemp;
                if (pMonsterGO) pMonster = pMonsterGO.GetComponent<PMonster>(); 
                if (pMonster)
                {
                    pMonster.skillList.Add(SkillSystemMangager.MonsterSkill.ABite);
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
                    eMonsterGOs[i] = eMonsterTemp;
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
    
        //CombatStartTurn
        private void HandleCombatStart()
        {
            SetupHUD();
                
            //Spawn Player Monster
            SpawnPMonster();
        
            //Spawn Enemies Monsters
            SpawnEMonsters();
                
            StartCoroutine(ChangeCombatState(CombatState.PlayerTurn));
       
        }
    
        //--PlayerTurn--
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
        public void PlayerSelectedSkill(SkillSystemMangager.MonsterSkill skill)
        {
            selectedSkill = skill;
        }
        public void PlayerEndTurn()
        {
            if (combatState == CombatState.PlayerTurn)
            {
                HandleCombatState(CombatState.EnemyTurn);
            }
        
            Debug.Log("End Turn");
        }//OnClick
    
        //--EnemyTurn--
        private void HandleEnemyTurn()
        {
            UpdateCombatState(CombatState.EnemyTurn);
                
            for (int i = 0; i < EMONSTERLISTLENGTH; i++)
            {
                Debug.Log("EMonster" + i + " is attacking");
            }

            StartCoroutine(ChangeCombatState(CombatState.End));
        }
    
        //--CombatEndTurn--
        private void HandleCombatEnd()
        {
            CombatEndCheck();
            //ChangeScene("MainMap 1");
        }
        private void CombatEndCheck()
        {
            //Check if player is death
            if (pMonster.mCurrentHealth <= 0)
            {
                Debug.Log("Player Die");
                UpdateCombatState(CombatState.End);
                return;
            }
            
            //Check if all monsters are death
            if (CheckIsAllMonsterDie())
            {
                Debug.Log("All Monster are Death");
                UpdateCombatState(CombatState.End);
                return;
            }

            UpdateCombatState(CombatState.PlayerTurn);
        }

        private bool CheckIsAllMonsterDie()
        {
            for (int i = 0; i < EMONSTERLISTLENGTH; i++)
            {
                if (eMonsters[i] == null)
                {
                    deathMonstCounter++;
                }
            }

            if (deathMonstCounter == 3)
            {
                return true;
            }
            else
            {
                Debug.Log("There still monsters left");
                deathMonstCounter = 0;
                return false;
            }
        }
    
        //--HUD--
        void SetupHUD()
        {
            ChangeStateHUD(CombatState.Start);
        }
        void ChangeStateHUD(CombatState state)
        {
            combatStateText.SetText("CombatState: " + state);
        }
    
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

    
    }
}