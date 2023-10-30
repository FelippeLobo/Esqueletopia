using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour
{
    public GameObject salaObj;
    public bool atual;
    public bool ativo;
    public bool visitado;
    public bool selecionado;
    public Color corAtual;
    public Color corAtivo;
    public Color corSelecionado;
    public Color corVisitado;
    void Start(){
        //salaObj = this.gameObject;
        ativo = false;
        visitado = false;
        selecionado = false;
        atual = false;
        corAtual =  new Color32(171, 51, 68, 255);
        corAtivo =  new Color32(236, 236, 236, 255);
        corSelecionado =  new Color32(168, 226, 144, 255);
        corVisitado =  new Color32(50, 50, 50, 255);
        salaObj = new GameObject();
        Iniciador();
    }

    public void Iniciador(){
        salaObj = this.gameObject;
    }

    //[SerializeField] private string carregarSala = "Sala1";
    void CarregarSalaButton()
    {
        //SceneManager.LoadScene(carregarSala);
        ativo = false;
    }

    public void AtivarSala()
    {
        //GameObject a = GameObject.Find("Image1");
        //GameObject a = this.gameObject;
        //Image salaImg = salaObj.GetComponent<Image>();
        //Debug.Log(salaImg);
        //salaImg.color = color;
        ativo = true;
    }

    public void SelecionaSala(){
        selecionado = true;
        GameObject MapGrid = GameObject.Find("MapGrid");
        //Debug.Log(MapGrid.GetComponent<MapGridController>());
        //MapGridController mgc = MapGrid.GetComponent<MapGridController>();
        //mgc.selectedCounter += 1; 
    }

    void Update(){
        
        if(atual == true){
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
}
