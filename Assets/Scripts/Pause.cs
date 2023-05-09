using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public GameObject pauseMenu;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(isPaused);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu(){
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
    }

    public void Quit(){
        Application.Quit();
    }
}
