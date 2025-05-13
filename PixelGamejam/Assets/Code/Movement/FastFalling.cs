using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FastFalling : MonoBehaviour
{
    public float multiplier;
    public float timer;
    public bool falling;
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Jumping jumper = GetComponent<Jumping>();

        if(jumper.groundCheck.Grounded == false){
            falling = true;
            timer += Time.deltaTime;

            rb.gravityScale = Mathf.Max(1, timer * multiplier);
        } else {
            falling = false;
            timer = 0;

            rb.gravityScale = 1;
        }
        
    }
}
