using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    void Awake() { instance = this; }


    //Sound effects
    public AudioClip sfx_landing;
    //Music
    public AudioClip music_tiktok;

    //Sound Object
    public GameObject soundObject;

    public void PlaySFX(string sfxName)
    {
        switch(sfxName)
        {
            case "landing":
                SoundObjectCreation(sfx_landing);
                break;
            default:
                break;
        }

    }

    void SoundObjectCreation(AudioClip clip)
    {
        //create soundsObject gameobject
        GameObject newObject = Instantiate(soundObject, transform);

        //assign audioclip to its audiosource
        newObject.GetComponent<AudioSource>().clip = clip;

        //play audio
        newObject.GetComponent<AudioSource>().Play();
    }


    public void PlayMusic(string musicName)
    {
        switch(musicName)
        {
            case "tiktok":
                MusicObjectCreation(music_tiktok);
                break;
            default:
                break;
        }

    }

    void MusicObjectCreation(AudioClip clip)
    {
        //create soundsObject gameobject
        GameObject newObject = Instantiate(soundObject, transform);

        //assign audioclip to its audiosource
        newObject.GetComponent<AudioSource>().clip = clip;
        //make the audio source looping
        newObject.GetComponent<AudioSource>().loop = true;

        //play audio
        newObject.GetComponent<AudioSource>().Play();
    }
}
