using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapGridController : MonoBehaviour
{
    public GameObject gridTile;
    public static int MATRIX_SIZE_X = 7;
    public static int MATRIX_SIZE_Y = 18;
    private static GameObject instance = null;
    public static int visitedCounter = 0;
    private static int[] instancePosition;
    public static int[] visitedI = new int[999];
    public static int[] visitedJ = new int[999];
    public int selectedCounter;
    public static GameObject salaAtualObj;
    public static int interationCounter = 0;
    public static GameObject salaSelecionadaObj;
    public static GameObject[][] map = new GameObject[MATRIX_SIZE_X][];
    public static int[][] ObjectsMap = new int[MATRIX_SIZE_X][];
    public static bool lockScale = false;
    public static int pathTaken = 0;

    public async void Start(){
        DontDestroyOnLoad(gameObject);
        InstantiateMap();
        InstantiateObjects();
        if(instance == null){
            instance = this.gameObject;
            salaAtualObj = map[3][0];
            IniciaSalaInicial(salaAtualObj);
        } else {
            DestroyObject(instance);
            instance = this.gameObject;
            InstanciaSalaAtual(instancePosition);
        }
        if(interationCounter == 0){
        }
        else if(interationCounter > 1){
        }
        selectedCounter = 0;
        interationCounter += 1;
        lockScale = false;
    }

    public void InstantiateObjects(){
        GameObject MapGrid = GameObject.Find("MapGrid");
        float posX = MapGrid.transform.position.x-424;
        float posXInicial = MapGrid.transform.position.x-424;
        float posY = MapGrid.transform.position.y+167;
        float tamTile = 50;
        for(int i = 0; i < MATRIX_SIZE_X; i++){
            map[i] = new GameObject[MATRIX_SIZE_Y];
            for(int j = 0; j < MATRIX_SIZE_Y; j++){
                map[i][j] = Instantiate(gridTile, MapGrid.transform);
                map[i][j].transform.position = new Vector3(posX, posY, 0);
                posX = posX + tamTile;
            }
            posX = posXInicial;
            posY = posY - tamTile;
        }
    }
    
    public void InstantiateMap(){
        // 0 blocked tiles
        // 1 path tiles
        // 2 initial tile
        // 30 chest 0
        // 31 chest 1
        // 32 chest 2
        // 33 chest 3
        // 34 chest 4
        // 4 campfire
        // 5 combat
        // 6 boss
        // 7 shop
        // 8 visited/closed path
        
        switch(pathTaken) 
            {
            case 0:
                ObjectsMap[0] = new int[18] {0, 0, 0, 1, 5, 1, 1, 5, 0, 0, 32, 1, 5, 0, 0, 0, 0, 0};
                ObjectsMap[1] = new int[18] {0, 0, 0, 5, 0, 0, 0, 31, 0, 0, 1, 0, 4, 7, 5, 0, 0, 0};
                ObjectsMap[2] = new int[18] {0, 5, 1, 5, 1, 5, 1, 5, 1, 0, 5, 0, 0, 0, 34, 5, 1, 0};
                ObjectsMap[3] = new int[18] {2, 1, 0, 0, 0, 0, 0, 0, 4, 7, 1, 0, 0, 0, 0, 0, 4, 6};
                ObjectsMap[4] = new int[18] {0, 5, 1, 5, 1, 5, 1, 1, 1, 0, 5, 1, 0, 0, 5, 7, 1, 0};
                ObjectsMap[5] = new int[18] {0, 0, 0, 30, 0, 0, 0, 5, 0, 0, 0, 1, 33, 5, 1, 0, 0, 0};
                ObjectsMap[6] = new int[18] {0, 0, 0, 5, 1, 1, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                break;
            case 1:
                ObjectsMap[0] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 32, 1, 5, 0, 0, 0, 0, 0};
                ObjectsMap[1] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 1, 0, 4, 7, 5, 0, 0, 0};
                ObjectsMap[2] = new int[18] {0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 5, 0, 0, 0, 34, 5, 1, 0};
                ObjectsMap[3] = new int[18] {2, 1, 0, 0, 0, 0, 0, 0, 4, 7, 1, 0, 0, 0, 0, 0, 4, 6};
                ObjectsMap[4] = new int[18] {0, 5, 1, 5, 1, 5, 1, 1, 1, 0, 5, 1, 0, 0, 5, 7, 1, 0};
                ObjectsMap[5] = new int[18] {0, 0, 0, 30, 0, 0, 0, 5, 0, 0, 0, 1, 33, 5, 1, 0, 0, 0};
                ObjectsMap[6] = new int[18] {0, 0, 0, 5, 1, 1, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                break;
            case 2:
                ObjectsMap[0] = new int[18] {0, 0, 0, 1, 5, 1, 1, 5, 0, 0, 32, 1, 5, 0, 0, 0, 0, 0};
                ObjectsMap[1] = new int[18] {0, 0, 0, 5, 0, 0, 0, 31, 0, 0, 1, 0, 4, 7, 5, 0, 0, 0};
                ObjectsMap[2] = new int[18] {0, 5, 1, 5, 1, 5, 1, 5, 1, 0, 5, 0, 0, 0, 34, 5, 1, 0};
                ObjectsMap[3] = new int[18] {2, 1, 0, 0, 0, 0, 0, 0, 4, 7, 1, 0, 0, 0, 0, 0, 4, 6};
                ObjectsMap[4] = new int[18] {0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 5, 1, 0, 0, 5, 7, 1, 0};
                ObjectsMap[5] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 33, 5, 1, 0, 0, 0};
                ObjectsMap[6] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                break;
            case 3:
                ObjectsMap[0] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 32, 1, 5, 0, 0, 0, 0, 0};
                ObjectsMap[1] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 1, 0, 4, 7, 5, 0, 0, 0};
                ObjectsMap[2] = new int[18] {0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 5, 0, 0, 0, 34, 5, 1, 0};
                ObjectsMap[3] = new int[18] {2, 1, 0, 0, 0, 0, 0, 0, 4, 7, 1, 0, 0, 0, 0, 0, 4, 6};
                ObjectsMap[4] = new int[18] {0, 5, 1, 5, 1, 5, 1, 1, 1, 0, 5, 1, 0, 0, 5, 7, 1, 0};
                ObjectsMap[5] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 33, 5, 1, 0, 0, 0};
                ObjectsMap[6] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                break;
            case 4:
                ObjectsMap[0] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 32, 1, 5, 0, 0, 0, 0, 0};
                ObjectsMap[1] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 1, 0, 4, 7, 5, 0, 0, 0};
                ObjectsMap[2] = new int[18] {0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 5, 0, 0, 0, 34, 5, 1, 0};
                ObjectsMap[3] = new int[18] {2, 1, 0, 0, 0, 0, 0, 0, 4, 7, 1, 0, 0, 0, 0, 0, 4, 6};
                ObjectsMap[4] = new int[18] {0, 5, 1, 5, 8, 8, 8, 1, 1, 0, 5, 1, 0, 0, 5, 7, 1, 0};
                ObjectsMap[5] = new int[18] {0, 0, 0, 30, 0, 0, 0, 5, 0, 0, 0, 1, 33, 5, 1, 0, 0, 0};
                ObjectsMap[6] = new int[18] {0, 0, 0, 5, 1, 1, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                break;
            case 5:
                ObjectsMap[0] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 32, 1, 5, 0, 0, 0, 0, 0};
                ObjectsMap[1] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 1, 0, 4, 7, 5, 0, 0, 0};
                ObjectsMap[2] = new int[18] {0, 5, 1, 5, 1, 5, 1, 5, 1, 0, 5, 0, 0, 0, 34, 5, 1, 0};
                ObjectsMap[3] = new int[18] {2, 1, 0, 0, 0, 0, 0, 0, 4, 7, 1, 0, 0, 0, 0, 0, 4, 6};
                ObjectsMap[4] = new int[18] {0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 5, 1, 0, 0, 5, 7, 1, 0};
                ObjectsMap[5] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 33, 5, 1, 0, 0, 0};
                ObjectsMap[6] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                break;
            case 6:
                ObjectsMap[0] = new int[18] {0, 0, 0, 1, 5, 1, 1, 5, 0, 0, 32, 1, 5, 0, 0, 0, 0, 0};
                ObjectsMap[1] = new int[18] {0, 0, 0, 5, 0, 0, 0, 31, 0, 0, 1, 0, 4, 7, 5, 0, 0, 0};
                ObjectsMap[2] = new int[18] {0, 5, 1, 5, 8, 8, 8, 5, 1, 0, 5, 0, 0, 0, 34, 5, 1, 0};
                ObjectsMap[3] = new int[18] {2, 1, 0, 0, 0, 0, 0, 0, 4, 7, 1, 0, 0, 0, 0, 0, 4, 6};
                ObjectsMap[4] = new int[18] {0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 5, 1, 0, 0, 5, 7, 1, 0};
                ObjectsMap[5] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 33, 5, 1, 0, 0, 0};
                ObjectsMap[6] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                break;
            case 7:
                ObjectsMap[0] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 32, 1, 5, 0, 0, 0, 0, 0};
                ObjectsMap[1] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 1, 0, 4, 7, 5, 0, 0, 0};
                ObjectsMap[2] = new int[18] {0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 5, 0, 0, 0, 34, 5, 1, 0};
                ObjectsMap[3] = new int[18] {2, 8, 0, 0, 0, 0, 0, 0, 8, 8, 1, 0, 0, 0, 0, 0, 4, 6};
                ObjectsMap[4] = new int[18] {0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 5, 1, 0, 0, 5, 7, 1, 0};
                ObjectsMap[5] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 33, 5, 1, 0, 0, 0};
                ObjectsMap[6] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                break;
            case 8:
                ObjectsMap[0] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 32, 1, 5, 0, 0, 0, 0, 0};
                ObjectsMap[1] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 1, 0, 4, 7, 5, 0, 0, 0};
                ObjectsMap[2] = new int[18] {0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 5, 0, 0, 0, 34, 5, 1, 0};
                ObjectsMap[3] = new int[18] {2, 8, 0, 0, 0, 0, 0, 0, 8, 8, 1, 0, 0, 0, 0, 0, 4, 6};
                ObjectsMap[4] = new int[18] {0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 8, 8, 0, 0, 8, 8, 8, 0};
                ObjectsMap[5] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 8, 8, 8, 8, 0, 0, 0};
                ObjectsMap[6] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                break;
            case 9:
                ObjectsMap[0] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 8, 8, 8, 0, 0, 0, 0, 0};
                ObjectsMap[1] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 8, 0, 8, 8, 8, 0, 0, 0};
                ObjectsMap[2] = new int[18] {0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 8, 0, 0, 0, 8, 8, 8, 0};
                ObjectsMap[3] = new int[18] {2, 8, 0, 0, 0, 0, 0, 0, 8, 8, 1, 0, 0, 0, 0, 0, 4, 6};
                ObjectsMap[4] = new int[18] {0, 8, 8, 8, 8, 8, 8, 8, 8, 0, 5, 1, 0, 0, 5, 7, 1, 0};
                ObjectsMap[5] = new int[18] {0, 0, 0, 8, 0, 0, 0, 8, 0, 0, 0, 1, 33, 5, 1, 0, 0, 0};
                ObjectsMap[6] = new int[18] {0, 0, 0, 8, 8, 8, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                break;
            default:
                break;
        }
    }

    public void resetLevel(){
        DestroyObject(instance);
        SceneManager.LoadScene("Initial_Screen");
    }

    public void verifyPathTaken(int[] pathPosition){
        int pathX = pathPosition[0];
        int pathY = pathPosition[1];
        if(pathX == 4 && pathY == 1){
            pathTaken = 1;
        }
        else if(pathX == 2 && pathY == 1){
            pathTaken = 2;
        }
        else if(pathX == 4 && pathY == 4){
            pathTaken = 3;
        }
        else if(pathX == 5 && pathY == 3){
            pathTaken = 4;
        }
        else if(pathX == 2 && pathY == 4){
            pathTaken = 5;
        }
        else if(pathX == 1 && pathY == 3){
            pathTaken = 6;
        }
        else if(pathX == 3 && pathY == 9){
            pathTaken = 7;
        }
        else if(pathX == 2 && pathY == 10){
            pathTaken = 8;
        }
        else if(pathX == 4 && pathY == 10){
            pathTaken = 9;
        }
    }

    public void StartMap(){
        for(int i = 0; i < MATRIX_SIZE_X; i++){
            for(int j = 0; j < MATRIX_SIZE_Y; j++){
                switch(ObjectsMap[i][j]) 
                {
                case 0:
                    ImageScript tile = map[i][j].GetComponent<ImageScript>();
                    tile.tileType = "blocked";
                    tile.bloqueado = true;
                    break;
                case 1:
                    tile = map[i][j].GetComponent<ImageScript>();
                    tile.tileType = "path";
                    break;
                case 2:
                    tile = map[i][j].GetComponent<ImageScript>();
                    tile.tileType = "initial";
                    break;
                case 30:
                    tile = map[i][j].GetComponent<ImageScript>();
                    tile.tileType = "chest0";
                    break;
                case 31:
                    tile = map[i][j].GetComponent<ImageScript>();
                    tile.tileType = "chest1";
                    break;
                case 32:
                    tile = map[i][j].GetComponent<ImageScript>();
                    tile.tileType = "chest2";
                    break;
                case 33:
                    tile = map[i][j].GetComponent<ImageScript>();
                    tile.tileType = "chest3";
                    break;
                case 34:
                    tile = map[i][j].GetComponent<ImageScript>();
                    tile.tileType = "chest4";
                    break;
                case 4:
                    tile = map[i][j].GetComponent<ImageScript>();
                    tile.tileType = "campfire";
                    break;
                case 5:
                    tile = map[i][j].GetComponent<ImageScript>();
                    tile.tileType = "fight";
                    break;
                case 6:
                    tile = map[i][j].GetComponent<ImageScript>();
                    tile.tileType = "boss";
                    break;
                case 7:
                    tile = map[i][j].GetComponent<ImageScript>();
                    tile.tileType = "shop";
                    break;
                case 8:
                    tile = map[i][j].GetComponent<ImageScript>();
                    tile.tileType = "path";
                    tile.visitado = true;
                    break;
                default:
                    break;
                }
            }
        }
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
        salaInicial.visitado = true;
    }

    public void IniciaSalaInicial(GameObject obj){
        salaAtualObj = obj;
        ImageScript salaInicial = obj.GetComponent<ImageScript>();
        salaInicial.ativo = true;
        salaInicial.atual = true;
    }

    public void sumSelecionado(GameObject obj){
        ImageScript img = obj.GetComponent<ImageScript>();
        int[] objPos = FindMatrixPosition(obj);
        if(img.atual != true && img.isVisited() != true && img.ativo == true && img.bloqueado == false){
            zoomScale(objPos);
            if(selectedCounter > 0){
                ClearSelecionado();
            }
            salaSelecionadaObj = obj;
            img.selecionado = true;
            selectedCounter += 1;
            GameObject buttonAvancar = GameObject.Find("ButtonAvancar");
            AvancarSala BTAScript = buttonAvancar.GetComponent<AvancarSala>();
            BTAScript.ativo = true;
        }
    }

    public void zoomScale(int[] pos){      
        if(lockScale == false){
            GameObject mapGrid = GameObject.Find("MapGrid");
            mapGrid.transform.localScale = new Vector3(1.7f, 1.7f, 1);
            float newPosX;
            if((pos[1]-1) > 6){
                newPosX = mapGrid.transform.position.x + 250f - (80 * 6);
            }
            else{
                newPosX = mapGrid.transform.position.x + 250f - (80 * (pos[1]-1));
            }
            float newPosY = mapGrid.transform.position.y - 35f;
            mapGrid.transform.position = new Vector3(newPosX ,newPosY , 0);
            lockScale = true;
        }
    }

    public int[] FindMatrixPosition(GameObject obj){
        for(int i = 0; i < MATRIX_SIZE_X; i++){
            for(int j = 0; j < MATRIX_SIZE_Y; j++){
                if(obj == map[i][j]){
                    return new[]{i, j};
                }
            }
        }
        return new[]{-1, -1};
    }

    public void ClearSelecionado(){
        selectedCounter = 0;
        for(int i = 0; i < MATRIX_SIZE_X; i++){
            for(int j = 0; j < MATRIX_SIZE_Y; j++){
                ImageScript imgs = map[i][j].GetComponent<ImageScript>();
                imgs.selecionado = false;
            }
        }
    }

    public void CheckVisited(){
        for(int k = 0; k < visitedCounter; k++){
            ImageScript img = map[visitedI[k]][visitedJ[k]].GetComponent<ImageScript>();
            img.visitado = true;            
        }
    }

    public void activateActualNeighbours(GameObject salaAtual){
        ImageScript salaAtualObj = salaAtual.GetComponent<ImageScript>();
        salaAtualObj.atual = true;
        salaAtualObj.visitado = true;

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
            else if(linha == MATRIX_SIZE_Y-1 && coluna == MATRIX_SIZE_Y-1){
                ImageScript imgs = map[linha-1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna-1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
            }
            else if(linha == MATRIX_SIZE_Y-1 && coluna == 0){
                ImageScript imgs = map[linha-1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna+1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
            }
            else if(linha == 0 && coluna == MATRIX_SIZE_Y-1){
                ImageScript imgs = map[linha+1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna-1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
            }
            else if(linha > 0 && coluna == MATRIX_SIZE_Y-1){
                ImageScript imgs = map[linha+1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha-1][coluna].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
                imgs = map[linha][coluna-1].GetComponent<ImageScript>();
                imgs.GetComponent<ImageScript>().ativo = true;
            }
            else if(linha == MATRIX_SIZE_Y-1 && coluna > 0){
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
                if(linha < MATRIX_SIZE_X-1){
                    imgs = map[linha+1][coluna].GetComponent<ImageScript>();
                    imgs.GetComponent<ImageScript>().ativo = true;
                }
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
    }

    public void IrParaSala(string sala){
        
        GameObject salaAnterior = salaAtualObj;
        salaAtualObj = salaSelecionadaObj;
        instancePosition = FindMatrixPosition(salaAtualObj);
        int[] lastInstancePosition = FindMatrixPosition(salaAnterior);
        visitedI[visitedCounter] = lastInstancePosition[0];
        visitedJ[visitedCounter] = lastInstancePosition[1];
        visitedCounter += 1;
        ImageScript salaAtual = salaAtualObj.GetComponent<ImageScript>();
        salaAtual.atual = true;

        verifyPathTaken(instancePosition);

        switch(salaAtual.tileType) {
            case "path":
                SceneManager.LoadScene("Sala");
                break;
            case "initial":
                SceneManager.LoadScene("Sala");
                break;
            case "chest0":
                SceneManager.LoadScene("Maze");
                break;
            case "chest1":
                SceneManager.LoadScene("Maze 1");
                break;
            case "chest2":
                SceneManager.LoadScene("Maze 2");
                break;
            case "chest3":
                SceneManager.LoadScene("Maze 3");
                break;
            case "chest4":
                SceneManager.LoadScene("Maze 4");
                break;
            case "campfire":
                SceneManager.LoadScene("Descanso");
                break;
            case "fight":
                SceneManager.LoadScene("Backup");
                break;
            case "boss":
                SceneManager.LoadScene("BossFight");
                break;
            case "shop":
                SceneManager.LoadScene("Shop");
                break;
            default:
                break;
        }
    }

    public void UpdateTilesStatus(GameObject salaAtual){
        CheckVisited();
        activateActualNeighbours(salaAtual);
        StartMap();
    }
    
    void Update(){
        if(interationCounter != 0){
            UpdateTilesStatus(salaAtualObj);
        }
    }
}
