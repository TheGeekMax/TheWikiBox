using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour{
    public GameObject source;

    float time = 0;
    bool rotate = false;
    int pendingValue = 0;
    int rotationCount = 0;

    float maxTime = 0.5f;

    void Update(){
        if(rotate){
            //utilise lerp pour faire une rotation de 0 a 90 degres en prendant en compte uniquement time
            time += (Time.deltaTime*rotationCount);
            if(time < maxTime*rotationCount){
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -90*rotationCount), time/(maxTime*rotationCount));
            }else{
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rotate = false;
                time = 0f;
                rotationCount = 0;
                UpdateSprite(pendingValue,false);
            }
        }
    }

    public void UpdateSprite(int value, bool isSource){
        GetComponent<SpriteRenderer>().sprite = SpriteManager.instance.GetSprite(value);
        if(isSource){
            source.SetActive(true);
        }else{
            source.SetActive(false);
        }
    }

    public void UpdateWatered(bool watered){
        if(watered){
            GetComponent<SpriteRenderer>().color = Color.blue;
        }else{
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void Rotate(int value){
        if(rotate){
            rotationCount++;
            pendingValue = value;
        }else{
            rotate = true;
            rotationCount = 1;
            pendingValue = value;
            time = 0f;
        }
    }
}
