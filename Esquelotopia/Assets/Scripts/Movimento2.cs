using System.Threading;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Movimento2 : MonoBehaviour
    {
        public UnityEngine.Vector3 destination;
        public static bool flag;
        
        void Start()
        {   
            flag = true;
            destination = new UnityEngine.Vector2(200, 200);
    
        }


        // Update is called once per frame
        void Update()
        {
            if(flag){
                transform.position = UnityEngine.Vector2.Lerp(transform.position, destination, Time.deltaTime);
            }
        }
    }

