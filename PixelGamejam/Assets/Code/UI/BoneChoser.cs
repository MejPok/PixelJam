using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoneChoser : MonoBehaviour
{
    public GameObject chosenButton;

    Image lastImageChosen;
    public string boneChosen;

    public GameObject[] boneButtons;
    public List<GameObject> bonesDisabled;
    void Start()
    {
        bonesDisabled = new List<GameObject>();
    }
    
    public void GetThisBone(string bone, GameObject gm){

        if(lastImageChosen != null){
            lastImageChosen.color = Color.white;
        }

        boneChosen = bone;
        chosenButton = gm;
        gm.GetComponent<Image>().color = Color.black;

        lastImageChosen = gm.GetComponent<Image>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){

            if(lastImageChosen != null){
                if(boneChosen != "Skull"){
                    chosenButton.SetActive(false);
                    bonesDisabled.Add(chosenButton);
                    PutDownTheBone();
                    lastImageChosen = null;
                }
            }

        } else if( Input.GetKeyDown(KeyCode.R) ){

            if(lastImageChosen != null){
                if(boneChosen != "Skull"){
                    chosenButton.SetActive(false);
                    bonesDisabled.Add(chosenButton);
                    ThrowTheBone();
                    lastImageChosen = null;
                }
            }

        }
    }

    void PutDownTheBone(){
        
    }

    void ThrowTheBone(){

    }
}
