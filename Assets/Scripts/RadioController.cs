using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    public AudioSource cassetteSource;

    public List<AudioClip> clips = new List<AudioClip>();
    
    AudioSource radioSource;

    int clipNum;
    int previouslyPlayed = 999;

    bool clipStarted = false;

    void Start()
    {
        clipNum = clips.Count;
        radioSource = GetComponent<AudioSource>();

        PlayRandomClip();
    }

    void Update()
    {
        cassetteSource.volume = radioSource.volume;

        if(clipStarted){
            if(!radioSource.isPlaying){     // clip ended
                Debug.Log("Ben radio clip ended.");

                clipStarted = false;
                PlayRandomClip();
            }
        }
    }

    void PlayRandomClip(){
        int randToPlay = Random.Range(0, clipNum);
        if(randToPlay == previouslyPlayed) randToPlay = Random.Range(0, clipNum); // if same as previous, random again

        if(clips[randToPlay] != null){
            radioSource.clip = clips[randToPlay];
            radioSource.Play();
        } 
        else Debug.Log("Clip num: " + randToPlay + " doesnt exist.");
        
        clipStarted = true;
    }
}
