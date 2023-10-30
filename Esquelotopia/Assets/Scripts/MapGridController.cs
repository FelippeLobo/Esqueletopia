using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapGridController : MonoBehaviour
{
    public static int MATRIX_SIZE = 6;

    private static GameObject instance = null;

    public static int visitedCounter = 0;
    private static int[] instancePosition;
    public static GameObject[][] visitedObjects;
    public int selectedCounter;
    public GameObject salaInicial;
    public static GameObject salaAtualObj;
    public static int interationCounter = 0;
    public string salaSelecionadaNome;
    public GameObject salaSelecionadaObj = null;
    public GameObject sala1, sala2, sala3, sala4, sala5, sala6;
    public GameObject sala7, sala8, sala9, sala10, sala11, sala12, sala13, sala14;
    public GameObject sala15, sala16, sala17, sala18, sala19, sala20, sala21, sala22;
    public GameObject sala23, sala24, sala25, sala26, sala27, sala28, sala29, sala30;
    public GameObject sala31, sala32, sala33, sala34, sala35, sala36;
    public static GameObject[][] map = new GameObject[MATRIX_SIZE][];
    public static string[,] mapScenes = {{"Sala 1", "Sala 2", "Sala 3", "Sala 4", "Sala 5", "Sala 6"},
                                {"Sala 7", "Sala 8", "Sala 9", "Sala 10", "Sala 11", "Sala 12"},
                                {"Sala 13", "Sala 14", "Sala 15", "Sala 16", "Sala 17", "Sala 18"},
                                {"Sala 19", "Sala 20", "Sala 21", "Sala 22", "Sala 23", "Sala 24"},
                                {"Sala 25", "Sala 26", "Sala 27", "Sala 28", "Sala 29", "Sala 30"},
                                {"Sala 31", "Sala 32", "Sala 33", "Sala 34", "Sala 35", "Sala 36"}};

    //private static string salaAtual;
    public void Start(){
        DontDestroyOnLoad(gameObject);
        Debug.Log(interationCounter);
        Debug.Log(salaAtualObj);
        visitedObjects = new GameObject[MATRIX_SIZE][];
        inicializador();
        if(instance == null){
            instance = this.gameObject;
            salaAtualObj = sala24;
            IniciaSalaInicial(salaAtualObj);
        } else {
            DestroyObject(instance);
            instance = this.gameObject;
            Debug.Log(salaAtualObj);
            InstanciaSalaAtual(instancePosition);
        }
        if(interationCounter == 0){
        }
        else if(interationCounter > 1){
        }
        //DontDestroyOnLoad(salaAtualObj);
        selectedCounter = 0;
        interationCounter += 1;
    }
    
    public void SetAtiveToFalse(){
        this.gameObject.SetActive(false);
        GameObject a = GameObject.Find("ButtonAvancar");
        a.SetActive(false);
    }
    public void SetAtiveToTrue(){
        this.gameObject.SetActive(true);
    }
    public void InstanciaSalaAtual(int[] pos){
        salaAtualObj= map[pos[0]][pos[1]];
        ImageScript salaInicial = salaAtualObj.GetComponent<ImageScript>();
        salaInicial.ativo = true;
        salaInicial.atual = true;
        salaInicial.visitado = true;
        UpdateActives(salaAtualObj);
    }
    public void inicializador(){
        sala1 = GameObject.Find("Image (0)");
        sala2 = GameObject.Find("Image (1)");
        sala3 = GameObject.Find("Image (2)");
        sala4 = GameObject.Find("Image (3)");
        sala5 = GameObject.Find("Image (4)");
        sala6 = GameObject.Find("Image (5)");
        sala7 = GameObject.Find("Image (6)");
        sala8 = GameObject.Find("Image (7)");
        sala9 = GameObject.Find("Image (8)");
        sala10 = GameObject.Find("Image (9)");
        sala11 = GameObject.Find("Image (10)");
        sala12 = GameObject.Find("Image (11)");
        sala13 = GameObject.Find("Image (12)");
        sala14 = GameObject.Find("Image (13)");
        sala15 = GameObject.Find("Image (14)");
        sala16 = GameObject.Find("Image (15)");
        sala17 = GameObject.Find("Image (16)");
        sala18 = GameObject.Find("Image (17)");
        sala19 = GameObject.Find("Image (18)");
        sala20 = GameObject.Find("Image (19)");
        sala21 = GameObject.Find("Image (20)");
        sala22 = GameObject.Find("Image (21)");
        sala23 = GameObject.Find("Image (22)");
        sala24 = GameObject.Find("Image (23)");
        sala25 = GameObject.Find("Image (24)");
        sala26 = GameObject.Find("Image (25)");
        sala27 = GameObject.Find("Image (26)");
        sala28 = GameObject.Find("Image (27)");
        sala29 = GameObject.Find("Image (28)");
        sala30 = GameObject.Find("Image (29)");
        sala31 = GameObject.Find("Image (30)");
        sala32 = GameObject.Find("Image (31)");
        sala33 = GameObject.Find("Image (32)");
        sala34 = GameObject.Find("Image (33)");
        sala35 = GameObject.Find("Image (34)");
        sala36 = GameObject.Find("Image (35)");
        map[0] = new GameObject[6]{sala1, sala2, sala3, sala4, sala5, sala6};
        map[1] = new GameObject[6]{sala7, sala8, sala9, sala10, sala11, sala12};
        map[2] = new GameObject[6]{sala13, sala14, sala15, sala16, sala17, sala18};
        map[3] = new GameObject[6]{sala19, sala20, sala21, sala22, sala23, sala24};
        map[4] = new GameObject[6]{sala25, sala26, sala27, sala28, sala29, sala30};
        map[5] = new GameObject[6]{sala31, sala32, sala33, sala34, sala35, sala36};
    }

    public void IniciaSalaInicial(GameObject obj){
        salaAtualObj = obj;
        ImageScript salaInicial = obj.GetComponent<ImageScript>();
        salaInicial.ativo = true;
        salaInicial.atual = true;
        salaInicial.visitado = true;
        UpdateActives(obj);
    }

    public void sumSelecionado(GameObject obj){
        ImageScript img = obj.GetComponent<ImageScript>();
        if(img.atual != true && img.visitado != true && img.ativo == true){
            if(selectedCounter > 0){
                ClearSelecionado();
            }
            int[] objPos = FindMatrixPosition(obj);
            salaSelecionadaObj = obj;
            img.selecionado = true;
            selectedCounter += 1;
            salaSelecionadaNome = mapScenes[objPos[0],objPos[1]];
            GameObject buttonAvancar = GameObject.Find("ButtonAvancar");
            AvancarSala BTAScript = buttonAvancar.GetComponent<AvancarSala>();
            BTAScript.ativo = true;
        }
    }

    public int[] FindMatrixPosition(GameObject obj){
        for(int i = 0; i < 6; i++){
            for(int j = 0; j < 6; j++){
                //ImageScript imgs = map[i][j].GetComponent<ImageScript>();
                if(obj == map[i][j]){
                    return new[]{i, j};
                }
            }
        }
        return new[]{-1, -1};
    }

    public void ClearSelecionado(){
        selectedCounter = 0;
        for(int i = 0; i < 6; i++){
            for(int j = 0; j < 6; j++){
                ImageScript imgs = map[i][j].GetComponent<ImageScript>();
                imgs.selecionado = false;
            }
        }
    }

    public void ClearAtual(){
        for(int i = 0; i < 6; i++){
            for(int j = 0; j < 6; j++){
                ImageScript imgs = map[i][j].GetComponent<ImageScript>();
                imgs.atual = false;
            }
        }
    }

    public void UpdateActives(GameObject salaAtual){
        int[] pos = FindMatrixPosition(salaAtual);
        int linha = pos[0];
        int coluna = pos[1];
        if(pos[0] != -1 && pos[1] != -1){

            if(linha == 0 && coluna == 0){
                ImageScript imgs = map[linha+1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna+1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
            }
            else if(linha == MATRIX_SIZE-1 && coluna == MATRIX_SIZE-1){
                ImageScript imgs = map[linha-1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna-1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
            }
            else if(linha == MATRIX_SIZE-1 && coluna == 0){
                ImageScript imgs = map[linha-1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna+1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
            }
            else if(linha == 0 && coluna == MATRIX_SIZE-1){
                ImageScript imgs = map[linha+1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna-1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
            }
            else if(linha > 0 && coluna == MATRIX_SIZE-1){
                ImageScript imgs = map[linha+1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha-1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna-1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
            }
            else if(linha == MATRIX_SIZE-1 && coluna > 0){
                ImageScript imgs = map[linha-1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna-1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna+1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
            }
            else if(linha > 0 && coluna > 0){
                ImageScript imgs = map[linha][coluna+1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna-1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha+1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha-1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
            }
            else if(linha > 0 && coluna == 0){
                ImageScript imgs = map[linha][coluna+1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha+1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha-1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
            }
            else if(linha == 0 && coluna > 0){
                ImageScript imgs = map[linha+1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna+1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna-1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
            }
        }
        //sala2.GetComponent<ImageScript>().ativo = true;
    }

    public void IrParaSala(string sala){
        salaAtualObj = salaSelecionadaObj;
        instancePosition = FindMatrixPosition(salaAtualObj);
        ClearAtual();
        ImageScript img = salaAtualObj.GetComponent<ImageScript>();
        img.atual = true;
        visitedCounter++;
        //SceneManager.LoadScene(salaSelecionadaNome);
        SceneManager.LoadScene("Sala");
    }

    void Update(){
        if(interationCounter != 0){
            UpdateActives(salaAtualObj);
        }

    }

}
