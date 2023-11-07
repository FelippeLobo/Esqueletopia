using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using TMPro;


public enum Estados { INICIO, TURNO_JOGADOR, TURNO_INIMIGO, GANHOU, PERDEU }
public class SistemaBatalha : MonoBehaviour
{   
    public Estados estado;

    public Transform playerPos;
    public Transform enemyPos;

    private static float timeActionsDialogue = 1.3f;
    public static Personagem playerUnit;
    public Animator playerAnimator;
    public Animator enemyAnimator;
    private Inimigo enemyUnit1;
    private Inimigo enemyUnit2;
    private Inimigo enemyUnit3;

    public InimigoHUD inimigoHUD1;

    public InimigoHUD inimigoHUD2;
    public InimigoHUD inimigoHUD3;

   public GameObject odinBag;
    public GameObject odinBattle;
    
    public TextMeshProUGUI dialogueText1;
    public TextMeshProUGUI dialogueText2;
    public TextMeshProUGUI dialogueText3;

    public GameObject Actions;
    public GameObject EnemySelection;

  
    private InimigoHUD inimigoHUD;

    
    private int contDeath;
    public int[] enemyDeaths;

    void Start()
    {    
        playerUnit = GameObject.Find("Player").GetComponent<Personagem>();
        enemyUnit1 = GameObject.Find("EnemyPos").transform.GetChild(0).gameObject.GetComponent<Inimigo>();
        enemyUnit2 = GameObject.Find("EnemyPos2").transform.GetChild(0).gameObject.GetComponent<Inimigo>();
        enemyUnit3 = GameObject.Find("EnemyPos3").transform.GetChild(0).gameObject.GetComponent<Inimigo>();
        //playerUnit.ItemFactory();
        enemyDeaths = new int[3];
        enemyDeaths[0] = 1;
        enemyDeaths[1] = 1;
        enemyDeaths[2] = 1;
        contDeath = 0;
        playerAnimator = playerUnit.GetComponent<Animator>();
        estado =  Estados.INICIO;

        StartCoroutine(SetupBatalha());
    }
    void UpdateFromDataStore(){
        /*
        SistemaBatalha sb = instance.GetComponent<SistemaBatalha>();
        float vda = sb.playerUnit.vidaAtual;
        Debug.Log(vda);
        playerUnit = sb.playerUnit;
        enemyUnit1 = sb.enemyUnit1;
        enemyUnit2 = sb.enemyUnit2;
        enemyUnit3 = sb.enemyUnit3;
        enemyDeaths = sb.enemyDeaths;
        contDeath = sb.contDeath;
        playerAnimator = playerUnit.GetComponent<Animator>();
        estado =  sb.estado;
        */
    }
    IEnumerator SetupBatalha(){
        //GameObject playerGo = Instantiate(playerPrefab, playerPos);
        //playerUnit = playerGo.GetComponent<Personagem>();

       //GameObject enemyGo = Instantiate(enemyPrefab, enemyPos);
       //enemyUnit = enemyGo.GetComponent<Inimigo>();
        if(estado == Estados.INICIO){
            UpdateDialogueText("Inimigos entraram em combate!");
          

            estado = Estados.TURNO_JOGADOR;
        }
            inimigoHUD1.SetHUD(enemyUnit1);
            inimigoHUD2.SetHUD(enemyUnit2);
            inimigoHUD3.SetHUD(enemyUnit3);
            yield return new WaitForSeconds(timeActionsDialogue);
      
    
       PlayerTurn();
    }

    void PlayerTurn(){
        UpdateDialogueText("É sua vez, faça sua ação: ");
      
       
    }

    void UpdateDialogueText(string info){
        dialogueText3.text =  dialogueText2.text;
        dialogueText2.text =  dialogueText1.text;
        dialogueText1.text = info;
    }

    public void OnAttackButton(GameObject panelButton, GameObject panelEnemy){
        panelButton.SetActive(false);
        panelEnemy.SetActive(true);
    }
    public void OnEnemyButton(int enemyID){

        if(enemyDeaths[enemyID] == 1){
            if(estado != Estados.TURNO_JOGADOR)
            return;

            StartCoroutine(PlayerAttack(enemyID));
        }else{
            UpdateDialogueText("Esse inimigo já morreu!");
        }
      
    }

