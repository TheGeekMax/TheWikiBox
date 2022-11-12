using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] sprites;
    public static SpriteManager instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    public Sprite GetSprite(int index){
        if (index < 0 || index >= sprites.Length){
            Debug.LogError("SpriteManager: index out of range -> " + index);
            return null;
        }
        return sprites[index];
    }


}
