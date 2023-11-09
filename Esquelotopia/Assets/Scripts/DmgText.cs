using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



    public class DmgText : MonoBehaviour
    {
        public float fadeSpeed;
        public float x;
        public float y;
        private static bool fadeOut;
        private static bool fadeIn;
        private bool active;
        // Update is called once per frame
        void Update()
        {   
                   if(DmgText.fadeOut){
                    
                    Color objectColor = this.gameObject.GetComponent<TextMeshProUGUI>().color;
                    float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                    y = 10 * fadeSpeed * Time.deltaTime;
                    
                    transform.position = transform.position + new Vector3(0, y, 0);
                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    this.gameObject.GetComponent<TextMeshProUGUI>().color = objectColor;
                    UnityEngine.Debug.Log("Fade Out, .a: "+objectColor.a);
                    if(objectColor.a <= 0){
                        DmgText.fadeOut = false;
                        DmgText.fadeIn = true;
                        active = false;
                        this.gameObject.SetActive(active);
                    }
                }

                if(!active){
                     y = 1000 * fadeSpeed * Time.deltaTime;
                    transform.position = transform.position - new Vector3(0, y, 0);
                }
                if(DmgText.fadeIn){
                    
                    Color objectColor = this.gameObject.GetComponent<TextMeshProUGUI>().color;
                    float fadeAmount = 1;

                    objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                    this.gameObject.GetComponent<TextMeshProUGUI>().color = objectColor;
                    
                    UnityEngine.Debug.Log("Fade In, .a: "+objectColor.a+"|FadeAmount: "+fadeAmount);
                    
                    
                    DmgText.fadeIn = false;              
                }
        }
        public void UpdateDMG(float dmg){
            this.gameObject.GetComponent<TextMeshProUGUI>().text = dmg+"";
        }
        public void FadeOutObject(){
            DmgText.fadeOut = true;
        }

        public void ResetFadeOut(){
            active = true;
            this.gameObject.SetActive(active);
            
            DmgText.fadeIn = false;
            DmgText.fadeOut = true;
            
            UnityEngine.Debug.Log("Reset: "+DmgText.fadeOut);

        }

    }

