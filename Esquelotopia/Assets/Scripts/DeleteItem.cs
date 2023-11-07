using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class DeleteItem : MonoBehaviour
    {
        private Item item;
        private GameObject slot;

        // Update is called once per frame
        void Update()
        {
            slot = GameObject.Find("Slot_Delete");     
            item = slot.transform.GetChild(0).gameObject.GetComponent<Item>();
            if (item == null){
                item = slot.transform.GetChild(1).gameObject.GetComponent<Item>();
            }    

             if(item.nome != "" && item.id != 0){
                item.nome = "";
                item.id = 0;
                item.vida = 0;
                item.ataque = 0;
                item.defesa = 0;
                item.magia = 0;
                item.imagem = null;
                item.LoadItem();
             }
        }
    }
}
