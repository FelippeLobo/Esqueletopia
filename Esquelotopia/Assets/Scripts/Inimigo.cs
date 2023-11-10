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
    public int ataque;
    public int defesa;
    public int moedas;
    public int xp;


      public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(vidaTotal, vidaAtual);
    }

    // Update is called once per frame
    void Update()
    {
        if(nome == "Bandido" || nome=="Soldado Exilado" || nome == "Héroi Renegado"){
            transform.localPosition = new Vector3(0, 0, 0);
        }else if(nome == "Saqueador"){
            transform.localPosition = new Vector3(0, 0.5f, 0);
        }else if(nome == "Slime"){
            transform.localPosition = new Vector3(0, 0, 0);
        }else{
             transform.localPosition = new Vector3(0, 0.8f, 0);
        }
    }
    public void LevelUpMonster(int lvl){
        for (var i = 0; i < lvl; i++)
        {   
            level++;
            vidaTotal+=10*level;
            vidaAtual= vidaTotal;
            ataque+=10*level;
            defesa+=10*level;
            moedas+=150*level;
            xp+=15;
        }

    }
    public float TakeDmg(float dano){
        
        float danoMitigado =  (float)(Math.Ceiling((UnityEngine.Random.Range(0, (0.01f*level) * defesa/2))));
        float danoFinal = dano-danoMitigado;
        if(danoFinal < 0){
            danoFinal = 0;
        }
        vidaAtual -= (danoFinal);
        
        healthBar.SetHealth(vidaAtual);
  
        return danoFinal;
        
    }

     public float InflictDmg(){

                int probAcerto = UnityEngine.Random.Range(0, 100);   

            if(probAcerto <= 5){
                    return 0;

            }else{
                float dano =  (float)((ataque*(0.10f*level)) + 10);
                return (float)(Math.Ceiling((UnityEngine.Random.Range(dano-(dano*0.20f), dano+(dano*0.1f)))));
            }

        

    }
}
