using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JumpProgressBar : MonoBehaviour
{
    public Transform LoadingBar;
    public Transform TextPercentage;
    public GameObject powerupUIPlaceholder;
    public Jump jump;
    // public Transform TextLoading;

    public float currentLoadingAmount;
    public float timeBetweenJumps; 
    public float speed;
    
    void Start(){
        if (this.gameObject.activeSelf == true){
            jump = transform.parent.parent.parent.GetComponent<Jump>();
            currentLoadingAmount = jump.jumpTimer;
            timeBetweenJumps = jump.timeBetweenJumps;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLoadingAmount < timeBetweenJumps){
            // currentLoadingAmount += speed * Time.smoothDeltaTime;
            currentLoadingAmount = jump.jumpTimer;
            TextPercentage.GetComponent<Text>().text = ((int)currentLoadingAmount).ToString() + "%";
            // TextLoading.gameObject.SetActive(true);
        }else if (currentLoadingAmount >= timeBetweenJumps){
            // TextLoading.gameObject.SetActive(false);
            TextPercentage.GetComponent<Text>().text = "100%";
        }
        LoadingBar.GetComponent<Image>().fillAmount = currentLoadingAmount/ 100; 
    }
    // void updatePos(Vector3 position){

    // }

    
}
