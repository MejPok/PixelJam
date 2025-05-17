using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] bool grounded;
    public float notGroundedTimer;
    public Jumping jumper;
    public bool Grounded
    {
        get
        {
            return grounded;
        }

        set
        {
            if (value != grounded)
            { //Check for change
                grounded = value;

                if (value == false)
                { // that means player has just left a platform, check for coyote time
                    notGroundedTimer = 0;
                }
                else
                {
                    jumper.UseBufferedJump();
                }
            }

        }

    }

    void Update()
    {
        Grounded = IsGrounded();
        if (!Grounded)
        {
            notGroundedTimer += Time.deltaTime;
        }
        else
        {
            notGroundedTimer = 0f;
        }
    }
    public LayerMask groundLayer;
    private bool IsGrounded()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        float rayLength = 0.1f;

        // Cast straight down from the center of the bottom of the collider
        Vector2 origin = new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength, groundLayer);

        // Debug: draw the ray
        Debug.DrawRay(origin, Vector2.down * rayLength, hit.collider != null ? Color.green : Color.red);

        return hit.collider != null;
    }

}
