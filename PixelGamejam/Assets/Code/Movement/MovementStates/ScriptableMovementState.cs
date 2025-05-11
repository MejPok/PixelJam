using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableMovementState : ScriptableObject
{
    public Rigidbody2D rb;

    public float JumpForce;
    public float JumpMultiplier;
    public bool CanJump;
    public bool CanMove;
    public bool CanThrow;
    public virtual void Jump(){

    }

    public virtual void Move(float speed){

    }
    public virtual void Move(){

    }
    

}
