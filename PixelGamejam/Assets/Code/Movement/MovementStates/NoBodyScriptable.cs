using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NoBody")]
public class NoBodyScriptable : ScriptableMovementState
{
    public float maxRollSpeed;
    public override void Jump(){
        return;
    }

    public override void Move(float speed){
        float horizontalInput = Input.GetAxis("Horizontal"); //Important!! add new input for controller inputs
        Vector2 force = new Vector2(horizontalInput * speed, 0);
        if(Math.Abs(rb.velocity.x) < maxRollSpeed){
            rb.AddForce(force);
        }
        
    }
}
