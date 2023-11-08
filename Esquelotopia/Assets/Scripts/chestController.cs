using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestController : MonoBehaviour
{
    public Sprite openChest;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            gameObject.GetComponent<SpriteRenderer>().sprite = openChest;
            gameObject.GetComponent<AudioSource>().Play();
            Personagem.ChestBonus();
        }    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
