using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateau : MonoBehaviour{
    public int longeur;
    public int largeur;

    public GameObject casePrefab;
    public GameObject parent;

    int[,] plateau;
    GameObject[,] cases;

    public static Plateau instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }

        plateau = new int[longeur, largeur];
        cases = new GameObject[longeur, largeur];
        for(int i = 0; i < longeur; i++){
            for(int j = 0; j < largeur; j++){
                plateau[i, j] = Random.Range(0, 16);
                //on ajoute la case du GameObject
                GameObject caseObj = Instantiate(casePrefab, parent.transform);
                caseObj.transform.position = new Vector3(i, -j, 0);
                caseObj.GetComponent<Case>().UpdateSprite(plateau[i, j]);
                cases[i, j] = caseObj;
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
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        //just draw a square to show the size of the plateau
        Gizmos.DrawWireCube(new Vector3(longeur/2 + .5f,-largeur/2 -.5f ,0), new Vector3(longeur, largeur, 0));
    }
}
