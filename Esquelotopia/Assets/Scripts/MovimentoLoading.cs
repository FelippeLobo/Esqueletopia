using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class MovimentoLoading : MonoBehaviour
    {
        public UnityEngine.Vector3 destination;
        public static bool flag;
        
        void Start()
        {   
            flag = true;
            destination = new UnityEngine.Vector2(900, 180);
    
        }


        // Update is called once per frame
        void Update()
        {
            if(flag){
                transform.position = UnityEngine.Vector2.Lerp(transform.position, destination, Time.deltaTime/1.5f);
            }
        }
    }
}
