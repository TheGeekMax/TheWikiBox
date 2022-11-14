using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateau : MonoBehaviour{
    //données a recup
    public int longeur;
    public int largeur;

    public GameObject casePrefab;
    public GameObject parent;

    //données du plateau
    int[,] plateau;
    bool[,] sources;
    bool[,] watered;
    GameObject[,] cases;

    public static Plateau instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
            return;
        }

        plateau = new int[longeur, largeur];
        cases = new GameObject[longeur, largeur];
        sources = new bool[longeur, largeur];
        watered = new bool[longeur, largeur];
    }

    public void Start(){
        //generation du plateau
        Labyrinth lab = new Labyrinth(longeur, largeur);
        lab.Generate(longeur/2, largeur/2);
        lab.RandomizeRotation();
        plateau = lab.GetGrille();

        for(int i = 0; i < longeur; i++){
            for(int j = 0; j < largeur; j++){
                //on ajoute la case du GameObject
                GameObject caseObj = Instantiate(casePrefab, parent.transform);
                caseObj.transform.position = new Vector3(i, -j, 0);
                caseObj.GetComponent<Case>().UpdateSprite(plateau[i, j]);
                cases[i, j] = caseObj;

                sources[i, j] = false;
                watered[i, j] = false;
            }
        }
        sources[0, 0] = true;
        sources[longeur - 1, largeur - 1] = true;

        //on met a jour les cases
        UpdateFlow();
    }

    public void Expend(int x, int y,int orientation){
        if(x < 0 || x >= longeur || y < 0 || y >= largeur){
            return;
        }else if(watered[x, y]){
            return;
        }
        

        int value = plateau[x, y];
        bool[] directions = new bool[4];
        
        directions[0] = value%2 == 1;
        value /= 2;
        directions[1] = value%2 == 1;
        value /= 2;
        directions[2] = value%2 == 1;
        value /= 2;
        directions[3] = value%2 == 1;

        if(!(orientation == -1) && !directions[orientation]){
            return;
        }

        watered[x, y] = true;
        
        if(directions[0]){
            Expend(x, y - 1,2);
        }
        if(directions[1]){
            Expend(x + 1, y,3);
        }
        if(directions[2]){
            Expend(x, y + 1,0);
        }
        if(directions[3]){
            Expend(x - 1, y,1);
        }
    }

    public void UpdateFlow(){
        for(int i = 0; i < longeur; i++){
            for(int j = 0; j < largeur; j++){
                watered[i, j] = false;
            }
        }
        for(int i = 0; i < longeur; i++){
            for(int j = 0; j < largeur; j++){
                if(sources[i, j]){
                    Expend(i, j,-1);
                }
            }
        }
        for(int i = 0; i < longeur; i++){
            for(int j = 0; j < largeur; j++){
                cases[i, j].GetComponent<Case>().UpdateWatered(sources[i, j], watered[i, j]);
            }
        }
    }

    public void Rotate(int x, int y){
        int value = plateau[x, y];
        int last = value/8 % 2;
        value -= last*8;
        value *= 2;
        value += last;
        plateau[x, y] = value;
        cases[x, y].GetComponent<Case>().UpdateSprite(value);
        UpdateFlow();
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        //just draw a square to show the size of the plateau
        Gizmos.DrawWireCube(new Vector3(longeur/2 + .5f,-largeur/2 -.5f ,0), new Vector3(longeur, largeur, 0));
        
        if(sources == null){
            return;
        }
        //on affiche les sources
        //et on affiche les cases qui ont été arrosées
        for(int i = 0; i < longeur; i++){
            for(int j = 0; j < largeur; j++){
                if(sources[i, j]){
                    //couleur en bleu
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(new Vector3(i+.5f, -j-.5f, 0), .2f);
                }
                if(watered[i, j]){
                    //couleur en vert
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(new Vector3(i+.5f, -j-.5f, 0), .1f);
                }
            }
        }
    }
}
