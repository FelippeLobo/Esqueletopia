using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStore : MonoBehaviour
{
   public static DataStore instance = null;

   public SistemaBatalha sistemaBatalha;
   public Personagem personagem;

   public Inimigo inimigo1;
   public Inimigo inimigo2;
   public Inimigo inimigo3;

   public bool flag;

    private void Awake()
    {       
           
           if (instance != null) { Debug.Log("instance not null"); Destroy(gameObject);return; }
            else
            {   
                Debug.Log("instance null");
                DontDestroyOnLoad(gameObject);
                instance = this;
            }
    }
    private void Start()
    {
            flag =  false;
            Debug.Log("Start Data Store");
            personagem = GameObject.Find("Player").GetComponent<Personagem>();
            inimigo1 = GameObject.Find("Inimigo1").GetComponent<Inimigo>();
            inimigo2 = GameObject.Find("Inimigo2").GetComponent<Inimigo>();
            inimigo3 = GameObject.Find("Inimigo3").GetComponent<Inimigo>();
            sistemaBatalha = GameObject.Find("SistemaBatalha").GetComponent<SistemaBatalha>();
            
    }

    public void UpdateDataStore(){
        instance.personagem = GameObject.Find("Player").GetComponent<Personagem>();
        instance.inimigo1 = GameObject.Find("Inimigo1").GetComponent<Inimigo>();
        instance.inimigo2 = GameObject.Find("Inimigo2").GetComponent<Inimigo>();
        instance.inimigo3 = GameObject.Find("Inimigo3").GetComponent<Inimigo>();
        instance.sistemaBatalha = GameObject.Find("SistemaBatalha").GetComponent<SistemaBatalha>();
    }
}
