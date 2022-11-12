using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    //touch management for mobile
    void Update(){
        UnityEngine.Input.multiTouchEnabled = false;

        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began){
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                int x = (int)touchPos.x;
                int y = (int)-touchPos.y;
                if(x >= 0 && x < Plateau.instance.longeur && y >= 0 && y < Plateau.instance.largeur){
                    Plateau.instance.Rotate(x,y);
                }
            }
        }
    }
}
