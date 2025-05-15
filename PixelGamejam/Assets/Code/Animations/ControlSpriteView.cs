using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSpriteView : MonoBehaviour
{
    public List<GameObject> SpriteViews;

    BoneChoser bc;

    void Start()
    {
        bc = BoneChoser.instance;
    }

    void Update()
    {
        
        CheckForNormalBones();
    }

    void CheckForNormalBones()
    {
        bc = BoneChoser.instance;
        List<string> names = new List<string>();

        for (int i = 0; i < bc.bonesDisabled.Count; i++)
        {
            names.Add(bc.bonesDisabled[i].name);
        }

        foreach (GameObject bone in SpriteViews)
        {
            if (names.Contains(bone.name))
            {
                bone.SetActive(false);
            }
            else
            {
                bone.SetActive(true);
            }
        }

    }

    
    void CheckForMainBones()
    {
        bc = BoneChoser.instance;
        List<string> names = new List<string>();

        for (int i = 0; i < bc.bonesDisabled.Count; i++)
        {
            names.Add(bc.bonesDisabled[i].name);
        }

        if (names.Contains("LeftLeg") && names.Contains("RightLeg"))
        {
            GameObject bone = SpriteViews.Find(x => x.name == "Spleen");
            
            bone.SetActive(false);
        }
        else
        {
            GameObject bone = SpriteViews.Find(x => x.name == "Spleen");
            bone.SetActive(true);
        }


        if (names.Contains("LeftArm") && names.Contains("RightArm"))
        {
            GameObject bone = SpriteViews.Find(x => x.name == "Ribcage");
            bone.SetActive(false);
        }
        else
        {
            GameObject bone = SpriteViews.Find(x => x.name == "Ribcage");
            bone.SetActive(true);
        }
    }
}
