using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class EndLoadingController : MonoBehaviour
    {
       private Animator personagemAnimator;
        public GameObject personagem;
        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(other.gameObject.tag == "Player"){
                personagemAnimator.SetBool("isStop", true);
                UnityEngine.Debug.Log("Voltar para o MAPA");
            }
        }
        void Start()
        {
            personagemAnimator = personagem.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
