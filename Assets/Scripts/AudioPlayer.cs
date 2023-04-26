using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioPlayer : MonoBehaviour
{
    public MediaController mediaController;
    public CameraPoints camController;

    public Slider slider;
    public TextMeshProUGUI currentLabel;

    public bool isRunthrough = false;

    AudioSource audioSource;
    List<AudioClip> clipList = new List<AudioClip>();
    List<string> nameList = new List<string>();

    bool isPlaying = false;
    bool isList = false;
    int listLength = 0;
    int listIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(isPlaying){
            slider.value = audioSource.time;

            if(!audioSource.isPlaying){
                Debug.Log("Clip finished");
                isPlaying = false;
                gameObject.SetActive(false);
            }
        }

        if(isList){
            slider.value = audioSource.time;
            if(!audioSource.isPlaying){ // while clip is playing
                if(listIndex < listLength - 1){
                    listIndex++;
                    Debug.Log("Playing clip index: " + listIndex + ". List length = " + listLength);
                    audioSource.clip = clipList[listIndex];
                    currentLabel.text = nameList[listIndex];

                    slider.maxValue = audioSource.clip.length;
                    slider.value = 0;

                    audioSource.Play();
                }
                else{   // on clip end
                    if(isRunthrough){
                        camController.MoveRight();
                        mediaController.SendList();
                    }
                    else{
                        Debug.Log("List finished");
                        isList = false;
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void UpdateCurrentPlaying(AudioClip clip, string name){

        if(clip == null){
            Debug.Log("No clip found with name: " + name);
        }
        else{
            gameObject.SetActive(true);
            Debug.Log("Playing clip: " + name);
            audioSource.clip = clip;

            currentLabel.text = name;

            slider.maxValue = clip.length;
            slider.value = 0;

            isPlaying = true;
            isList = false;
            audioSource.Play(); 
        }
    }

    public void PlayList(List<AudioClip> clips, List<string> names, string name){
        gameObject.SetActive(true);

        clipList = clips;
        nameList = names;

        isList = true;
        listLength = clips.Count;
        listIndex = 0;

        Debug.Log("Playing clip list of size: " + listLength + ". Starting with: " + names[listIndex]);

        audioSource.clip  = clipList[listIndex];
        currentLabel.text = names[listIndex];

        slider.maxValue = clipList[listIndex].length;
        slider.value = 0;

        audioSource.Play();
    }
}
