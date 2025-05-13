using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set;}
    public AudioSource soundFXobject;

    public AudioClip[] audioClipsSaved;
    void Awake()
    {
        Instance = this;
    }

    public void PlaySoundFX(AudioClip audio, Transform trans, float volume){
        var soundFX = Instantiate(soundFXobject, trans.position, Quaternion.identity);

        soundFX.clip = audio;
        soundFX.Play();
        soundFX.volume = volume;

        float clipLength = soundFX.clip.length;

        Destroy(soundFX.gameObject, clipLength);
    }

    public void PlaySoundFXnoAmmo(){
        PlaySoundFX(audioClipsSaved[0], transform, 1);
    }
}

