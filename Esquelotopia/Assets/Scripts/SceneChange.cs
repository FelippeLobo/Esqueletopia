using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public GameObject odinBag;
    public GameObject odinBattle;
     public string returnScene;
      public void ChangeSceneToBag(){
        odinBattle.SetActive(true);
        odinBag.SetActive(false);
        
     }
}
