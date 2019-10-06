using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JumpProgressBar : MonoBehaviour
{
    public Transform LoadingBar;
    public Transform TextPercentage;
    public GameObject powerupUIPlaceholder;
    // public Transform TextLoading;

    public float currentAmount; 
    public float speed;
    
    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmount < 100){
            currentAmount += speed * Time.smoothDeltaTime;
            TextPercentage.GetComponent<Text>().text = ((int)currentAmount).ToString() + "%";
            // TextLoading.gameObject.SetActive(true);
        }else{
            // TextLoading.gameObject.SetActive(false);
            TextPercentage.GetComponent<Text>().text = "100%";
        }
        LoadingBar.GetComponent<Image>().fillAmount = currentAmount/ 100; 
    }
    // void updatePos(Vector3 position){

    // }

    
}
