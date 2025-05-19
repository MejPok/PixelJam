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

    void Awake()
    {
        instance = this;
        bonesDisabled = new List<GameObject>();
    }
    void Start()
    {

    }

    public void GetThisBone(string bone, GameObject gm)
    {

        if (lastImageChosen != null)
        {
            lastImageChosen.color = Color.white;
        }

        boneChosen = bone;
        chosenButton = gm;
        gm.GetComponent<Image>().color = Color.black;

        lastImageChosen = gm.GetComponent<Image>();
    }

    bool TryPutDown()
    {
        List<string> names = new List<string>();
        for (int i = 0; i < bonesDisabled.Count; i++)
        {
            names.Add(bonesDisabled[i].name);
        }

        if (Input.GetKeyDown(KeyCode.E) || fakePutdown)
        {
            if (fakePutdown)
            {
                fakePutdown = false;
            }
            if (lastImageChosen != null)
            {
                if (boneChosen != "Skull")
                {
                    if (boneChosen == "Spine")
                    {
                        foreach (GameObject bone in boneButtons)
                        {
                            if (!names.Contains(bone.name) && bone.name != "Skull" && bone.name != "Ribcage" && bone.name != "Spleen")
                            {
                                bonesDisabled.Add(bone);
                                bone.SetActive(false);

                                ThrowingBones.tb.PutDownTheBone(bone.name, bone.GetComponent<Image>().sprite);
                                lastImageChosen = null;
                                chosenButton = null;
                                boneChosen = null;
                                boneButtons.ToList().Find(x => x.name == bone.name).GetComponent<Image>().color = Color.white;
                            }
                        }

                        if (chosenButton != null)
                        {
                            ResetStates();
                        }
                        else
                        {
                            lastImageChosen = null;
                            chosenButton = null;
                            boneChosen = null;
                        }
                        boneButtons.ToList().Find(x => x.name == "Spine").GetComponent<Image>().color = Color.white;
                        return true;
                    }

                    chosenButton.SetActive(false);
                    bonesDisabled.Add(chosenButton);
                    ThrowingBones.tb.PutDownTheBone(boneChosen, chosenButton.GetComponent<Image>().sprite);
                    ResetStates();
                    return true;
                }
            }

        }
        return false;
    }

    void TryThrow()
    {
        List<string> names = new List<string>();
        for (int i = 0; i < bonesDisabled.Count; i++)
        {
            names.Add(bonesDisabled[i].name);
        }

        if (Input.GetKeyDown(KeyCode.R) || fakeThrow)
        {
            if (fakeThrow)
            {
                fakeThrow = false;
            }

            if (lastImageChosen != null)
            {
                if (boneChosen != "Skull")
                {
                    if (boneChosen == "Spine")
                    {
                        foreach (GameObject bone in boneButtons)
                        {
                            if (!names.Contains(bone.name) && bone.name != "Skull" && bone.name != "Ribcage" && bone.name != "Spleen")
                            {
                                bonesDisabled.Add(bone);
                                bone.SetActive(false);

                                if (PlayerMovement.pm.movementState.CanThrow)
                                {
                                    ThrowingBones.tb.ThrowTheBone(bone.name, bone.GetComponent<Image>().sprite);
                                    
                                    lastImageChosen = null;
                                    chosenButton = null;
                                    boneChosen = null;
                                }
                                else
                                {
                                    ThrowingBones.tb.PutDownTheBone(bone.name, bone.GetComponent<Image>().sprite);
                                    
                                    lastImageChosen = null;
                                    chosenButton = null;
                                    boneChosen = null;
                                }

                                boneButtons.ToList().Find(x => x.name == bone.name).GetComponent<Image>().color = Color.white;
                            }
                        }

                        if (chosenButton != null)
                        {
                            ResetStates();
                        }
                        else
                        {
                            
                            lastImageChosen = null;
                            chosenButton = null;
                            boneChosen = null;
                        }

                        boneButtons.ToList().Find(x => x.name == "Spine").GetComponent<Image>().color = Color.white;
                        return;
                    }

                    chosenButton.SetActive(false);
                    chosenButton.GetComponent<Image>().color = Color.white;
                    bonesDisabled.Add(chosenButton);
                    lastImageChosen = null;

                    if (PlayerMovement.pm.movementState.CanThrow)
                    {
                        ThrowingBones.tb.ThrowTheBone(boneChosen, chosenButton.GetComponent<Image>().sprite);
                        ResetStates();
                    }
                    else
                    {
                        ThrowingBones.tb.PutDownTheBone(boneChosen, chosenButton.GetComponent<Image>().sprite);
                        ResetStates();
                    }
                }
            }

        }
    }

    void ResetStates()
    {
        chosenButton.GetComponent<Image>().color = Color.white;
        lastImageChosen = null;
        chosenButton = null;
        boneChosen = null;
    }



    void Update()
    {
        ThrowingBones.tb.bc = this;

        if (!TryPutDown())
        {
            TryThrow();
        }

        foreach (GameObject bone in bonesDisabled)
        {
            Debug.Log("" + bone.name);
        }

        CheckForMainBones();
        SetCollidersCorrect();

        if (cantPickUp)
        {
            timer += Time.deltaTime;
            if (timer >= 0.01f)
            {
                cantPickUp = false;
                timer = 0;
            }
        }

    }
    bool fakeThrow;
    bool fakePutdown;
    public void ImitateThrow()
    {
        fakeThrow = true;
    }
    public void ImitatePutDown()
    {
        fakePutdown = true;
    }

    void CheckForMainBones()
    {
        List<string> names = new List<string>();
        for (int i = 0; i < bonesDisabled.Count; i++)
        {
            names.Add(bonesDisabled[i].name);
        }

        if (names.Contains("LeftLeg") && names.Contains("RightLeg"))
        {
            GameObject bone = boneButtons.ToList().Find(x => x.name == "Spleen");
            bone.SetActive(false);

            if (!names.Contains(bone.name))
            {
                bonesDisabled.Add(bone);
            }

        }
        else
        {
            GameObject bone = boneButtons.ToList().Find(x => x.name == "Spleen");
            bone.SetActive(true);
            if (names.Contains(bone.name))
            {
                bonesDisabled.Remove(bone);
            }

        }


        if (names.Contains("LeftArm") && names.Contains("RightArm"))
        {
            GameObject bone = boneButtons.ToList().Find(x => x.name == "Ribcage");
            bone.SetActive(false);
            if (!names.Contains(bone.name))
            {
                bonesDisabled.Add(bone);
            }

        }
        else
        {
            GameObject bone = boneButtons.ToList().Find(x => x.name == "Ribcage");
            bone.SetActive(true);
            if (names.Contains(bone.name))
            {
                bonesDisabled.Remove(bone);
            }
        }
    }

    float timer;
    bool cantPickUp;

    public void GetBoneBack(string name, GameObject sender)
    {
        if (cantPickUp)
        {
            Debug.Log("Cant pick up bones because for cooldown");
            return;
        }
        List<string> names = new List<string>();
        for (int i = 0; i < BoneChoser.instance.bonesDisabled.Count; i++)
        {
            names.Add(BoneChoser.instance.bonesDisabled[i].name);
        }

        if (names.Contains(name))
        {
            if (names.Contains("Spine") && name != "Spine")
            {
                Debug.Log("Cant allow the joinment of " + name + " because we dont have spine");
                return;
            }

            foreach (GameObject button in boneButtons)
            {
                if (button.name == name)
                {
                    LevelManager.instance.Player.transform.Translate(new Vector2(0, 1f));
                    cantPickUp = true;
                    bonesDisabled.Remove(button);
                    button.SetActive(true);
                    Destroy(sender);
                    return;
                }
            }
        }
    }

    public void RemoveTheseBones(string[] bonesToRemove)
    {
        GetAllBonesBack();
        Debug.Log("Trying to delete bones for level");
        List<string> names = new List<string>();
        for (int i = 0; i < bonesToRemove.Length; i++)
        {
            names.Add(bonesToRemove[i]);
            
        }

        foreach (GameObject bone in boneButtons)
        {
            if (names.Contains(bone.name))
            {
                bonesDisabled.Add(bone);
                bone.SetActive(false);
                Debug.Log("Removed bone for level, " + bone.name);

                lastImageChosen = null;
                chosenButton = null;
                boneChosen = null;
            }

        }

    }

    void GetAllBonesBack()
    {
        List<GameObject> tester = new List<GameObject>();
        foreach (GameObject bone in bonesDisabled)
        {
            tester.Add(bone);
        }

        foreach (GameObject bone in tester)
        {
            bonesDisabled.Remove(bone);
            bone.SetActive(true);

            lastImageChosen = null;
            chosenButton = null;
            boneChosen = null;
        }
    }

    public int CalculatePlayerWeight()
    {
        var CountableBones = new List<GameObject>();
        foreach (GameObject bone in boneButtons)
        {
            if (bone.name != "Spleen" && bone.name != "Ribcage")
                CountableBones.Add(bone);
        }

        foreach (GameObject bone in bonesDisabled)
        {
            if (CountableBones.Contains(bone))
            {
                CountableBones.Remove(bone);
            }
        }

        return CountableBones.Count;

    }

    public GameObject Arms;
    public GameObject Legs;
    
    void SetCollidersCorrect()
    {
        List<string> names = new List<string>();
        for (int i = 0; i < bonesDisabled.Count; i++)
        {
            names.Add(bonesDisabled[i].name);
        }

        Legs.SetActive(true);
        Arms.SetActive(true);

        if (names.Contains("RightLeg") && names.Contains("LeftLeg"))
        {
            Legs.SetActive(false);
        }


        if (names.Contains("RightArm") && names.Contains("LeftArm"))
        {
            Arms.SetActive(false);
        }
    }
}


