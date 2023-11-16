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
        10, 0, 5, 0, 0, 7, 0, 6, 0, 0, 12, 0, 0, 9, 0, 10, 0, 12, 0, 0, 7, 0, 0, 14, 0, 9, 0, 4, 11, 0, 6, 7, 4, 0, 0
    };

    private int[] defesas = {
        4, 14, 0, 0, 9, 0, 11, 0, 7, 5, 4, 5, 0, 0, 6, 0, 0, 12, 0, 0, 0, 10, 5, 0, 0, 4, 11, 5, 4, 12, 7, 5, 4, 5, 5
    };

    private int[] vidas = {
        0, 8, 4, 9, 5, 4, 6, 5, 4, 4, 0, 8, 5, 5, 4, 0, 8, 6, 4, 6, 8, 5, 4, 0, 8, 9, 0, 5, 4, 8, 5, 4, 4, 4, 4
    };

    private int[] magias = {
        6, 4, 10, 0, 4, 8, 6, 9, 6, 0, 8, 6, 9, 10, 4, 6, 10, 8, 9, 8, 10, 6, 8, 4, 8, 6, 10, 0, 4, 10, 0, 0, 0, 0, 0
    };

    private string[] imagens = {
            "Assets/SpriteItens/sword_02.png", "Assets/SpriteItens/Item__57.png", "Assets/SpriteItens/Item__20.png", "Assets/SpriteItens/cookies.png", "Assets/SpriteItens/shield_02.png", "Assets/SpriteItens/Item__00.png", "Assets/SpriteItens/Item__58.png", "Assets/SpriteItens/wand_02.png", "Assets/SpriteItens/Item__42.png", "Assets/SpriteItens/Item__44.png", "Assets/SpriteItens/Item__01.png", "Assets/SpriteItens/Item__33.png", "Assets/SpriteItens/Item__64.png", "Assets/SpriteItens/wand_01.png", "Assets/SpriteItens/Item__45.png", "Assets/SpriteItens/meat.png", "Assets/SpriteItens/Item__03.png", "Assets/SpriteItens/Item__59.png", "Assets/SpriteItens/Item__19.png", "Assets/SpriteItens/mana_potion.png", "Assets/SpriteItens/Item__22.png", "Assets/SpriteItens/Item__27.png", "Assets/SpriteItens/Item__55.png", "Assets/SpriteItens/Item__05.png", "Assets/SpriteItens/hp_potion.png", "Assets/SpriteItens/Item__43.png", "Assets/SpriteItens/Item__14.png", "Assets/SpriteItens/mantua.png", "Assets/SpriteItens/Item__23.png", "Assets/SpriteItens/Item__46.png", "Assets/Brackeys/2D Mega Pack/Weapons & Tools/Spear.png", "Assets/Brackeys/2D Mega Pack/Weapons & Tools/Club.png", "Assets/Brackeys/2D Mega Pack/Weapons & Tools/Pickaxe.png", "Assets/Brackeys/2D Mega Pack/Weapons & Tools/Staff.png", "Assets/Brackeys/2D Mega Pack/Weapons & Tools/Axe.png"
        };


    public static Item[] itens;
    public ItensManager()
    {
        itens = new Item[35];
        for (var i = 0; i < 35; i++)
        {
            itens[i] = new Item(0, "", 0, 0, 0, 0, null);
        }
        ItemFactory();
    }


    public void ItemFactory()
    {

        for (int i = 0; i < 35; i++)
        {
            Texture2D newTexture = CarregarTexturaDoArquivo(imagens[i]);

            Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));


            itens[i] = new Item(ids[i], nomes[i], vidas[i], ataques[i], defesas[i], magias[i], newSprite);

        }

    }

public Item[] CreateListaItem(int tam)
{
    if (tam >= 35)
    {
        UnityEngine.Debug.LogError("A quantidade de itens solicitada é maior ou igual ao número total de itens disponíveis. Não é possível garantir que não haverá repetição.");
        return null;
    }

    Item[] retorno = new Item[tam];
    List<Item> itensDisponiveis = new List<Item>(itens); // 'itens' é a lista completa de itens disponíveis
    List<Item> itensEscolhidos = new List<Item>();

    for (int i = 0; i < tam; i++)
    {
        if (itensDisponiveis.Count == 0)
        {
            UnityEngine.Debug.LogError("Não há itens suficientes disponíveis para atender à quantidade solicitada.");
            return null;
        }

        int indexEscolhido = UnityEngine.Random.Range(0, itensDisponiveis.Count);
        retorno[i] = itensDisponiveis[indexEscolhido];
        itensEscolhidos.Add(itensDisponiveis[indexEscolhido]);
        itensDisponiveis.RemoveAt(indexEscolhido);
    }

    return retorno;
}

    public Item[] CreateStartListaItem(int tam)
    {

        Item[] retorno = new Item[tam];
        if (tam < 35)
        {

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

