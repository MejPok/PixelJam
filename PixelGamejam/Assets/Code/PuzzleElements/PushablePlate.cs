using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushablePlate : MonoBehaviour
{
    public bool isPushable;

    public float TimeToPressDown;

    public int WeightNeeded;

    public GameObject button;
    public Vector2 WhereToMove;

    float timer;

    Vector2 startPosition;

    public bool keepDown;

    public GameObject whatToMove;


    public Vector2 WhereToMoveDoor;
    public Vector2 StartDoor;
    void Start()
    {
        startPosition = button.transform.position;
        StartDoor = whatToMove.transform.position;
    }

    void Update()
    {
        if(!somethingStepping && !keepDown){
            if(timer >= 0){
                timer -= Time.deltaTime;
                if(timer <= 0){
                    timer = 0;
                }
                float dif = timer / TimeToPressDown;
                button.transform.position = new Vector2(startPosition.x + dif * WhereToMove.x, startPosition.y + dif * WhereToMove.y);

                whatToMove.transform.position = new Vector2(StartDoor.x + dif * WhereToMoveDoor.x, StartDoor.y + dif * WhereToMoveDoor.y);
                Debug.Log("down");
            }
            
        }
        
    }
    bool somethingStepping;

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bone")) {
            somethingStepping = true;
            timer += Time.deltaTime;
            if(timer >= TimeToPressDown){
                timer = TimeToPressDown;
            }
            float dif = timer / TimeToPressDown;

            button.transform.position = new Vector2(startPosition.x + (dif * WhereToMove.x), startPosition.y + (dif * WhereToMove.y));

            whatToMove.transform.position = new Vector2(StartDoor.x + dif * WhereToMoveDoor.x, StartDoor.y + dif * WhereToMoveDoor.y);
            Debug.Log("up");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bone")){
            somethingStepping = false;
        }
    }

}
