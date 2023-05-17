using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraPoints : MonoBehaviour
{
    public MediaController mediaController;
    public Transform[] cameraPoints;

    public int currentPosIndex;
    public float slerpDuration;

    public bool controlsActive = false;

    Vector3 slerpTarget;

    int numOfPoints;

    float time;

    bool slerpActive;

    // Start is called before the first frame update
    void Start()
    {
        numOfPoints = cameraPoints.Length;
        currentPosIndex = 0;
        mediaController.MovedIndex();

        transform.position = cameraPoints[0].position;
        transform.rotation = cameraPoints[0].rotation;
        slerpActive = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(controlsActive){
            if(!slerpActive){
                if(Input.GetKeyDown(KeyCode.RightArrow)){
                    RightArrowPressed();
                }

                if(Input.GetKeyDown(KeyCode.LeftArrow)){
                    LeftArrowPressed();
                }
            }
            else{
                //Debug.Log("Slerp in progress");
            }
        }
    }

    public void RightArrowPressed(){
        if(currentPosIndex == 0) MoveRight();
        else MoveLeft();
    }

    public void LeftArrowPressed(){
        if(currentPosIndex == 0) MoveLeft();
        else MoveRight();
    }

    public void MoveRight(){
        if(!slerpActive){
            currentPosIndex++;
            if(currentPosIndex >= numOfPoints){
                currentPosIndex = 0;
            }
            mediaController.MovedIndex();
            //Debug.Log("Lerping forward to point: " + currentPosIndex);
            StartCoroutine(LerpToPoint(cameraPoints[currentPosIndex]));
        }
    }

    public void MoveLeft(){
        if(!slerpActive){
            currentPosIndex--;
            if(currentPosIndex < 0){
                currentPosIndex = numOfPoints - 1;
            }
            mediaController.MovedIndex();
            //Debug.Log("Lerping back to point: " + currentPosIndex);
            StartCoroutine(LerpToPoint(cameraPoints[currentPosIndex]));
        }
    }

    public void EnableControls(){
        controlsActive = true;
    }

    public void DisableControls(){
        controlsActive = false;
    }

    IEnumerator LerpToPoint(Transform target){
        slerpActive = true;
        time = 0;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        while(time < slerpDuration){
            transform.position = Vector3.Lerp(startPosition, target.position, time / slerpDuration);
            //transform.rotation =  Quaternion.Euler(Vector3.Slerp(startRotation, target.rotation.eulerAngles, time / slerpDuration));
            transform.rotation = Quaternion.Slerp(startRotation, target.rotation, time / slerpDuration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = target.position;
        transform.rotation = target.rotation;
        slerpActive = false;
    }
}
