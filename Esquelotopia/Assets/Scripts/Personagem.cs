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
    public static Item[] itensInventarios;
    public static Item[] itensEquipados;

    public static int level;
    public static int proxLevelXp;
    public static int manaPoints;
    public static int moedas;
    public static float vidaTotal;
    public static float vidaAtual;
    public static int ataque;
    public static int defesa;
    
    private static int ataqueAux;
    private static int defesaAux;
    public static int magia;
    public static bool berserkStatus;
    public static int berserkTurns;
    
    public HealthBar healthBar;
    public static ItensManager itensManager;

    
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
        /*nome = Personagem.nome;
        level = Personagem.level;
        vidaTotal = Personagem.vidaTotal;
        vidaAtual = Personagem.vidaAtual;
        if(!(healthBar is null)){
            healthBar.SetMaxHealth(vidaTotal, vidaAtual);
        }
        ataque = Personagem.ataque;
        defesa = Personagem.defesa;
        magia = Personagem.magia;
        moedas = Personagem.moedas;
        //itensInventarios = personagem.itensInventarios;
        
        //itensEquipados = personagem.itensEquipados;

        itensInventarios = new Item[25];
        itensEquipados = new Item[4];

        for (int i = 0; i < 25; i++)
        {   
            itensInventarios[i] = Personagem.itensInventarios[i];                
        }

        for (var i = 0; i < 4; i++)
        {
             itensEquipados[i] = Personagem.itensEquipados[i];
        }*/
    }

    private void StartWithoutInstance(){
        
       
        nome = "Invocação"; 

        level = 1;
        vidaTotal = 100;
        vidaAtual = vidaTotal;
        ataque = 25;
        defesa = 15;
        magia = 5;

        moedas = 0;
        manaPoints = 0;

        ataqueAux = 0;
        defesaAux = 0;

        if (!(healthBar is null))
        {
            healthBar.SetMaxHealth(vidaTotal, vidaAtual);
        }

        itensInventarios = new Item[25];
        itensEquipados = new Item[4];
        berserkTurns = 0;
        for (var i = 0; i < 25; i++){
            itensInventarios[i] = new Item(0, "", 0, 0, 0, 0, null);
        }

        for (var i = 0; i < 4; i++){

            itensEquipados[i] = new Item(0, "", 0, 0, 0, 0, null);
        }
        // for (int i = 0; i < 4; i++)
        // {
        //     LevelUp();
        // }

        itensManager = new ItensManager();
        Personagem.ItemFactory();
        Personagem.GuardarMoedas(250);
    
    }

    // Update is called once per frame
    void Update()
    {
        if(!(healthBar is null)){
              healthBar.SetMaxHealth(vidaTotal, vidaAtual);
        }
        if(proxLevelXp >= 100){
            proxLevelXp = proxLevelXp - 100;
            LevelUp(); 
        }
        if(manaPoints >= 100){
            manaPoints = 100;
        }
        if(berserkTurns == 0 && ataqueAux > 0 && defesaAux > 0){
            ataque = ataqueAux;
            defesa = defesaAux;
            ataqueAux = 0;
            defesaAux = 0;
        }      
    }

    public static void EquipItens(Item[] itensEquipadosAux){
          for (int i = 0; i < 4; i++)
    {
        if (itensEquipados[i] != null && itensEquipados[i].id != 0)
        {
           
            vidaTotal -= itensEquipados[i].vida;
            ataque -= itensEquipados[i].ataque;
            defesa -= itensEquipados[i].defesa;
            magia -= itensEquipados[i].magia;
        }


        itensEquipados[i] = itensEquipadosAux[i];
    }


    List<int> idsDuplicados = new List<int>();


       for (int i = 0; i < 25; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            if (itensInventarios[i] != null && itensEquipados[j] != null && itensInventarios[i].id == itensEquipados[j].id)
            {
                idsDuplicados.Add(itensInventarios[i].id);
            }
        }
    }

    for (int i = 0; i < 25; i++)
    {
        if (itensInventarios[i] != null && idsDuplicados.Contains(itensInventarios[i].id))
        {
            itensInventarios[i] = null;
        }
    }

    
    AtualizaStatus();
    }

    
    public static float HealLifeMagic(){
        proxLevelXp+=3;
        manaPoints-=25;
        float maxCura = (float)Math.Ceiling(vidaTotal * 0.50f);
        float minCura = (float)Math.Ceiling(vidaTotal * 0.35f);
        float cura = (int)UnityEngine.Random.Range(minCura, maxCura);
        float vidaFinal = vidaAtual + cura;

        if(vidaFinal < vidaTotal){
            vidaAtual = vidaFinal; 
           
        }else{
            vidaAtual = vidaTotal;
        }

         return cura;
    }
    public static float TempestadeAstralMagic(){
        
        float scaling = UnityEngine.Random.Range(75, 150);
        float danoBase = ((ataque*0.5f) * (1 + level/10)) + (magia*1.5f);
        
        float danoFinal = danoBase * (scaling / 100);

        return (int)danoFinal;

    }

    public static void BerserkerBuffMagic(){

        berserkTurns = 3;
        ataqueAux = ataque;
        defesaAux = defesa;
        ataque += (int)(ataque*1.5f);
        defesa += (int)(defesa*1.5f);

    }


    public static void HealLife(){
        proxLevelXp+=15;
        float cura = (float)Math.Ceiling(vidaTotal * 0.5f);
        float vidaFinal = vidaAtual + cura;

        if(vidaFinal < vidaTotal){
            vidaAtual = vidaFinal; 
        }else{
            vidaAtual = vidaTotal;
        }
    }
    public static void LevelUp(){
        level++;
        vidaTotal+=10;
        vidaAtual= vidaTotal;
        ataque+=2;
        defesa+=1;
        magia += 1;
    }
    public static void Aprimorar(){
        UpdatePanel.atualizar = true;
        proxLevelXp+=15;
        vidaTotal+=10;
        vidaAtual+=10;
        ataque+=5;
        defesa+=5;
    }
    public static void GuardarMoedas(int valor){
        moedas+= valor;
    }

    public static void GanharXP(int valor){
        proxLevelXp+=valor;
    }

    public static void ArmazenarItens(Item[] itens){

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

    public static void ArmazenarItem(Item item){

        for (var j = 0; j < 25; j++)
        {
            if(itensInventarios[j].id == 0){
                itensInventarios[j] = item;
                j=30;
                break;

            }

        } 
    }

    public static void AtualizaStatus(){
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

    public static void ChestBonus(){
        moedas+= UnityEngine.Random.Range(100,10000);
        proxLevelXp+=  UnityEngine.Random.Range(10,100);
        Item[] itemAux =  itensManager.CreateListaItem(UnityEngine.Random.Range(1,3));
        int cont=0;
        for (int i = 0; i < 25; i++)
        {   
            if(cont == itemAux.Length){
                i=26;
                break;
            }
         
            if(itensInventarios[i].id == 0){
              
                itensInventarios[i] = itemAux[cont];  
                cont++;
            }        
        }
    }

    public static void ItemFactory(){
        
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

    public static float TakeDmg(float dano){
        
        proxLevelXp+=1;
        manaPoints+= (int)(2 + (level*0.05f));
        float danoMitigadoBase = dano - (((defesa*0.8f) + (magia*0.2f)) / (2 * (1 + (level / 10))));
        float danoMitigado = (float)(Math.Ceiling((UnityEngine.Random.Range(danoMitigadoBase - (danoMitigadoBase * 0.1f), danoMitigadoBase + (danoMitigadoBase * 0.1f)))));

        if(danoMitigado < 0){
            danoMitigado = 0;
        }

        if(berserkTurns > 0){
            vidaAtual -= (int)(danoMitigado/2);
            return danoMitigado/2;
        }else{
            vidaAtual -= (int)(danoMitigado);
            return danoMitigado;
        }
     
        
       //healthBar.SetHealth(vidaAtual);
  
       
        
    }

    public static float InflictDmg(){
        
        if(berserkTurns != 0){
            berserkTurns--;
        }
        proxLevelXp+=2;   
        manaPoints+= (int)((10 + (level*0.5f)));


        int probAcerto = UnityEngine.Random.Range(0, 100);   

            if(probAcerto <= 5){
                    return 0;

            }else if(probAcerto >= 95){ 

                float dano = (float)((((ataque*0.60f)+ (magia*0.25f))*(1 + level/10)));
                return dano*2;


            }else{
                float dano = (float)((ataque*0.60f)+ (magia*0.25f)*(1 + level/10));
                return (float)(Math.Ceiling((UnityEngine.Random.Range(dano - (dano * 0.1f), dano + (dano * 0.1f))))); ;
            }

        
    }                      

}
