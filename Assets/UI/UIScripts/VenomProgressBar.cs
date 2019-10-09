using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VenomProgressBar : MonoBehaviour
{
    public Transform LoadingBar;
    public Transform TextPercentage;
    public GameObject powerupUIPlaceholder;
    // public Transform TextLoading;
    public VenomShootingScript vShoot;

    public float currentLoadingAmount; 
    public float speed1 = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        vShoot = transform.parent.parent.parent.GetComponent<VenomShootingScript>();
        currentLoadingAmount = 100;   
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLoadingAmount >= 0 && (vShoot.activate == true) && vShoot.shootTimer >  0){
            
            currentLoadingAmount = (vShoot.shootTimer * 100)/vShoot.maxTimeToShoot;
            TextPercentage.GetComponent<Text>().text = ((int)currentLoadingAmount).ToString() + "%";
            // TextLoading.gameObject.SetActive(true);
        }
        
        LoadingBar.GetComponent<Image>().fillAmount = (currentLoadingAmount/ 100);
    }
}
