using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Project
{
    public class BotaoAtaque : MonoBehaviour
    {
        public GameObject inimigoPos;
        private Inimigo inimigo;

        void Start(){
            inimigo = inimigoPos.transform.GetChild(3).gameObject.GetComponent<Inimigo>();
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = inimigo.nome + " Lvl " + inimigo.level;
        }


    }
}
