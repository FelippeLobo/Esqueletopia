using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class AnimationTrigger : MonoBehaviour
    {
        
        private Animator personagemAnimator;
        public GameObject personagem;
        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(other.gameObject.tag == "Player"){
                personagemAnimator.SetBool("isStop", true);
                Movimento2.flag = false;
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
