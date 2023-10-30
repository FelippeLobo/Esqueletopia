using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SalaBtn : MonoBehaviour
{
    //[SerializeField] private string novaSalaDoJogo = "Sala1";

    public void VoltarMapa(){
        GameObject Mapa = GameObject.Find("Mapa");
        MapGridController grid = Mapa.GetComponent<MapGridController>();
        grid.SetAtiveToTrue();
        
        SceneManager.LoadScene("Mapa");
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
