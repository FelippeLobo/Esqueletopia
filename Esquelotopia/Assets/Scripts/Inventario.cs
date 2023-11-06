 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventario : MonoBehaviour
{   
    public Item[] itens;
    public GameObject mouseItem;
    public GameObject inventario;

    public Personagem personagem;
    public void Start(){
        
        if(personagem == null){
            //personagem = SistemaBatalha.playerUnit;
        }


        itens = new Item[25];

        for (int i = 0; i < personagem.itensInventarios.Length; i++)
        {
            itens[i] = personagem.itensInventarios[i];
            
        }


        LoadItem();

    }
    public void DragItem(GameObject button){
        mouseItem = button;
        mouseItem.transform.position = Input.mousePosition;
    }

    public void DropItem(GameObject button){
        
        if(mouseItem != null){

            Transform aux = mouseItem.transform.parent;
            mouseItem.transform.SetParent(button.transform.parent);
            button.transform.SetParent(aux);
            
        }
       
    }

    public void LoadItem(){
        int[] cont = new int[25];
        int j = 0;


        for (int i = 0; i < itens.Length; i++){
            GameObject slot = inventario.transform.GetChild(i).gameObject;
            Item itemAux = slot.transform.GetChild(0).gameObject.GetComponent<Item>();

            
           
            if(!(itens[i] is null)){
                itemAux.id = itens[i].id;
                itemAux.nome = itens[i].nome;
                itemAux.vida = itens[i].vida;
                itemAux.ataque = itens[i].ataque;
                itemAux.defesa = itens[i].defesa;
                itemAux.magia = itens[i].magia;
                itemAux.imagem = itens[i].imagem;  

                itemAux.LoadItem();
            }
          
          
        }
    }

 


}
