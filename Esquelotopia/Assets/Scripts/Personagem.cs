using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using System;


public class Personagem : MonoBehaviour 
{   
    public string nome;

    public static GameObject instance = null;
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

        DontDestroyOnLoad(this.gameObject);
        if(instance == null){
            StartWithoutInstance();
            instance = this.gameObject;

        }else {
            StartInstance();
            instance = this.gameObject;
        }


    }

    public void StartInstance(){
        Personagem personagem = instance.GetComponent<Personagem>();
        nome = personagem.nome;
        level = personagem.level;
        vidaTotal = personagem.vidaTotal;
        vidaAtual = personagem.vidaAtual;
        if(!(healthBar is null)){
            healthBar.SetMaxHealth(vidaTotal, vidaAtual);
        }
        ataque = personagem.ataque;
        defesa = personagem.defesa;
        magia = personagem.magia;
        moedas = personagem.moedas;
        //itensInventarios = personagem.itensInventarios;
        
        //itensEquipados = personagem.itensEquipados;

        itensInventarios = new Item[25];
        itensEquipados = new Item[4];

        for (int i = 0; i < 25; i++)
        {   
            itensInventarios[i] = personagem.itensInventarios[i];                
        }

        for (var i = 0; i < 4; i++)
        {
             itensEquipados[i] = personagem.itensEquipados[i];
        }
    }

    private void StartWithoutInstance(){
        
        level = 1;
        vidaTotal = 50;
        vidaAtual = vidaTotal;
        if(!(healthBar is null)){
            healthBar.SetMaxHealth(vidaTotal, vidaAtual);
        }
        ataque = 20;
        defesa = 10;
        magia = 0;
        moedas = 0;
        itensInventarios = new Item[25];
        itensEquipados = new Item[4];

        for (var i = 0; i < 25; i++){
            itensInventarios[i] = new Item(0, "", 0, 0, 0, 0, null);
        }

        for (var i = 0; i < 4; i++){
            itensEquipados[i] = new Item(0, "", 0, 0, 0, 0, null);
        }

        itensManager = new ItensManager();
        ItemFactory();
        GuardarMoedas(250);
    
    }

    // Update is called once per frame
    void Update()
    {
        if(!(healthBar is null)){
              healthBar.SetMaxHealth(vidaTotal, vidaAtual);
        }
        if(proxLevelXp >= 100){
            proxLevelXp = 0;
            LevelUp(); 
        }
      
    }

    public void EquipItens(Item[] itensEquipadosAux){
        for (var i = 0; i < 4; i++)
        {
                 if(!(itensEquipados[i] == null) && !(itensEquipados[i].id == 0)){
                    vidaTotal-= itensEquipados[i].vida;
                    ataque-= itensEquipados[i].ataque;
                    defesa-= itensEquipados[i].defesa;
                    magia-= itensEquipados[i].magia;
                 }

                itensEquipados[i] = itensEquipadosAux[i];
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
            
            if(!(itensEquipados[i] == null) && !(itensEquipados[i].id == 0)){
                    vidaTotal+= itensEquipados[i].vida;
                    ataque+= itensEquipados[i].ataque;
                    defesa+= itensEquipados[i].defesa;
                    magia+= itensEquipados[i].magia;
            }
        }
    }

 

    public void ItemFactory(){
        
        Item[] itemAux = itensManager.CreateStartListaItem(2);   
   
        for (int i = 0; i < itemAux.Length; i++)
        {
            itensInventarios[i] = itemAux[i];  
            UnityEngine.Debug.Log("Nome:"+itensInventarios[i].nome);
            UnityEngine.Debug.Log("Ataque:"+itensInventarios[i].ataque);
            UnityEngine.Debug.Log("Defesa:"+itensInventarios[i].defesa);
            UnityEngine.Debug.Log("Magia:"+itensInventarios[i].magia);
                 
        }

           
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
