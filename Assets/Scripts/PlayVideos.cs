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
    public VideoPlayer martinCube;

    [Header("File hosting names")]
    public string khushURL;
    public string lesURL;
    public string martinURL;
    public string martinCubeURL;

    bool hasStarted = false;

    void Start()
    {
        khush.url = khushURL;
        les.url = lesURL;
        martin.url = martinURL;
        martinCube.url = martinCubeURL;

        khush.Prepare();
        les.Prepare();
        martin.Prepare();
        martinCube.Prepare();

        if(playOnStart){
            StartCoroutine(WaitThenPlay());
        }

    }

    void Update()
    {
        if(!hasStarted){
            if(Input.anyKey && khush.isPrepared && les.isPrepared && martin.isPrepared){
                Debug.Log("Starting videos");
                khush.Play();
                les.Play();
                martin.Play();
                martinCube.Play();
                hasStarted = true;
            }
        }
    }

    IEnumerator WaitThenPlay(){
        yield return new WaitForSeconds(2);
        khush.Play();
        les.Play();
        martin.Play();
        martinCube.Play();
    }
}
