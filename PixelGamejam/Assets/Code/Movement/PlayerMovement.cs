using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Jumping))] 
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    Vector2 baseScale; //starting relative scale, so that it can proeprly rotate the sprite

    public ScriptableMovementState[] movementStates; //container for the states

    private void Awake()
    {
        baseScale = transform.localScale;
        
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        RotateSprite();
    }

    void Move(){
        ScriptableMovementState state = movementStates[0];
        state.rb = rb;
        state.Move(speed);
    }

    void RotateSprite(){
        float horizontalInput = Input.GetAxis("Horizontal"); //Important!! add new input for controller inputs

        //flips player sprite when moving, now using relative scale instead of fixed one
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector2(baseScale.x, baseScale.y);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector2(-baseScale.x, baseScale.y);

    }

}
