using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused=false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gamePaused){
                Resume();
                Debug.Log("Resumed");

            }
            else{
                Pause();
                Debug.Log("Paused");
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale =1.0f;
        gamePaused = false;
    }
    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale =0.0f;
        gamePaused=true;
    }

    public void MainMenu(){
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    public void Quit(){
        Application.Quit();
    }
}
