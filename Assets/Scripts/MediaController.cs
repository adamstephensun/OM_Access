using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MediaController : MonoBehaviour
{
    [Header("GameObject references")]
    public CameraPoints camPoints;
    public BSLController bslController;
    public AudioPlayer audioPlayer;
    public AudioSource[] audioSources;

    [Header("Controls UI")]
    public GameObject aControls;
    public GameObject bControls;
    public GameObject iControls;
    public GameObject qControls;

    [Header("Clips")]
    public List<AudioClip> clipList = new List<AudioClip>();
    public List<string> clipNames = new List<string>();

    [Header("Vars")]
    [Range(0f,1f)] public float lowVolume;
    [Range(0f,1f)] public float highVolume;

    AssetManager assets;

    int currentIndex;       // Current position in space (0 = entrance, 1 = Khush, 2 = Les, etc.)
    bool runthroughMode = false;
    bool bslEnabled = true;

    public string currentName;
    public AudioClip currentInfo;
    public AudioClip currentDescription;
    public AudioClip currentQA;
    public AudioClip currentBio;

    void Start(){
        assets = GetComponent<AssetManager>();
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.R)){
            // full run-through mode
            //runthroughMode = true;
            //audioPlayer.isRunthrough = runthroughMode;
            //SendList();
        }

        if(!runthroughMode){        // normal keybinds for when not in run-through mode
            if(Input.GetKeyDown(KeyCode.Space)){
                // Play audio description, info board recording, and artist Q&A in sequence on current index
                SendList();
            }
            if(Input.GetKeyDown(KeyCode.A)){
                // Play audio description
                audioPlayer.UpdateCurrentPlaying(currentDescription, currentName + " audio description");
            }
            if(Input.GetKeyDown(KeyCode.B)){
                // Play bio recording
                audioPlayer.UpdateCurrentPlaying(currentBio, currentName + " bio");
            }
            if(Input.GetKeyDown(KeyCode.I)){
                // Play info board recording
                audioPlayer.UpdateCurrentPlaying(currentInfo, currentName + " info board");
            }
        }
        
    }

    public void MovedIndex(){
        Debug.Log("Moved index");
        // called when arrow key is pressed and current index is changed in CameraPoints.cs
        currentIndex = camPoints.currentPosIndex;

        // update current media from asset library
        currentName = assets.assetList[currentIndex].name;
        currentInfo = assets.assetList[currentIndex].infoBoardRec;
        currentDescription = assets.assetList[currentIndex].audioDescRec;
        currentQA = assets.assetList[currentIndex].QandARec;
        currentBio = assets.assetList[currentIndex].bio;

        if(currentInfo == null) iControls.SetActive(false);
        else iControls.SetActive(true);
        if(currentDescription == null) aControls.SetActive(false);
        else aControls.SetActive(true);
        if(currentQA == null) qControls.SetActive(false);
        else qControls.SetActive(true);
        if(currentBio == null) bControls.SetActive(false);
        else bControls.SetActive(true);

        lowerAllVideoSources();
        increaseCurrentSource(); // then set the current source to high volume

        bslController.ChangeBSLVideo(currentIndex);
    }

    public void SendList(){
        clipList.Clear();
        clipNames.Clear();

        if(currentBio != null){
            clipList.Add(currentBio);
            clipNames.Add(currentName + " bio");
        }
        if(currentInfo != null){
            clipList.Add(currentInfo);
            clipNames.Add(currentName + " info board");
        }
        if(currentDescription != null){
            clipList.Add(currentDescription);
            clipNames.Add(currentName + " audio description");
        } 
        if(currentQA != null){
            clipList.Add(currentQA);
            clipNames.Add(currentName + " Q & A");
        }
        audioPlayer.PlayList(clipList, clipNames, currentName);
    }

    void lowerAllVideoSources(){
        foreach(AudioSource s in audioSources){     //Set all audio sources to low volume
            if(s != null) s.volume = lowVolume;
        }
    }

    void increaseCurrentSource(){
        if(audioSources[currentIndex] != null) audioSources[currentIndex].volume = highVolume; //set the current source to high volume
    }
}
