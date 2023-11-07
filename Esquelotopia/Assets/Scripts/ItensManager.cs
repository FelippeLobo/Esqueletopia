using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


    public class ItensManager 
    {   
        private string[] nomes = {
            "Espada Mágica de Fogo", "Armadura do Dragão", "Varinha do Mago", "Biscoitos", "Escudo de Ébano", "Adaga das Sombras", "Armadura de Cura", "Bastão do Arcano", "Anel da Proteção", "Elmo de Prata", "Espada de Gelo", "Amuleto da Sabedoria", "Maçã Encantada", "Cajado do Trovão", "Elmo de Cristal", "Carne de Zombi Amaldiçoada", "Adaga Envenenada", "Armadura da Luz", "Arco Élfico", "Poção de Energia", "Cajado das Estrelas", "Escudo Rúnico", "Elmo do Ladrão", "Espada do Rei", "Poção de Resistência", "Anel da Vida Eterna", "Machado dos Anões", "Capa da Invisibilidade", "Cajado das Trevas", "Elmo do Dragão", "Lança", "Porrete", "Picareta", "Cajado", "Machado"
        };

        private int[] ids = {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35
        };

        private int[] ataques = {
            25, 0, 10, 0, 0, 20, 0, 15, 0, 0, 30, 0, 0, 20, 0, 25, 0, 30, 0, 0, 15, 0, 0, 35, 0, 20, 0, 5, 25, 0, 10, 15, 5, 0, 0
        };

        private int[] defesas = {
            5, 35, 0, 0, 20, 0, 25, 0, 15, 10, 5, 10, 0, 0, 15, 0, 0, 30, 0, 0, 0, 25, 10, 0, 0, 5, 25, 10, 5, 30, 15, 10, 5, 10,10
        };

        private float[] vidas = {
            0, 20, 5, 30, 10, 5, 15, 10, 5, 5, 0, 20, 10, 10, 5, 0, 20, 15, 5, 15, 20, 10, 5, 0, 20, 30, 0, 10, 5, 20, 10, 5, 5, 5,5
        };

        private int[] magias = {
            10, 5, 30, 0, 5, 15, 10, 25, 10, 0, 20, 15, 25, 30, 5, 10, 30, 15, 20, 15, 25, 10, 15, 5, 15, 10, 25, 0, 5, 30, 15, 25, 10, 10,0
        };

        private string[] imagens = {
            "Assets/SpriteItens/sword_02.png", "Assets/SpriteItens/Item__57.png", "Assets/SpriteItens/Item__20.png", "Assets/SpriteItens/cookies.png", "Assets/SpriteItens/shield_02.png", "Assets/SpriteItens/Item__00.png", "Assets/SpriteItens/Item__58.png", "Assets/SpriteItens/wand_02.png", "Assets/SpriteItens/Item__42.png", "Assets/SpriteItens/Item__44.png", "Assets/SpriteItens/Item__01.png", "Assets/SpriteItens/Item__33.png", "Assets/SpriteItens/Item__64.png", "Assets/SpriteItens/wand_01.png", "Assets/SpriteItens/Item__45.png", "Assets/SpriteItens/meat.png", "Assets/SpriteItens/Item__03.png", "Assets/SpriteItens/Item__59.png", "Assets/SpriteItens/Item__19.png", "Assets/SpriteItens/mana_potion.png", "Assets/SpriteItens/Item__22.png", "Assets/SpriteItens/Item__27.png", "Assets/SpriteItens/Item__55.png", "Assets/SpriteItens/Item__05.png", "Assets/SpriteItens/hp_potion.png", "Assets/SpriteItens/Item__43.png", "Assets/SpriteItens/Item__14.png", "Assets/SpriteItens/mantua.png", "Assets/SpriteItens/Item__23.png", "Assets/SpriteItens/Item__46.png", "Assets/Brackeys/2D Mega Pack/Weapons & Tools/Spear.png", "Assets/Brackeys/2D Mega Pack/Weapons & Tools/Club.png", "Assets/Brackeys/2D Mega Pack/Weapons & Tools/Pickaxe.png", "Assets/Brackeys/2D Mega Pack/Weapons & Tools/Staff.png", "Assets/Brackeys/2D Mega Pack/Weapons & Tools/Axe.png"
        };


        public static Item[] itens;
        public ItensManager(){
            itens = new Item[35];
            for (var i = 0; i < 35; i++)
            {
                itens[i] = new Item(0, "", 0, 0, 0, 0, null);
            }
            ItemFactory();
        }
        

    public void ItemFactory(){

        for(int i = 0; i < 35; i++)
        {      
            Texture2D newTexture = CarregarTexturaDoArquivo(imagens[i]);

            Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));
  

            itens[i] = new Item(ids[i], nomes[i], vidas[i], ataques[i], defesas[i], magias[i], newSprite);
  
        }
  
    }

    public Item[] CreateListaItem(int tam){

        Item[] retorno = new Item[tam];
        if(tam < 35){

             for (int i = 0; i < tam; i++)
            {
                int j = (int)UnityEngine.Random.Range(0, tam);
                retorno[i] = itens[j];
                
                
            }
        }

        return retorno;
    }

        public Item[] CreateStartListaItem(int tam){
            
        Item[] retorno = new Item[tam];
        if(tam < 35){

             for (int i = 0; i < tam; i++)
            {
                int j = (int)UnityEngine.Random.Range(31, 34);

                retorno[i] = itens[j];
                
                
            }
        }

        return retorno;
    }
private Texture2D CarregarTexturaDoArquivo(string caminho)
    {

        Texture2D textura = null;

        if (System.IO.File.Exists(caminho))
        {
            byte[] dadosDoArquivo = System.IO.File.ReadAllBytes(caminho);
            textura = new Texture2D(2, 2);

            if (textura.LoadImage(dadosDoArquivo))
            {

                return textura;
            }
            else
            {
                UnityEngine.Debug.LogError("Falha ao carregar a imagem do arquivo.");
            }
        }
        else
        {
            UnityEngine.Debug.LogError("Arquivo não encontrado: " + caminho);
        }

 
        return null;
    }
}

