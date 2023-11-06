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


      public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(vidaTotal, vidaAtual);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TakeDmg(float dano){
        
        vidaAtual -= dano;
        healthBar.SetHealth(vidaAtual);
        if(vidaAtual <= 0)
            return true;
        else
            return false;
        
    }

     public float InflictDmg(){


        return (float)(Math.Ceiling((UnityEngine.Random.Range(0, (float)((ataque*(0.15*level)) + 1)))));

    }
}
