using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour{
    public void UpdateSprite(int value){
        GetComponent<SpriteRenderer>().sprite = SpriteManager.instance.GetSprite(value);
    }
}
