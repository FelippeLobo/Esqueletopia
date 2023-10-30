using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public bool ativo = false;
    public Color color = new Color(1, 1, 1, 255);
    //[SerializeField] private string carregarSala = "Sala1";
    public void CarregarSalaButton()
    {
        //SceneManager.LoadScene(carregarSala);
        ativo = false;
    }

    public void AtivarSala()
    {
        GameObject a = GameObject.Find("Image1");
        //GameObject a = this.gameObject;
        Image oi = a.GetComponent<Image>();
        Debug.Log(oi);
        oi.color = color;
        ativo = true;
    }
}
