using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int BonesLeft;
    public int BonesMax;

    public void LoseBones(int amount){
        if(BonesLeft >= amount){
            BonesLeft -= amount;
        } else {
            BonesLeft = 0;
            Dead();
        }
    }

    void Dead(){
        Debug.Log("Deaht not implemented");
    }
}
