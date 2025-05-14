using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoneChoser : MonoBehaviour
{
    public static BoneChoser instance;
    public GameObject chosenButton;

    Image lastImageChosen;
    public string boneChosen;

    public GameObject[] boneButtons;
    public List<GameObject> bonesDisabled;
    void Start()
    {
        instance = this;
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
        ThrowingBones.tb.bc = this;

        if(Input.GetKeyDown(KeyCode.E)){
            if(lastImageChosen != null){
                if(boneChosen != "Skull"){
                    chosenButton.SetActive(false);
                    bonesDisabled.Add(chosenButton);
                    ThrowingBones.tb.PutDownTheBone(boneChosen, chosenButton.GetComponent<Image>().sprite);
                    lastImageChosen = null;
                }
            }

        } else if( Input.GetKeyDown(KeyCode.R) ){

            if(lastImageChosen != null){
                if(boneChosen != "Skull"){
                    chosenButton.SetActive(false);
                    bonesDisabled.Add(chosenButton);
                    lastImageChosen = null;

                    if(PlayerMovement.pm.movementState.CanThrow){
                        ThrowingBones.tb.ThrowTheBone(boneChosen, chosenButton.GetComponent<Image>().sprite);
                    } else {
                        ThrowingBones.tb.PutDownTheBone(boneChosen, chosenButton.GetComponent<Image>().sprite);
                    }
                }
            }

        }
        
        CheckForMainBones();

    }

    void CheckForMainBones(){
        List<string> names = new List<string>();
        for(int i = 0; i < bonesDisabled.Count; i++){
            names.Add(bonesDisabled[i].name);
        }

        if(names.Contains("LeftLeg") && names.Contains("RightLeg")){
            GameObject bone = boneButtons.ToList().Find(x => x.name == "Spleen");
            bone.SetActive(false);
        } else {
            GameObject bone = boneButtons.ToList().Find(x => x.name == "Spleen");
            bone.SetActive(true);
        }


        if(names.Contains("LeftArm") && names.Contains("RightArm")){
            GameObject bone = boneButtons.ToList().Find(x => x.name == "Ribcage");
            bone.SetActive(false);
        } else {
            GameObject bone = boneButtons.ToList().Find(x => x.name == "Ribcage");
            bone.SetActive(true);
        }
    }


}
