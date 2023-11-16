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
    public HealthBar playerHUD;
    public HealthBar playerXpHUD;

    public HealthBar playerManaHUD;
    private static float timeActionsDialogue = 1.3f;
    public static Personagem playerUnit;
    public Animator playerAnimator;
    public Animator enemyAnimator;

    public GameObject raio1;
    public GameObject raio2;
    public GameObject raio3;

    private Inimigo enemyUnit1;
    private Inimigo enemyUnit2;
    private Inimigo enemyUnit3;

    public InimigoHUD inimigoHUD1;

    public InimigoHUD inimigoHUD2;
    public InimigoHUD inimigoHUD3;

    public GameObject inimigoPos1;

    public GameObject inimigoPos2;
    public GameObject inimigoPos3;

   public GameObject odinBag;
    public GameObject odinBattle;
    
    public TextMeshProUGUI dialogueText1;
    public TextMeshProUGUI dialogueText2;
    public TextMeshProUGUI dialogueText3;

    public GameObject Actions;
    public GameObject EnemySelection;
    public GameObject Magias;
    public GameObject flames;

    public GameObject healEffect;
  
    private InimigoHUD inimigoHUD;

    public GameObject canvas;
    public GameObject vitoria;
    private int contDeath;
    public int[] enemyDeaths;

    private List<GameObject> inimigos;
    public GameObject backgrounds;

    public bool isBoss;
    void Start()
    {   

        playerUnit = GameObject.Find("PersonagemStats").GetComponent<Personagem>(); 
        playerUnit.healthBar = playerHUD;

        playerXpHUD.SetMaxHealth(100, Personagem.proxLevelXp);
        playerManaHUD.SetMaxHealth(100, 0);
        int seedBackGround = (int)UnityEngine.Random.Range(1, 6);
        
        backgrounds.transform.GetChild(seedBackGround).gameObject.SetActive(true);
        
        SetEnemies();
        
        enemyDeaths = new int[3];
        enemyDeaths[0] = 1;
        enemyDeaths[1] = 1;
        enemyDeaths[2] = 1;
        contDeath = 0;

        if(isBoss){
            contDeath = 2;
            enemyDeaths[0] = 0;
            enemyDeaths[2] = 0;

        }
        playerAnimator =  GameObject.Find("Player").GetComponent<Animator>();
        estado =  Estados.INICIO;

        StartCoroutine(SetupBatalha());
    }
    void Update(){

        if(Personagem.vidaAtual <= 0){
            playerAnimator.SetBool("isDeath", true);
            estado = Estados.PERDEU;
            EndBattle();
        }
        playerXpHUD.SetHealth(Personagem.proxLevelXp);
        playerXpHUD.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Lvl "+Personagem.level;

        playerManaHUD.SetHealth(Personagem.manaPoints);

        if(Personagem.berserkTurns > 0){
            flames.SetActive(true);
        }
    }
    void SetEnemies(){
        if(!isBoss){
        inimigoPos1 = GameObject.Find("EnemyPos");
        inimigoPos2 = GameObject.Find("EnemyPos2");
        inimigoPos3 = GameObject.Find("EnemyPos3");

        int[] inimigosSeed = new int[3];
        UnityEngine.Debug.Log("LVL:"+Personagem.level);
        if(Personagem.level < 3){
            for (int i = 0; i < 3; i++)
            {
                inimigosSeed[i] =  (int)UnityEngine.Random.Range(1, 5);
            }
               
        }

        if(Personagem.level >= 3 && Personagem.level <= 6){
            for (int i = 0; i < 3; i++)
                {
                    inimigosSeed[i] =  (int)UnityEngine.Random.Range(1, 8);
                }

        }

        if(Personagem.level > 6 && Personagem.level <= 8){
            for (int i = 0; i < 3; i++)
                {
                    inimigosSeed[i] =  (int)UnityEngine.Random.Range(5, 10);
                }

        }

        if(Personagem.level > 8){
            for (int i = 0; i < 3; i++)
                {
                    inimigosSeed[i] =  (int)UnityEngine.Random.Range(8, 12);
                }

        }

        GameObject enemy1 =Instantiate(Resources.Load("Inimigo"+inimigosSeed[0], typeof(GameObject))) as GameObject;
        GameObject enemy2 =Instantiate(Resources.Load("Inimigo"+inimigosSeed[1], typeof(GameObject))) as GameObject;
        GameObject enemy3 =Instantiate(Resources.Load("Inimigo"+inimigosSeed[2], typeof(GameObject))) as GameObject;

        enemy1.transform.position = inimigoPos1.transform.position;
        enemy2.transform.position = inimigoPos2.transform.position;
        enemy3.transform.position = inimigoPos3.transform.position;

        enemy1.transform.position -= new Vector3(0, -50, 0);
        enemy2.transform.position -= new Vector3(0, -50, 0);
        enemy3.transform.position -= new Vector3(0, -50, 0);

        enemy1.transform.parent = inimigoPos1.transform;
        enemy2.transform.parent = inimigoPos2.transform;
        enemy3.transform.parent = inimigoPos3.transform;

        
        
        // enemy1.transform.localScale += new Vector3(2.5f, 2.5f, 0);
        // enemy2.transform.localScale += new Vector3(2.5f, 2.5f, 0);
        // enemy3.transform.localScale += new Vector3(2.5f, 2.5f, 0);

        enemyUnit1 = enemy1.GetComponent<Inimigo>();
        enemyUnit2 = enemy2.GetComponent<Inimigo>();
        enemyUnit3 = enemy3.GetComponent<Inimigo>();

        if(Personagem.level > 2){
            enemyUnit1.LevelUpMonster((int)UnityEngine.Random.Range(Personagem.level-1, Personagem.level+1));
            enemyUnit2.LevelUpMonster((int)UnityEngine.Random.Range(Personagem.level-1, Personagem.level+1));
            enemyUnit3.LevelUpMonster((int)UnityEngine.Random.Range(Personagem.level-1, Personagem.level+1));
        }
       
        // enemyUnit1.LevelUpMonster(10);
        // enemyUnit2.LevelUpMonster(10);
        // enemyUnit3.LevelUpMonster(10);

        enemyUnit1.healthBar = inimigoHUD1.transform.GetChild(0).gameObject.GetComponent<HealthBar>();
        enemyUnit2.healthBar = inimigoHUD2.transform.GetChild(0).gameObject.GetComponent<HealthBar>();
        enemyUnit3.healthBar = inimigoHUD3.transform.GetChild(0).gameObject.GetComponent<HealthBar>();

        }else{
            inimigoPos2 = GameObject.Find("EnemyPos2");
            enemyUnit2 = inimigoPos2.transform.GetChild(3).GetComponent<Inimigo>();
            enemyUnit2.LevelUpMonster(14);
            enemyUnit2.healthBar = inimigoHUD2.transform.GetChild(0).gameObject.GetComponent<HealthBar>();
        }

    }
    void UpdateFromDataStore(){
        /*
        SistemaBatalha sb = instance.GetComponent<SistemaBatalha>();
        float vda = sb.Personagem.vidaAtual;
        Debug.Log(vda);
        Personagem = sb.Personagem;
        enemyUnit1 = sb.enemyUnit1;
        enemyUnit2 = sb.enemyUnit2;
        enemyUnit3 = sb.enemyUnit3;
        enemyDeaths = sb.enemyDeaths;
        contDeath = sb.contDeath;
        playerAnimator = Personagem.GetComponent<Animator>();
        estado =  sb.estado;
        */
    }
    IEnumerator SetupBatalha(){
        //GameObject playerGo = Instantiate(playerPrefab, playerPos);
        //Personagem = playerGo.GetComponent<Personagem>();

       //GameObject enemyGo = Instantiate(enemyPrefab, enemyPos);
       //enemyUnit = enemyGo.GetComponent<Inimigo>();
        if(estado == Estados.INICIO){
            if(!isBoss){
            UpdateDialogueText("Inimigos entraram em combate!");
            }else{
            UpdateDialogueText("O boss "+ enemyUnit2.nome +" entrou em combate!");     
            }
          

            estado = Estados.TURNO_JOGADOR;
        }
        if(!isBoss){
            inimigoHUD1.SetHUD(enemyUnit1);
            inimigoHUD2.SetHUD(enemyUnit2);
            inimigoHUD3.SetHUD(enemyUnit3);
        }else{
            inimigoHUD2.SetHUD(enemyUnit2);
        }
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

    public void OnAttackButton(GameObject panelEnemy){
        for (var i = 0; i < 3; i++)
        {
            if(enemyDeaths[i] == 0){
                panelEnemy.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }else{
                panelEnemy.transform.GetChild(i).GetComponent<Button>().interactable = true;
            }
            
        }
        panelEnemy.SetActive(true);
    }
    public void OnCurarButton(){
        if(Personagem.manaPoints >= 25){
            float cura = Personagem.HealLifeMagic();
            DmgText dmgText =  GameObject.Find("Player").transform.GetChild(1).gameObject.GetComponent<DmgText>();
            if(dmgText != null){
                dmgText.x = -0.3f;
                dmgText.y = 0;
                dmgText.UpdateDMG(cura);
                dmgText.ResetFadeOut();
            }
            UpdateDialogueText("Seu Necromante te curou em: +"+cura);
            estado = Estados.TURNO_INIMIGO;

            healEffect.SetActive(true);
            StartCoroutine(EnemyTurn());

        }else{
            UpdateDialogueText("Energia Insuficiente!");
            Magias.SetActive(false);
            Actions.SetActive(true);
        }
    }
    public void OnTempestadeAstralButtonAux(){
        StartCoroutine(OnTempestadeAstralButton());
    }
    IEnumerator OnTempestadeAstralButton(){
         if(Personagem.manaPoints >= 50){
            Personagem.manaPoints-=50;
            float danoArea = Personagem.TempestadeAstralMagic();
            UpdateDialogueText("A Tempestade Astral de seu Necromante gerou "+danoArea+ " de dano!");
            UpdateDialogueText("Veremos se seus inimigos conseguirão se defender!");

            int m1 = 0;
            int m2 = 0;
            int m3 = 0;

            int d100Inimigo1 = (int)UnityEngine.Random.Range(0, 100);
            int d100Inimigo2 = (int)UnityEngine.Random.Range(0, 100);
            int d100Inimigo3 = (int)UnityEngine.Random.Range(0, 100);

            DmgText dmgText1 =  GameObject.Find("EnemyPos").transform.GetChild(1).gameObject.GetComponent<DmgText>();
            DmgText dmgText2 =  GameObject.Find("EnemyPos2").transform.GetChild(1).gameObject.GetComponent<DmgText>();
            DmgText dmgText3 =  GameObject.Find("EnemyPos3").transform.GetChild(1).gameObject.GetComponent<DmgText>();

            Animator enemyAnimator1 = enemyUnit1.GetComponent<Animator>();
            Animator enemyAnimator2 = enemyUnit2.GetComponent<Animator>();
            Animator enemyAnimator3 = enemyUnit3.GetComponent<Animator>();

            //Bloco de Animação
            //Start Animação da Skill
            
            yield return new WaitForSeconds(timeActionsDialogue);

            if(d100Inimigo1 <= 75 && isBoss == false){
                raio1.SetActive(true);
                enemyAnimator1.SetBool("isTakeHit", true);
            }
            if(d100Inimigo2 <= 75){
                raio2.SetActive(true);
                enemyAnimator2.SetBool("isTakeHit", true);
            }
            if(d100Inimigo3 <= 75 && isBoss == false){
                raio3.SetActive(true);
                enemyAnimator3.SetBool("isTakeHit", true);
            }
            

            yield return new WaitForSeconds(timeActionsDialogue);
            float dmgFinal1;
            float dmgFinal2;
            float dmgFinal3;

            bool isMorto1;
            bool isMorto2;
            bool isMorto3;

            //End Animação da Skill
            if(d100Inimigo1 <= 75 && isBoss == false){
                enemyAnimator1.SetBool("isTakeHit", false);
                dmgFinal1 = enemyUnit1.TakeDmg(danoArea);

                dmgText1.x = 0.5f;
                dmgText1.y = 0.5f;
                dmgText1.UpdateDMG(-dmgFinal1);
                dmgText1.ResetFadeOut();

                isMorto1 = (enemyUnit1.vidaAtual <= 0f);
                inimigoHUD1.SetHP(enemyUnit1.vidaAtual);

                UpdateDialogueText("A Tempestade Astral gerou "+dmgFinal1+ " de dano no "+enemyUnit1.nome);

                if(isMorto1){
                    contDeath++;
                    m1++;
                    enemyAnimator1.SetBool("isDeath", true);
                    enemyDeaths[0] = 0;
                }
            }
            if(d100Inimigo2 <= 75){
                enemyAnimator2.SetBool("isTakeHit", false);
                dmgFinal2 = enemyUnit2.TakeDmg(danoArea);

                dmgText2.x = 0.5f;
                dmgText2.y = 0.5f;
                dmgText2.UpdateDMG(-dmgFinal2);
                dmgText2.ResetFadeOut();

                isMorto2 = (enemyUnit2.vidaAtual <= 0f);
                inimigoHUD2.SetHP(enemyUnit2.vidaAtual);
                UpdateDialogueText("A Tempestade Astral gerou "+dmgFinal2+ " de dano no "+enemyUnit2.nome);

                if(isMorto2){
                    contDeath++;
                    m2++;
                    enemyAnimator2.SetBool("isDeath", true);
                    enemyDeaths[1] = 0;
                }
            }
            if(d100Inimigo3 <= 75  && isBoss == false){
                enemyAnimator3.SetBool("isTakeHit", false);
                dmgFinal3 = enemyUnit3.TakeDmg(danoArea);

                dmgText3.x = 0.5f;
                dmgText3.y = 0.5f;
                dmgText3.UpdateDMG(-dmgFinal3);
                dmgText3.ResetFadeOut();

                isMorto3 = (enemyUnit3.vidaAtual <= 0f);
                inimigoHUD3.SetHP(enemyUnit3.vidaAtual);
                UpdateDialogueText("A Tempestade Astral gerou "+dmgFinal3+ " de dano no "+enemyUnit3.nome);

                if(isMorto3){
                    contDeath++;
                    m3++;
                    enemyAnimator3.SetBool("isDeath", true);
                    enemyDeaths[2] = 0;
                }
            }
            
            yield return new WaitForSeconds(timeActionsDialogue);

            if(contDeath == 3 || ((m1+m2+m3) == 3 && isBoss == false) || ((m2) == 1 && isBoss == true) ){
                estado = Estados.GANHOU;
                EndBattle();
            }else{
                estado = Estados.TURNO_INIMIGO;
                StartCoroutine(EnemyTurn());
            }
        
         }else{
            UpdateDialogueText("Energia Insuficiente!");
            Magias.SetActive(false);
            Actions.SetActive(true);
         }


    }

    public void OnBerserkButton(){

        if(Personagem.manaPoints >= 80){
            Personagem.manaPoints-= 80;
            Personagem.BerserkerBuffMagic();
            UpdateDialogueText("Seu Necromante invocou a Fúria do Berserker Caído!");
            estado = Estados.TURNO_INIMIGO;

            healEffect.SetActive(true);
            StartCoroutine(EnemyTurn());

        }else{
            UpdateDialogueText("Energia Insuficiente!");
            Magias.SetActive(false);
            Actions.SetActive(true);

        }
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
    public void OnFugir(GameObject buttonFugir){
        StartCoroutine(Fugir(buttonFugir));
    }
    IEnumerator Fugir(GameObject buttonFugir){
        float vidaAux = (int)(Personagem.vidaTotal * 0.25f);
        Personagem.vidaAtual -= vidaAux;
        if(Personagem.vidaAtual > 0){
            Personagem.moedas -= (int)(Personagem.moedas * 0.1f );
            buttonFugir.GetComponent<ButtonUI>().StartGameButton();
        }else{
            playerAnimator.SetBool("isDeath", true);
            yield return new WaitForSeconds(timeActionsDialogue);
            estado = Estados.PERDEU;
            EndBattle();
        }

        
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
                dmgText = GameObject.Find("EnemyPos").transform.GetChild(1).gameObject.GetComponent<DmgText>();
                break;
            case 1:
                enemyUnit = enemyUnit2;
                inimigoHUD = inimigoHUD2;
                dmgText = GameObject.Find("EnemyPos2").transform.GetChild(1).gameObject.GetComponent<DmgText>();
                break;
            case 2:
                enemyUnit = enemyUnit3;
                inimigoHUD = inimigoHUD3;
                dmgText = GameObject.Find("EnemyPos3").transform.GetChild(1).gameObject.GetComponent<DmgText>();
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

        float dmg = Personagem.InflictDmg();
        float dmgFinal = enemyUnit.TakeDmg(dmg);
        if(dmgText != null){
            dmgText.x = 0.5f;
            dmgText.y = 0.5f;
            dmgText.UpdateDMG(-dmgFinal);
            dmgText.ResetFadeOut();
        }
        
        bool isMorto = (enemyUnit.vidaAtual <= 0f);
        inimigoHUD.SetHP(enemyUnit.vidaAtual);

        
       
        
        UpdateDialogueText("Seu ataque causou "+dmgFinal+ " de dano!");


        yield return new WaitForSeconds(timeActionsDialogue);

        if(isMorto){
            contDeath++;
            enemyAnimator.SetBool("isDeath", true);
            enemyDeaths[enemyID] = 0;
        }

        UnityEngine.Debug.Log("ContDeath:"+contDeath);
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
        yield return new WaitForSeconds(0.3f);
        UpdateDialogueText("Turno dos inimigos, se proteja!");
       
      
        if(healEffect.active == true){
             healEffect.SetActive(false);
        }
        if(raio1.active == true){
            raio1.SetActive(false);
        }
        if(raio2.active == true){
            raio2.SetActive(false);
        }
        if(raio3.active == true){
            raio3.SetActive(false);
        }

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
                float dmgFinal = Personagem.TakeDmg(dmg);
                 if(dmgText != null){
                    dmgText.x = -0.3f;
                    dmgText.y = 0;
                    dmgText.UpdateDMG(-dmgFinal);
                    dmgText.ResetFadeOut();
                }
                
                isMorto = (Personagem.vidaAtual <= 0f);
                UpdateDialogueText("O ataque inimigo lhe causou "+dmgFinal+ " de dano!");
                
                yield return new WaitForSeconds(timeActionsDialogue);
            }
            
        }
        Magias.SetActive(false);
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
    public void EnemyTakeHit(Animator enemyAnimator, Inimigo enemy, float dmg){


    }
    void EndBattle(){
      
        if(estado ==  Estados.GANHOU){
            UpdateDialogueText("Você venceu a batalha!");
            int moedasGanhas = enemyUnit1.moedas+enemyUnit1.moedas+enemyUnit1.moedas;
            int xpGanho = enemyUnit1.xp+enemyUnit1.xp+enemyUnit1.xp;
            Personagem.GuardarMoedas(moedasGanhas);
            Personagem.GanharXP(xpGanho);
            playerXpHUD.SetHealth(Personagem.proxLevelXp);
            UpdateDialogueText("Você ganhou $"+moedasGanhas+" moedas e "+xpGanho+"xp");

            Personagem.berserkTurns = 0;

            vitoria.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Moedas encontradas:" +moedasGanhas;
            vitoria.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Xp ganho:" +xpGanho;
            vitoria.SetActive(true);
            
            //SceneManager.LoadScene("Mapa");

        }else if (estado == Estados.PERDEU){
            UpdateDialogueText("Sua invocação foi derrotada!");

            odinBag.SetActive(false);
            odinBattle.SetActive(false);
            canvas.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void ChangeSceneToBag(){
        odinBag.SetActive(true);
        odinBattle.SetActive(false);
        
    }
}
