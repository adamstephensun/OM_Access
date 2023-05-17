using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class BSLController : MonoBehaviour
{
    VideoPlayer BSL_player;
    RawImage image;

    bool isPlaying = false;
    bool playWhenReady = false;

    public GameObject hideBSLButton;

    [Header("BSL video URLs")]
    public string introPath;
    public string khushPath;
    public string lesPath;
    public string martinPath;
    public string chrisPath;
    public string benPath;

    void Start()
    {
        BSL_player = GetComponent<VideoPlayer>();
        image = GetComponent<RawImage>();
        HideBSL();
    }

    void Update(){
        if(playWhenReady && BSL_player.isPrepared){     // waiting for the video player to be prepared then play
            Debug.Log("BSL ready");
            ShowBSL();
            BSL_player.Play();
            playWhenReady = false;
            isPlaying = true;
        }

        if(isPlaying){
            if(!BSL_player.isPlaying){
                Debug.Log("BSL video finished");
                HideBSL();
                isPlaying = false;
            }
        }
    }

    public void ChangeBSLVideo(int index){
        switch(index){
            case 0:
                //intro
                BSL_player.url = introPath;
                //Debug.Log("BSL video changed to Intro");
                break;
            case 1:
                //khush
                BSL_player.url = khushPath;
                //Debug.Log("BSL video changed to Khush");
                break;
            case 2:
                //les
                BSL_player.url = lesPath;
                //Debug.Log("BSL video changed to Les");
                break;
            case 3:
                //martin
                BSL_player.url = martinPath;
                //Debug.Log("BSL video changed to Martin");
                break;
            case 4:
                //chris
                BSL_player.url = chrisPath;
                //Debug.Log("BSL video changed to Chris");
                break;
            case 5:
                //ben
                BSL_player.url = benPath;
                //Debug.Log("BSL video changed to Ben");
                break;
            default:
                Debug.Log("No BSL video available with index: " + index);
                break;
        }
        
        BSL_player.Prepare();
        playWhenReady = true;
    }

    public void HideBSL(){
        Debug.Log("Hiding BSL video");
        image.color = new Color(1,1,1,0);
        if(BSL_player.isPlaying) BSL_player.Pause();
        hideBSLButton.SetActive(false);
    }

    public void ShowBSL(){
        Debug.Log("Showing BSL video");
        image.color = new Color(1,1,1,1);
        if(!BSL_player.isPlaying) BSL_player.Play();
        hideBSLButton.SetActive(true);
    }
}
