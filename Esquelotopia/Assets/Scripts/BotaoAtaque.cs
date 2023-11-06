using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Project
{
    public class BotaoAtaque : MonoBehaviour
    {
        public Inimigo inimigo;

        void Start(){
             UnityEngine.Debug.Log(this.gameObject.name);
              UnityEngine.Debug.Log(this.gameObject.tag);
            UnityEngine.Debug.Log(transform.childCount);
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = inimigo.nome + " Lvl " + inimigo.level;
        }


    }
}
