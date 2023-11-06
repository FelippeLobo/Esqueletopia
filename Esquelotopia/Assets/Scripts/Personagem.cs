using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using System;


public class Personagem : MonoBehaviour 
{   
    public string nome;

    public Item[] itensInventarios;
    public Item[] itensEquipados;

    public int level;
    public int proxLevelXp;
    public int moedas;
    public float vidaTotal;
    public float vidaAtual;
    public int ataque;
    public int defesa;
    public int magia;
    
    public HealthBar healthBar;

    public ItensManager itensManager;

    
     void Start()
    {
        nome = "Esqueleto";
        level = 1;
        vidaTotal = 50;
        vidaAtual = vidaTotal;
        healthBar.SetMaxHealth(vidaTotal, vidaAtual);
        moedas = 2000;
        ataque = 20;
        defesa = 10;
        magia = 0;
        moedas = 0;
        itensInventarios = new Item[25];
        itensEquipados = new Item[4];
        for (var i = 0; i < 25; i++)
        {
            itensInventarios[i] = new Item(0, "", 0, 0, 0, 0, null);
        }
        for (var i = 0; i < 4; i++)
        {
            itensEquipados[i] = new Item(0, "", 0, 0, 0, 0, null);
        }
        itensManager = new ItensManager();
        ItemFactory();
        GuardarMoedas(2500);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetMaxHealth(vidaTotal, vidaAtual);
        if(proxLevelXp >= 100){
            proxLevelXp = 0;
            LevelUp(); 
        }
    }

    public void EquipItens(Item[] itensEquipados){
        for (var i = 0; i < 4; i++)
        {
                if(this.itensEquipados[i] != null){
                    vidaTotal-= this.itensEquipados[i].vida;
                    ataque-= this.itensEquipados[i].ataque;
                    defesa-= this.itensEquipados[i].defesa;
                    magia-= this.itensEquipados[i].magia;
                }
            
                this.itensEquipados[i] = itensEquipados[i];
        }
        AtualizaStatus();
    }

    public void HealLife(){
        proxLevelXp+=15;
        float cura = (float)Math.Ceiling(vidaTotal * 0.35f);
        float vidaFinal = vidaAtual + cura;

        if(vidaFinal < vidaTotal){
            vidaAtual = vidaFinal; 
        }else{
            vidaAtual = vidaTotal;
        }
    }
    public void LevelUp(){
        level++;
        vidaTotal+=10*level;
        ataque+=5*level;
        defesa+=5*level;
    }
    public void Aprimorar(){
        UpdatePanel.atualizar = true;
        proxLevelXp+=15;
        vidaTotal+=10;
        vidaAtual+=10;
        ataque+=5;
        defesa+=5;
    }
    public void GuardarMoedas(int valor){
        moedas+= valor;
    }

    public void ArmazenarItens(Item[] itens){

        for (var i = 0; i < itens.Length; i++)
        {
             for (var j = 0; j < 25; j++)
            {
                if(itensInventarios[j].id == 0){
                    itensInventarios[j] = itens[i];
                }
            }
        }
    }

    public void ArmazenarItem(Item item){

        int newTam = (itensInventarios.Length)+1;
        Item[] newInventory = new Item[newTam];

        for (var j = 0; j < newTam; j++)
        {
            if( j >= itensInventarios.Length ){
                newInventory[j] = item;
            }else{
                newInventory[j] = itensInventarios[j];
            }
        } 

        itensInventarios = newInventory;
    }

    public void AtualizaStatus(){
        for (var i = 0; i < 4; i++)
        {
            vidaTotal+= itensEquipados[i].vida;
            ataque+= itensEquipados[i].ataque;
            defesa+= itensEquipados[i].defesa;
            magia+= itensEquipados[i].magia;
        }
    }

 

    public void ItemFactory(){
        
        itensInventarios = itensManager.CreateStartListaItem(2);       

    }

    public bool TakeDmg(float dano){
        
        proxLevelXp+=1;
        vidaAtual -= dano;
        healthBar.SetHealth(vidaAtual);
        if(vidaAtual <= 0)
            return true;
        else
            return false;
        
    }

    public float InflictDmg(){
        
        proxLevelXp+=1;                                                
        return (float)(Math.Ceiling((UnityEngine.Random.Range(0, (float)(((ataque*(0.2*level)) + 5)+magia)))));
    }                      

}
