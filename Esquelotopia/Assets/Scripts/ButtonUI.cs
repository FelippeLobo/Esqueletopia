using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string novaSalaDoJogo;
    public void StartGameButton()
    {
        SceneManager.LoadScene(novaSalaDoJogo);
    }

}
