using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Equipamentos : MonoBehaviour
{
    public Item[] item;
    public GameObject equipamento;
    public GameObject blockText;

    public float vidaAtualStat;
    public float vidaTotalStat;
    public int ataqueStat;
    public int defesaStat;
    public int magiaStat;

    public HealthBar barraVida;
    public HealthBar barraXp;

    public Personagem personagem;

    // Start is called before the first frame update
    void Start()
    {  
        vidaAtualStat = Personagem.vidaAtual;
        vidaTotalStat = Personagem.vidaTotal;
        ataqueStat = Personagem.ataque;
        defesaStat = Personagem.defesa;
        magiaStat = Personagem.magia;



        item = new Item[4];
        for (var i = 0; i < 4; i++)
        {
            item[i] = Personagem.itensEquipados[i];
        }

        equipamento = this.gameObject;

        for (int i = 0; i < 4; i++)
        {
            GameObject slot = equipamento.transform.GetChild(i).gameObject;
            Item itemAux = slot.transform.GetChild(0).gameObject.GetComponent<Item>();            
           
            if(!(item[i] is null)){
                itemAux.id = item[i].id;
                itemAux.nome = item[i].nome;
                itemAux.vida = item[i].vida;
                itemAux.ataque = item[i].ataque;
                itemAux.defesa = item[i].defesa;
                itemAux.magia = item[i].magia;
                itemAux.imagem = item[i].imagem;  

                itemAux.LoadItem();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        equipamento = GameObject.Find("Equipamento");     
        UpdateStatus();
        
    }

    public void UpdateStatus(){
        bool flag = false;
        int cont = 0;
        for (int i = 0; i < 4; i++)
        {
            GameObject slot = equipamento.transform.GetChild(i).gameObject;
            
            Item itemAux = slot.transform.GetChild(0).gameObject.GetComponent<Item>();
            if (itemAux is null){
                itemAux = slot.transform.GetChild(1).gameObject.GetComponent<Item>();
            }     
            
            for (int j = 0; j < 25; j++)
               {        
                    

                        
                        if(Personagem.itensInventarios[j].id == itemAux.id){
                            Personagem.itensInventarios[j] = new Item(0, "", 0, 0, 0, 0, null);
                            j=26;
                            break;
                        }
                    

                   
                }
              
            
            if(item[i].id != itemAux.id){
                  item[i] = itemAux;
            }else{
                cont++;
            }
          
            

        }

        UnityEngine.Debug.Log("CONT:"+cont);


       
        Personagem.EquipItens(item);

        vidaAtualStat = Personagem.vidaAtual;
        vidaTotalStat = Personagem.vidaTotal;
        ataqueStat = Personagem.ataque;
        defesaStat = Personagem.defesa;
        magiaStat = Personagem.magia;
        
        barraVida.SetMaxHealth(vidaTotalStat, vidaAtualStat);
        barraXp.SetMaxHealth(100, Personagem.proxLevelXp);
        barraXp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Lvl "+Personagem.level;
        blockText.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "- Ataque: "+ ataqueStat;
        blockText.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "- Defesa: "+ defesaStat;
        blockText.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "- Magia: "+ magiaStat;
        blockText.transform.GetChild(5).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Personagem.moedas+"";
    }


}
