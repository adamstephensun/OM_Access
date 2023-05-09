using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideos : MonoBehaviour
{

    public bool playOnStart = false;

    [Header("Video players")]
    public VideoPlayer khush;
    public VideoPlayer les;
    public VideoPlayer martin;

    [Header("File hosting names")]
    public string khushName;
    public string lesName;
    public string martinName;

    public string filePrefix;
    public string fileSuffix;

    bool hasStarted = false;

    void Start()
    {
        if(playOnStart){
            StartCoroutine(WaitThenPlay());
        }

        //khush.url = filePrefix + khushName + fileSuffix;
        //les.url = filePrefix + lesName + fileSuffix;
        //martin.url = filePrefix + martinName + fileSuffix;
    }

    void Update()
    {
        if(!hasStarted){
            if(Input.anyKey){
                Debug.Log("Starting videos");
                khush.Play();
                les.Play();
                martin.Play();
                hasStarted = true;
            }
        }
    }

    IEnumerator WaitThenPlay(){
        yield return new WaitForSeconds(2);
        khush.Play();
        les.Play();
        martin.Play();
    }
}
