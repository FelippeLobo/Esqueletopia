using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using System;

public class Inimigo : MonoBehaviour
{   

    public string nome;
    public int level;
    public float vidaTotal;
    public float vidaAtual;
    public float ataque;
    public float defesa;
    public int moedas;
    public int xp;

    private float ataqueBase;
    private float defesaBase;
    private float vidaTotalBase;


      public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        ataqueBase = ataque;
        defesaBase = defesa;
        vidaTotalBase = vidaTotal;
        DuzentosAnosDeBalanceamentoRiotGames();
        healthBar.SetMaxHealth(vidaTotal, vidaAtual);
    }

    // Update is called once per frame
    void Update()
    {
        if(nome == "Bandido" || nome=="Soldado Exilado" || nome == "Héroi Renegado" || nome == "Demon Slime"){
            transform.localPosition = new Vector3(0, 0, 0);
        }else if(nome == "Saqueador"){
            transform.localPosition = new Vector3(0, 0.5f, 0);
        }else if(nome == "Slime"){
            transform.localPosition = new Vector3(0, 0, 0);
        }else{
             transform.localPosition = new Vector3(0, 0.8f, 0);
        }
 
    }
    public void DuzentosAnosDeBalanceamentoRiotGames()
    {
        for (var i = 0; i < 2; i++)
        {
            vidaTotal += (vidaTotalBase * 0.5f);
            vidaAtual = vidaTotal;
            ataque += (ataqueBase * 0.65f);
            defesa += (defesaBase * 0.60f);
        }
    }
    public void LevelUpMonster(int lvl){
        for (var i = 0; i < lvl; i++)
        {   
            level++;
            vidaTotal+= (vidaTotalBase * 0.5f);
            vidaAtual= vidaTotal;
            ataque+= (ataqueBase * 0.65f);
            defesa+=(defesaBase * 0.60f);

            moedas+=150;
            xp += 1;
        }
    }
    public float TakeDmg(float dano){

        float danoMitigadoBase = dano - ((defesa / 2) * (1 + (Personagem.level / 10)));
        float danoMitigado = (float)(Math.Ceiling((UnityEngine.Random.Range(danoMitigadoBase - (danoMitigadoBase * 0.1f), danoMitigadoBase + (danoMitigadoBase * 0.1f)))));

        if(danoMitigado < 0){
            danoMitigado = 0;
        }
        vidaAtual -= (danoMitigado);
        
        healthBar.SetHealth(vidaAtual);
  
        return danoMitigado;
        
    }

     public float InflictDmg(){

            int probAcerto = UnityEngine.Random.Range(0, 100);   

            if(probAcerto <= 5){
                    return 0;

            }else{
                float dano =  (ataque* 0.55f);
                return (float)(Math.Ceiling((UnityEngine.Random.Range(dano-(dano*0.1f), dano+(dano*0.1f)))));
            }

        

    }
}
