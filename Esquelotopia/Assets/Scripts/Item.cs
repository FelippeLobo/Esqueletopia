using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item : MonoBehaviour 
{
    public int id;
    public string nome;
    public float vida;
    public int ataque;
    public int defesa;
    public int magia;
    public Sprite imagem;

    public int preco;

    public ItemStats itemStats;

    public Item(int id, string nome, float vida, int ataque, int defesa, int magia, Sprite imagem)
    {
        this.id = id;
        this.nome = nome;
        this.vida = vida;
        this.ataque = ataque;
        this.defesa = defesa;
        this.magia = magia;
        this.imagem = imagem;

    }

    public void LoadItem()
    {   
        GameObject buttonItem = this.gameObject;

        if (buttonItem != null)
        {       
            Image imagemObj = buttonItem.GetComponent<Image>();
              imagemObj.sprite = imagem;
            if(imagem != null){
                 imagemObj.color =  new Color(imagemObj.color.r, imagemObj.color.g, imagemObj.color.b, 1f);
            }else{
                 imagemObj.color =  new Color(imagemObj.color.r, imagemObj.color.g, imagemObj.color.b, 0f);
            }  
        }
    }
   
}