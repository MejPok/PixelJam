using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats ps;
    public int BonesLeft;
    public int BonesMax;
    Health health;

    public ScriptableMovementState[] allMovementStates;

    void Start()
    {
        ps = this;
        health = GetComponent<Health>();
    }

    void Update()
    {
        BonesLeft = health.BonesLeft;
        BonesMax = health.BonesMax;
    }

    public ScriptableMovementState chooseCorrectState(){
        if(BonesLeft > 17){
            return allMovementStates[0];
        } 
        if(BonesLeft > 14){
            return allMovementStates[1];
        }
        if(BonesLeft > 10){
            return allMovementStates[2];
        }
        if(BonesLeft > 5){
            return allMovementStates[3];
        }
        if(BonesLeft >= 0){
            return allMovementStates[4];
        }
        return allMovementStates[1];
    }
}
