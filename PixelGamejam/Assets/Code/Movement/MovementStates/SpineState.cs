using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SpineState")]
public class SpineState : ScriptableMovementState
{
    public override void Jump(){
        
    }

    public override void Move(float speed)
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //Important!
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y); 
    }
    
}
