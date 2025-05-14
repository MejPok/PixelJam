using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] bool grounded;
    public float notGroundedTimer;
    public Jumping jumper;
    public bool Grounded { 
        get { 
            return grounded; 
        } 

        set{
            if(value != grounded) { //Check for change
                grounded = value;
                
                if(value == false){ // that means player has just left a platform, check for coyote time
                    notGroundedTimer = 0;
                } else {
                    jumper.UseBufferedJump();
                }
            }
            
        }

        }

    void Update()
    {
        Grounded = isGrounded();
        if(!grounded){
            notGroundedTimer += Time.deltaTime;
        } else {
            notGroundedTimer = 0f;
        }
    }
    public LayerMask groundLayer;
    private bool isGrounded()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        return raycastHit.collider != null;
    }
}
