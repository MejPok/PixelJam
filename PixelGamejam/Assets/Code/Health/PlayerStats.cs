using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
        List<string> names = new List<string>();
        for(int i = 0; i < BoneChoser.instance.bonesDisabled.Count; i++){
            names.Add(BoneChoser.instance.bonesDisabled[i].name);
        }

        ScriptableMovementState stateChosen = null;

        if(names.Contains("Spine")){ //nema nic only skull
            stateChosen = allMovementStates[4];
            return stateChosen;
        }

        bool hasntOneArm = names.Contains("LeftArm") || names.Contains("RightArm");
        bool hasntBothArms = names.Contains("LeftArm") && names.Contains("RightArm");

        bool hasntOneLeg = names.Contains("LeftLeg") || names.Contains("RightLeg");
        bool hasntBothLegs = names.Contains("LeftLeg") && names.Contains("RightLeg");

        bool OneLeg = hasntOneLeg && !hasntBothLegs;
        bool OneArm = hasntOneArm && !hasntBothArms;

        if(!hasntOneArm && !hasntOneLeg){ // has everything
            allMovementStates[0].CanThrow = true;
            return allMovementStates[0];
        }

        if(hasntBothArms && hasntBothLegs){ // does onmly have spine
            return allMovementStates[3];
        }
        
        if(hasntBothLegs && !hasntBothArms){ // doesnt have legs but has both arms
            return allMovementStates[2];
        }

        if(hasntBothArms && !hasntOneLeg){ // doesnt have Arms but has both Legs
            stateChosen = allMovementStates[0];

            stateChosen.CanThrow = false;

            return stateChosen;
        }

        if(hasntBothArms && OneLeg){ // doesnt have Arms and has boh
            stateChosen = allMovementStates[1];

            stateChosen.CanThrow = false;

            return stateChosen;
        }

        if (!hasntBothArms && !hasntOneLeg)
        {
            allMovementStates[0].CanThrow = true;
            return allMovementStates[0];
        }

        if (OneLeg)
        { // has one leg
            if (OneArm || !hasntBothArms)
            {
                allMovementStates[1].CanThrow = true;
            }
            else
            {
                allMovementStates[1].CanThrow = false;
            }
            return allMovementStates[1];
        }

        if(OneArm){ // has one arm
            allMovementStates[0].CanThrow = true;
            return allMovementStates[0];
        }


       return allMovementStates[0];
    }
}
