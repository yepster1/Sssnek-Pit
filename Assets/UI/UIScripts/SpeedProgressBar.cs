using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeedProgressBar : MonoBehaviour
{
    public Transform LoadingBar;
    public Transform TextPercentage;
    public GameObject powerupUIPlaceholder;
    // public Transform TextLoading;
    public Speed speed;

    public float currentLoadingAmount; 
    public float speed1 = 20.0f;
    
    void Start(){
       speed = transform.parent.parent.parent.GetComponent<Speed>();
       currentLoadingAmount = 100; 
       
    }
    // public void startFromZero(){
    //     currentLoadingAmount = 0;
    // }
    // Update is called once per frame
    void Update()
    {
        if(currentLoadingAmount >= 0 && (speed.activate == true) && speed.speedTimer >  0){
            
            currentLoadingAmount = (speed.speedTimer * 100)/speed.maxTimeToSpeed;
            TextPercentage.GetComponent<Text>().text = ((int)currentLoadingAmount).ToString() + "%";
            // TextLoading.gameObject.SetActive(true);
        }
        // if ( currentLoadingAmount >= 0 && speed.speedTimer > speed.maxTimeToSpeed){
        //     currentLoadingAmount -= ((speed.speedTimer/speed.maxTimeToSpeed));
        // }
        // else if (currentLoadingAmount <= 0 && speed.speedTimer <  speed.maxTimeToSpeed){
        //     Debug.Log("current loading amount over 100%%%%%%%%%");
        //     currentLoadingAmount = 0;
            
        //     // TextLoading.gameObject.SetActive(false);
        //     // TextPercentage.GetComponent<Text>().text = "100%";
        // }
       
        LoadingBar.GetComponent<Image>().fillAmount = (currentLoadingAmount/ 100); 
    }
    // void updatePos(Vector3 position){

    // }

    
}
