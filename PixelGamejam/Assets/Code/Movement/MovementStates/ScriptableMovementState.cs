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

    public bool isGrounded;
    public float maxRollSpeed;

    public Jumping jumper;
    public virtual void Jump()
    {

    }

    public virtual void Move(float speed)
    {

    }
    public virtual void Move()
    {

    }

    public AudioClip audioClip;
    public Transform transform;
    public virtual void PlayJumpSound()
    {
        if (CanJump)
        {
            AudioClip clip = audioClip;
            SoundManager.Instance.PlaySoundFX(clip, transform, 1);

        }
    }

    public virtual void Update()
    {

    }

    int whichOne = 0;
    float timer = 0;
    public virtual void PlayMoveSound()
    {
        
        
        timer += Time.deltaTime;
        
        if (!isGrounded)
        {
            return;
        }

        if (timer >= 0.43f)
        {
            AudioClip clip = (whichOne % 2 == 0) ? PlayerMovement.pm.gameObject.GetComponent<FXchoser>().audioClips[1] : PlayerMovement.pm.gameObject.GetComponent<FXchoser>().audioClips[2];
            SoundManager.Instance.PlaySoundFX(clip, PlayerMovement.pm.gameObject.transform, 0.1f);
            timer = 0;
        }
            

        
    }

}
