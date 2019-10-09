using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvincibilityProgressBar : MonoBehaviour
{
    public Transform LoadingBar;
    public Transform TextPercentage;
    public GameObject powerupUIPlaceholder;
    // public Transform TextLoading;
    public Invincibility inv;

    public float currentLoadingAmount; 
    public float speed1 = 20.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        inv = transform.parent.parent.parent.GetComponent<Invincibility>();
        currentLoadingAmount = 100; 
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLoadingAmount >= 0 && (inv.activate == true) && inv.invincibilityTimer >= 0){
            
            currentLoadingAmount = (inv.invincibilityTimer * 100)/inv.maxTimeForInvincibility;
            TextPercentage.GetComponent<Text>().text = ((int)currentLoadingAmount).ToString() + "%";
            // TextLoading.gameObject.SetActive(true);
        }
        LoadingBar.GetComponent<Image>().fillAmount = (currentLoadingAmount/ 100); 
    }
}
