using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemLabelScript : MonoBehaviour
{
    public GameObject slot;
    private Item item;
    
    public void OnPointerEnter(GameObject newSlot){
        slot = newSlot;
        item = slot.transform.GetChild(0).GetComponent<Item>();
        if(item == null){
            item = slot.transform.GetChild(1).GetComponent<Item>();
        }
        if(item != null && item.id != 0){
             this.gameObject.transform.position = slot.transform.position - new Vector3(100, -75);
            this.gameObject.SetActive(true);
        }
       
    }
    public void OnPointerExit(){
        slot = null;
        item = null;

        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(item != null  && item.id != 0){
            GameObject itemImage = this.gameObject.transform.GetChild(0).gameObject;
            this.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.nome;
            GameObject blockText = this.gameObject.transform.GetChild(2).gameObject;
            blockText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Vida: "+item.vida;
            blockText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Ataque: "+item.ataque;
            blockText.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Defesa: "+item.defesa;
            blockText.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Magia: "+item.magia;
            

            if (itemImage != null)
            {       
                Image imagemObj = itemImage.GetComponent<Image>();
                imagemObj.sprite = item.imagem;
                if(item.imagem != null){
                    imagemObj.color =  new Color(imagemObj.color.r, imagemObj.color.g, imagemObj.color.b, 1f);
                }else{
                    imagemObj.color =  new Color(imagemObj.color.r, imagemObj.color.g, imagemObj.color.b, 0f);
                }  
            }
        }
        
    }
}

