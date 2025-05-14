using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneData : MonoBehaviour
{
    public Sprite sprite;
    public string BoneName;

    public void CreateBone(string bone, Sprite sprite){
        BoneName = bone;
        gameObject.name = BoneName;

        this.sprite = sprite;
        GetComponent<SpriteRenderer>().sprite = this.sprite;
    }

}
