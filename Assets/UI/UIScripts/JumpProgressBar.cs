﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JumpProgressBar : MonoBehaviour
{
    public Transform LoadingBar;
    public Transform TextPercentage;
    public GameObject powerupUIPlaceholder;
    // public Transform TextLoading;
    public Jump jump;

    public float currentLoadingAmount; 
    public float speed;
    
    void Start(){
       jump = transform.parent.parent.parent.GetComponent<Jump>(); 
       currentLoadingAmount = 0.0f;
    }
    // public void startFromZero(){
    //     currentLoadingAmount = 0;
    // }
    // Update is called once per frame
    void Update()
    {
        if (jump != null){
            if(currentLoadingAmount < 100 && jump.jumpTimer < jump.timeBetweenJumps){
            
                currentLoadingAmount += jump.jumpTimer;
                TextPercentage.GetComponent<Text>().text = ((int)currentLoadingAmount).ToString() + "%";
                // TextLoading.gameObject.SetActive(true);
            }
            else if (currentLoadingAmount >= 100 && jump.jumpTimer >= jump.timeBetweenJumps){
                // Debug.Log("current loading amount over 100%%%%%%%%%");
                currentLoadingAmount = 0.0f;
                
                // TextLoading.gameObject.SetActive(false);
                // TextPercentage.GetComponent<Text>().text = "100%";
            }
            
        
            LoadingBar.GetComponent<Image>().fillAmount = currentLoadingAmount/ 100; 
            }
        
    }
    // void updatePos(Vector3 position){

    // }

    
}
