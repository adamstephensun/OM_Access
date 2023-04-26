using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideos : MonoBehaviour
{

    public bool turnOn = false;

    public VideoPlayer khush;
    public VideoPlayer les;
    public VideoPlayer martin;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitThenPlay());
    }

    // Update is called once per frame
    void Update()
    {
        if(turnOn){
            khush.Play();
            les.Play();
            martin.Play();
            turnOn = false;
        }
    }

    IEnumerator WaitThenPlay(){
        yield return new WaitForSeconds(2);
        khush.Play();
        les.Play();
        martin.Play();
    }
}
