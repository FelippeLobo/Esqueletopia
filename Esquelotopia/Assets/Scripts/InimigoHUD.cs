using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InimigoHUD : MonoBehaviour
{   

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;

    public void SetHUD(Inimigo inimigo){
        nameText.text = inimigo.nome;
        levelText.text = "lvl "+ inimigo.level;
        hpSlider.maxValue  = inimigo.vidaTotal;
        hpSlider.value  = inimigo.vidaAtual;
    }

    public void SetHP(float vida){
        hpSlider.value = vida;
    }
}
