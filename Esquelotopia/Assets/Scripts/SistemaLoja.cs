using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.Random;
using System;




    public class SistemaLoja : MonoBehaviour
    {
        private Item[] itensVenda;
        public Personagem personagem;
        public int qtdItens;
        public GameObject barraca;

        private TextMeshProUGUI nome;
        private TextMeshProUGUI valor;

        private Image imagem;

        private TextMeshProUGUI ataque;
        private TextMeshProUGUI defesa;
        private TextMeshProUGUI vida;

        private GameObject alerta;

        private GameObject moedas;

        private TextMeshProUGUI magia;

        void Start()
        {   
            personagem = GameObject.Find("PersonagemStats").GetComponent<Personagem>();
            moedas = GameObject.Find("Moedas");
            alerta = GameObject.Find("Alerta");

            moedas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+Personagem.moedas;
            itensVenda = new Item[qtdItens];
            itensVenda = Personagem.itensManager.CreateListaItem(qtdItens);

            GameObject item = barraca.transform.GetChild(0).gameObject;
            UpdateItens(item, 0);
            for (int i = 1; i < qtdItens; i++)
            {
                GameObject itemCopia = Instantiate(item);
                itemCopia.transform.parent = barraca.transform;
                UpdateItens(itemCopia, i);
            }


        }

        public void ComprarItem(GameObject button){
            Item item = button.GetComponent<Item>();
            if(Personagem.moedas >= item.preco){
                Personagem.moedas -= item.preco;
                moedas.GetComponent<TextMeshProUGUI>().text = "$"+Personagem.moedas;
                Personagem.ArmazenarItem(item);
                button.GetComponent<Button>().interactable  = false;
            }else{
                //alerta.GetComponent<DmgText>().ResetFadeOut();

            }

           

        }

        private void UpdateItens(GameObject item, int i){
            nome = item.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            valor = item.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            imagem = item.transform.GetChild(2).GetComponent<Image>();

            GameObject statusBlock = item.transform.GetChild(3).gameObject;

            ataque = statusBlock.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            vida = statusBlock.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            defesa = statusBlock.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            magia = statusBlock.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

            int valorR = (int)(Math.Ceiling(UnityEngine.Random.Range(50f, 5000f)));

            item.GetComponent<Item>().id = itensVenda[i].id;
            item.GetComponent<Item>().nome = itensVenda[i].nome;
            item.GetComponent<Item>().vida = itensVenda[i].vida;
            item.GetComponent<Item>().ataque = itensVenda[i].ataque;
            item.GetComponent<Item>().defesa = itensVenda[i].defesa;
            item.GetComponent<Item>().magia = itensVenda[i].magia;
            item.GetComponent<Item>().imagem = itensVenda[i].imagem;
            item.GetComponent<Item>().preco = valorR;

            nome.text = itensVenda[i].nome;    
            valor.text = "$" + valorR;
            imagem.sprite = itensVenda[i].imagem;
            ataque.text = "Ataque: "+itensVenda[i].ataque;
            vida.text = "Vida: "+itensVenda[i].vida;
            defesa.text = "Defesa: "+itensVenda[i].defesa;
            magia.text = "Magia: "+itensVenda[i].magia;
        }

 

 
}
