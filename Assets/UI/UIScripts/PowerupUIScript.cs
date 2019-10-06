using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupUIScript : MonoBehaviour
{
    // public List<Powerup> powerups;
    public JumpProgressBar JPB;
    // public SpeedProgressBar SPB;
    private Canvas canvas;
    private RectTransform canvasRT;
    private GameObject player;
    // Start is called before the first frame update
    void Start(){
        canvas = GetComponentInParent<Canvas>();
        
        canvasRT = canvas.GetComponent<RectTransform>();

        player = GameObject.Find("player0");
        if (player) Debug.Log("player0 found");
        // player = Game
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = setPos();
        this.gameObject.GetComponent<RectTransform>().localScale = setSize();
        // if(JPB == null){

        //     JPB = powerupManager.jumpProgressBar;
        //     // JPB.position = calcJPBPos();
        //     // JPB.rotation = gameObject.transform.rotation;
            
            
        //     // JPB.SetParent(canvas);
            
        //     JPB = Instantiate(JPB as GameObject, new Vector3(-60.0f,60.0f,0.0f), new Quaternion() );
        //     JPB.transform.parent = canvas.transform;
        //     if (JPB) Debug.Log("JPB set#############################");
        // }
    }
    public void setPowerupDisplay(string type){
        if(type == "jump"){
            // SPB.SetActive(false);
            JPB.gameObject.SetActive(true);
            // JPB.startFromZero();
        }else if (type == "speed"){
            JPB.gameObject.SetActive(false);
            // SPB.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 setPos(){
        
        float offsetPropX = -0.1f;
        float offsetPropY = 0.1f;
        // Vector3 position = 
        
        // Proportional distance based on canvas size
        
        float propDistX = canvasRT.rect.width * offsetPropX;
        float propDistY = canvasRT.rect.height * offsetPropY;
        Debug.Log("#########propDistX: " + propDistX);
 
        // Placement using transform pos, bounds and offset percentage based on canvas size
        // float offset = canvasRT.transform.position.y + propDist;
       
        Vector3 offsetPos = new Vector3(propDistX, propDistY, 0);
            
        return offsetPos;
        
    }

    private Vector3 setSize(){
        
        float match = 0.5f;
        float scaleFactor = (canvasRT.rect.width/ 800) * (1 - match) +
        (canvasRT.rect.height / 800) * (match);
        
        Vector3 offsetSize = new Vector3(scaleFactor, scaleFactor, 1);
        return offsetSize;
    }


}
