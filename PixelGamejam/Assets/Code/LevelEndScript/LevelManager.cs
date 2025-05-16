using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public int currentRoomOnLevel = 0;

    public ScriptableRoom[] roomsInScene;
    public ScriptableRoom roomActive;

    public GameObject activeMap;

    public GameObject Player;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadRoom();
    }

    public void Reload()
    {
        if (activeMap != null)
        {
            Destroy(activeMap);
        }

        roomActive = roomsInScene[currentRoomOnLevel - 1];

        BoneChoser.instance.RemoveTheseBones(roomActive.bonesRequiredRemoved);

        roomActive.StartGame(Player);
        activeMap = roomActive.activeMap;
        
    }

    public void LoadRoom()
    {
        if (currentRoomOnLevel >= roomsInScene.Length)
        {
            Debug.Log("Not enough rooms for that");
            return;
        }
        if (activeMap != null)
        {
            Destroy(activeMap);
        }

        roomActive = roomsInScene[currentRoomOnLevel];

        BoneChoser.instance.RemoveTheseBones(roomActive.bonesRequiredRemoved);

        roomActive.StartGame(Player);
        activeMap = roomActive.activeMap;

        currentRoomOnLevel += 1;
    }

    void NextLevel() 
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void LoadScene(string sceneName) 
    {
        SceneManager.LoadSceneAsync(sceneName);   
    }
}
