using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    Light l;
    bool lightOn = false;

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Light>();
        StartCoroutine(flicker());
    }

    IEnumerator flicker(){
        while(true){
            float randChoice = Random.value;
            if(randChoice > 0.1f) yield return new WaitForSeconds(Random.Range(0.5f, 2));
            else yield return new WaitForSeconds(Random.Range(1,5));
            
            lightOn = !lightOn;
            setLight();
        }
    }

    void setLight(){
        if(lightOn) l.intensity = Random.Range(5,8);
        else l.intensity = 0;
    }
}