    public void Fugir(){

        float vidaAux = playerUnit.vidaTotal * 0.25f;
        playerUnit.vidaAtual -= vidaAux;
        
    }
    IEnumerator PlayerAttack(int enemyID){
        
        Inimigo enemyUnit;
        InimigoHUD inimigoHUD;
        DmgText dmgText = null;
        switch (enemyID)
        {
            case 0:
                enemyUnit = enemyUnit1;
                inimigoHUD = inimigoHUD1;
                dmgText = GameObject.Find("EnemyPos").transform.GetChild(2).gameObject.GetComponent<DmgText>();
                break;
            case 1:
                enemyUnit = enemyUnit2;
                inimigoHUD = inimigoHUD2;
                dmgText = GameObject.Find("EnemyPos2").transform.GetChild(2).gameObject.GetComponent<DmgText>();
                break;
            case 2:
                enemyUnit = enemyUnit3;
                inimigoHUD = inimigoHUD3;
                dmgText = GameObject.Find("EnemyPos3").transform.GetChild(2).gameObject.GetComponent<DmgText>();
                break;
            default:
                enemyUnit = null;
                inimigoHUD = null;
                break;
        }
        enemyAnimator = enemyUnit.GetComponent<Animator>();

        //Bloco de Animação
        playerAnimator.SetBool("isAttacking", true);
        enemyAnimator.SetBool("isTakeHit", true);
        yield return new WaitForSeconds(timeActionsDialogue);
        playerAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetBool("isTakeHit", false);
        float dmg = playerUnit.InflictDmg();
        if(dmgText != null){
            dmgText.x = 0.5f;
            dmgText.y = 0.5f;
            dmgText.UpdateDMG(dmg);
            dmgText.ResetFadeOut();
        }
         
        bool isMorto = enemyUnit.TakeDmg(dmg);
        inimigoHUD.SetHP(enemyUnit.vidaAtual);

        
       
        
        UpdateDialogueText("Seu ataque causou "+dmg+ " de dano!");


        yield return new WaitForSeconds(timeActionsDialogue);

        if(isMorto){
            contDeath++;
            enemyAnimator.SetBool("isDeath", true);
            enemyDeaths[enemyID] = 0;
        }
        
        if(contDeath == 3){
            
            estado = Estados.GANHOU;
            EndBattle();
        }else{
            estado = Estados.TURNO_INIMIGO;
            StartCoroutine(EnemyTurn());

        }

        
        
       
        
    }

    IEnumerator EnemyTurn(){
         
        Inimigo enemyUnit;
        bool isMorto = false;
        DmgText dmgText;
        UpdateDialogueText("Turno dos inimigos, se proteja!");
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < 3; i++)
        {
            dmgText =  GameObject.Find("Player").transform.GetChild(0).gameObject.GetComponent<DmgText>();
            if(enemyDeaths[i] == 1){
                    switch (i)
                {
                    case 0:
                        enemyUnit = enemyUnit1;
                        inimigoHUD = inimigoHUD1;
                        break;
                    case 1:
                        enemyUnit = enemyUnit2;
                        inimigoHUD = inimigoHUD2;
                        break;
                    case 2:
                        enemyUnit = enemyUnit3;
                        inimigoHUD = inimigoHUD3;
                        break;
                    default:
                        enemyUnit = null;
                        inimigoHUD = null;
                        break;
                }
                enemyAnimator = enemyUnit.GetComponent<Animator>();

                enemyAnimator.SetBool("isAttacking", true);
                playerAnimator.SetBool("isTakeHit", true);
                yield return new WaitForSeconds(timeActionsDialogue);
                enemyAnimator.SetBool("isAttacking", false);
                playerAnimator.SetBool("isTakeHit", false);
                float dmg = enemyUnit.InflictDmg();
                 if(dmgText != null){
                    dmgText.x = -0.3f;
                    dmgText.y = 0;
                    dmgText.UpdateDMG(dmg);
                    dmgText.ResetFadeOut();
                }
                isMorto = playerUnit.TakeDmg(dmg);
                UpdateDialogueText("O ataque inimigo lhe causou "+dmg+ " de dano!");
                
                yield return new WaitForSeconds(timeActionsDialogue);
            }
            
        }

        EnemySelection.SetActive(false);
        Actions.SetActive(true);

        if(isMorto){
            playerAnimator.SetBool("isDeath", true);
            estado = Estados.PERDEU;
            EndBattle();
        }else{
            estado = Estados.TURNO_JOGADOR;
            PlayerTurn();
        }

    }
 
    void EndBattle(){
        if(estado ==  Estados.GANHOU){
            UpdateDialogueText("Você venceu a batalha!");
        }else if (estado == Estados.PERDEU){
            UpdateDialogueText("Sua invocação foi derrotada!");
        }
    }

    public void ChangeSceneToBag(){
        odinBag.SetActive(true);
        odinBattle.SetActive(false);
        

        
    }
}
