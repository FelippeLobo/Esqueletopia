using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class countDownTimmer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float remainingTime= 30f;

    // Update is called once per frame
    void Update()
    {
        remainingTime-=Time.deltaTime;
        if(remainingTime<=10){
            timerText.color = new Color32(145, 20, 20, 255);
        }
        if(remainingTime <= 0){
            remainingTime = 0;
            SceneManager.LoadScene("Mapa");
        }
        timerText.text = remainingTime.ToString("0");
    }
}
