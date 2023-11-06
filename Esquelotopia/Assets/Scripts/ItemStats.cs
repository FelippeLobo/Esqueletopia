using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ItemStats 
    {
            public int id;
            public string nome;
            public float vida;
            public int ataque;
            public int defesa;
            public int magia;
            public Sprite imagem;

       
          public ItemStats(int id, string nome, float vida, int ataque, int defesa, int magia, Sprite imagem)
        {
            this.id = id;
            this.nome = nome;
            this.vida = vida;
            this.ataque = ataque;
            this.defesa = defesa;
            this.magia = magia;
            this.imagem = imagem;

        }
    }

