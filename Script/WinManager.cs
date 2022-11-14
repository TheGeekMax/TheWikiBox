using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour{

    void Update(){
        bool win = true;
        for(int i = 0; i < Plateau.instance.longeur; i++){
            for(int j = 0; j < Plateau.instance.largeur; j++){
                if(!Plateau.instance.watered[i, j]){
                    win = false;
                }
            }
        }
        if(win){
            Plateau.instance.Win();
        }
    }
}
