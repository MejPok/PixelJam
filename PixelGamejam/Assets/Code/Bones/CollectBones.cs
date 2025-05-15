using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBones : MonoBehaviour
{
    bool fakePickup;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bone")
        {
            if (Input.GetKeyDown(KeyCode.F) || fakePickup)
            {
                if (fakePickup)
                {
                    fakePickup = false;
                }

                List<string> names = new List<string>();
                for (int i = 0; i < BoneChoser.instance.bonesDisabled.Count; i++)
                {
                    names.Add(BoneChoser.instance.bonesDisabled[i].name);
                }

                if (names.Contains(other.gameObject.name))
                {
                    BoneChoser.instance.GetBoneBack(other.gameObject.name, other.gameObject);
                }
            }
        }
    }

    public void ImitateKeyCode()
    {
        fakePickup = true;
    }
}
