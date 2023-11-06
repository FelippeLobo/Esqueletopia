using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
   public Slider slider;
   public TextMeshProUGUI texto;

    public void SetMaxHealth(float vidaMaxima, float vidaAtual){
        
        slider.maxValue = vidaMaxima;
        slider.value = vidaAtual;
        texto.text = vidaAtual + "/" + vidaMaxima;
    }
   public void SetHealth(float vida){
        slider.value = vida;
        texto.text = vida + "/" + slider.maxValue;
   }
}
