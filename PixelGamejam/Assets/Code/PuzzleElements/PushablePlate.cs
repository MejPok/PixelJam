using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PushablePlate : MonoBehaviour
{
    [Header("States")]
    public bool isPushable;
    public bool keepDown;

    [Header("References")]
    public GameObject button;
    public GameObject whatToMove;

    [Header("New Positions")]
    public Vector2 WhereToMoveButton;
    public Vector2 WhereToMoveDoor;

    [Header("Attributes")]
    public int WeightNeeded;
    public float TimeToPressDown;






    float timer;
    Vector2 startPosition;
    Vector2 StartDoor;
    void Start()
    {
        startPosition = button.transform.position;
        StartDoor = whatToMove.transform.position;
    }
    float FXtimer;
    bool FXwait;

    void Update()
    {
        for (int i = 0; i < bonesStaying.Count; i++)
        {
            if (bonesStaying[i] == null) {
                bonesStaying.RemoveAt(i);
            }

        }
        
        if (AppliedWeight >= WeightNeeded)
        {
               timer += Time.deltaTime; 
        }
        if (timer >= TimeToPressDown)
        {
            timer = TimeToPressDown;
            FXtimer += Time.deltaTime;
            if (FXtimer >= 5f)
            {
                FXtimer = 0f;
                FXwait = false;
            }
            if (!FXwait)
            {
                FXwait = true;
                SoundManager.Instance.PlaySoundFX(GetComponent<FXchoser>().audioClips[0], transform, 0.3f);
            }
                
        }

        if (playerIsOn)
        {
            AppliedWeight = bonesStaying.Count + BoneChoser.instance.CalculatePlayerWeight();
        }
        else
        {
            AppliedWeight = bonesStaying.Count;
        }

        if (!isPushable)
        {
            return;
        }
        
        if (AppliedWeight < WeightNeeded && !keepDown)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0;
                }


                SetNewButtonPosition();
                SetNewDoorPosition();
            }

        }
        
    }
    bool somethingStepping;

    List<GameObject> bonesStaying = new List<GameObject>();

    bool playerIsOn;
    public int AppliedWeight;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bone"))
        {
            
            if (other.gameObject.name == "Area" && other.gameObject.CompareTag("Bone") && !bonesStaying.Contains(other.gameObject))
            {
                bonesStaying.Add(other.gameObject);
            }

            somethingStepping = true;


            if (other.gameObject.CompareTag("Player"))     playerIsOn = true;
            

            if (timer >= TimeToPressDown)
            {
                timer = TimeToPressDown;
            }

            if (playerIsOn)
            {
                AppliedWeight = bonesStaying.Count + BoneChoser.instance.CalculatePlayerWeight();
            }
            else
            {
                AppliedWeight = bonesStaying.Count;
            }

            if (AppliedWeight >= WeightNeeded)
            {
                SetNewButtonPosition();
                SetNewDoorPosition();
            }
            
        }
    }

    void SetNewButtonPosition()
    {
        float dif = timer / TimeToPressDown;
        

        var NewButtonPosition = new Vector2(startPosition.x + (dif * WhereToMoveButton.x), startPosition.y + (dif * WhereToMoveButton.y));
        button.transform.position = NewButtonPosition;
    }

    void SetNewDoorPosition()
    {
        float dif = timer / TimeToPressDown;

        var NewDoorPosition = new Vector2(StartDoor.x + dif * WhereToMoveDoor.x, StartDoor.y + dif * WhereToMoveDoor.y);
        whatToMove.transform.position = NewDoorPosition;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bone"))
        {
            somethingStepping = false;
            if (bonesStaying.Contains(other.gameObject))
            {
                bonesStaying.Remove(other.gameObject);
            }

            if (other.gameObject.CompareTag("Player"))
            {
                playerIsOn = false;
            }
        }
    }

}
