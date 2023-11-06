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

    public Personagem personagem;

    // Start is called before the first frame update
    void Start()
    {  

        vidaAtualStat = personagem.vidaAtual;
        vidaTotalStat = personagem.vidaTotal;
        ataqueStat = personagem.ataque;
        defesaStat = personagem.defesa;
        magiaStat = personagem.magia;


        item = new Item[4];
          for (var i = 0; i < 4; i++)
        {
            item[i] = new Item(0, "", 0, 0, 0, 0, null);
        }
        equipamento = this.gameObject;

        for (int i = 0; i < 4; i++)
        {
            GameObject slot = equipamento.transform.GetChild(i).gameObject;
            Item itemAux = slot.transform.GetChild(0).gameObject.GetComponent<Item>();

            vidaAtualStat += itemAux.vida;
            vidaTotalStat += itemAux.vida;
            ataqueStat += itemAux.ataque;
            defesaStat += itemAux.defesa;
            magiaStat += itemAux.magia;
            

        }

    }

    // Update is called once per frame
    void Update()
    {
        equipamento = GameObject.Find("Equipamento");     
        UpdateStatus();
        
    }

    public void UpdateStatus(){
        
        for (int i = 0; i < 4; i++)
        {
            GameObject slot = equipamento.transform.GetChild(i).gameObject;
            
            Item itemAux = slot.transform.GetChild(0).gameObject.GetComponent<Item>();
            if (itemAux is null){
                itemAux = slot.transform.GetChild(1).gameObject.GetComponent<Item>();
            }     
            item[i] = itemAux;
            UnityEngine.Debug.Log(item.Length);
            
        }

        personagem.EquipItens(item);

        vidaAtualStat = personagem.vidaAtual;
        vidaTotalStat = personagem.vidaTotal;
        ataqueStat = personagem.ataque;
        defesaStat = personagem.defesa;
        magiaStat = personagem.magia;

        blockText.transform.GetChild(0).GetComponent<HealthBar>().SetMaxHealth(vidaTotalStat, vidaAtualStat);
        blockText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "- Ataque: "+ ataqueStat;
        blockText.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "- Defesa: "+ defesaStat;
        blockText.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "- Magia: "+ magiaStat;

    }


}
