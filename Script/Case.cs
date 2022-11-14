using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour{
    public void UpdateSprite(int value){
        GetComponent<SpriteRenderer>().sprite = SpriteManager.instance.GetSprite(value);
    }

    public void UpdateWatered(bool source, bool watered){
        if(source){
            GetComponent<SpriteRenderer>().color = Color.blue;
        }else if(watered){
            GetComponent<SpriteRenderer>().color = Color.green;
        }else{
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
