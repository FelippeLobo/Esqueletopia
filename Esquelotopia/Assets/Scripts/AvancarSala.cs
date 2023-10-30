using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AvancarSala : MonoBehaviour
{
    public bool ativo = false;
    public string salaSelecionada = "Sala1";
    public GameObject obj;
    //public salaSelecionada = 
    //[SerializeField] private string carregarSala = "Sala1";
    public void CarregarSalaButton(string nomeSala)
    {
        SceneManager.LoadScene(salaSelecionada);
    }

    // Update is called once per frame
    void Update()
    {
        obj = this.gameObject;
        if(ativo == true){
            Button botao = obj.GetComponent<Button>();
            botao.interactable = true;
        }else{
            Button botao = obj.GetComponent<Button>();
            botao.interactable = false;
        }
    }
}
