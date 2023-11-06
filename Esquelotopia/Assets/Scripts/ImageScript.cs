using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour
{
    public GameObject salaObj;
    public Sprite imagemSalaInicial;
    public Sprite imagemPath;
    public Sprite imageCombat;
    public Sprite imageCrown;
    public Sprite imageCampfire;
    public Sprite imageChest;
    public Sprite imageMoneyBag;
    public bool salaInicial;
    public bool atual;
    public bool ativo;
    public bool visitado;
    public bool selecionado;
    public bool bloqueado;
    public string tileType;
    public Color corAtual;
    public Color corAtivo;
    public Color corSelecionado;
    public Color corVisitado;
    public Color corBloqueado;
    void Start(){
        //salaObj = this.gameObject;
        salaInicial = false;
        ativo = false;
        visitado = false;
        selecionado = false;
        atual = false;
        bloqueado = false;
        corAtual =  new Color32(171, 51, 68, 255);
        corAtivo =  new Color32(255, 255, 255, 255);
        corSelecionado =  new Color32(168, 226, 144, 255);
        corVisitado =  new Color32(50, 50, 50, 255);
        corBloqueado = new Color32(0, 0, 0, 0);
        salaObj = this.gameObject;
    }

    public bool isVisited(){
        return visitado;
    }

    public void tilesColorUpdate(){
        if(bloqueado == true){
            Image salaImg = salaObj.GetComponent<Image>();
            salaImg.color =  corBloqueado;
        }
        else if(atual == true){
            Image salaImg = salaObj.GetComponent<Image>();
            salaImg.color =  corAtual;
        }
        else if(visitado == true){
            Image salaImg = salaObj.GetComponent<Image>();
            salaImg.color =   corVisitado;
        }
        else if(selecionado == true && ativo == true){
            Image salaImg = salaObj.GetComponent<Image>();
            salaImg.color =  corSelecionado;
        }
        else if(ativo == true){
            Image salaImg = salaObj.GetComponent<Image>();
            salaImg.color =  corAtivo;
        }
    }

    public void tilesSpriteUpdate(){
        switch(tileType) {
            case "blocked":
                break;
            case "path":
                Image salaImg = salaObj.GetComponent<Image>();
                salaImg.sprite = imagemPath;
                break;
            case "initial":
                salaImg = salaObj.GetComponent<Image>();
                salaImg.sprite = imagemSalaInicial;
                break;
            case "chest0":
                salaImg = salaObj.GetComponent<Image>();
                salaImg.sprite = imageChest;
                break;
            case "chest1":
                salaImg = salaObj.GetComponent<Image>();
                salaImg.sprite = imageChest;
                break;
            case "chest2":
                salaImg = salaObj.GetComponent<Image>();
                salaImg.sprite = imageChest;
                break;
            case "chest3":
                salaImg = salaObj.GetComponent<Image>();
                salaImg.sprite = imageChest;
                break;
            case "chest4":
                salaImg = salaObj.GetComponent<Image>();
                salaImg.sprite = imageChest;
                break;
            case "campfire":
                salaImg = salaObj.GetComponent<Image>();
                salaImg.sprite = imageCampfire;
                break;
            case "fight":
                salaImg = salaObj.GetComponent<Image>();
                salaImg.sprite = imageCombat;
                break;
            case "boss":
                salaImg = salaObj.GetComponent<Image>();
                salaImg.sprite = imageCrown;
                break;
            case "shop":
                salaImg = salaObj.GetComponent<Image>();
                salaImg.sprite = imageMoneyBag;
                break;
            default:
                break;
        }
    }

    void Update(){
        tilesColorUpdate();
        tilesSpriteUpdate();
    }
}
