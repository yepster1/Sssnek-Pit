using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Powerup : MonoBehaviour
{
    public string powerupType;
    public bool isActive; //!isActive is passive powerup
    public bool activateNow; 
    private int playerNum;
    private gameController gc;
    private List<GameObject> playerList;
    private Rigidbody rb;
    private float maxTimeToJump = 3f;
    private GameObject floor;
    
    private bool onGround;
    private Vector3 jump;
    private float jumpForce = 10.0f;
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;
    // Start is called before the first frame update
    void Start()
    {
        if (gc == null){
            gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        }
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 1.0f, 0.0f);
        playerList = gc.playerList; //to be used for passive powerup effects
        powerupType = "jump";
        isActive = setIsActive();
        activateNow = false;
       
        
    }

    void OnCollisionStay()
    {
        onGround = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (powerupType == "jump" && onGround && activateNow){
            rb.velocity = transform.up * jumpForce;
            onGround = false;
            activateNow = false;
            Debug.Log("jumping");
        }
        if (rb.velocity.y < 0 ){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier -1 ) * Time.smoothDeltaTime;
        }
        else if (rb.velocity.y > 0){
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier -1 ) * Time.smoothDeltaTime;
        }
        // if (powerupType == "speed" && isActive){ //active speed powerup

        // }
    }

    // void addJump(){

    // }
    // void addSpeed(){

    // }
    private bool setIsActive(){
        int[] activeTypes = new int[] { 0, 1};       //active/passive
        int length = activeTypes.Length;                //get the length 
        bool isActive = true;
        // Debug.Log("length of powerups: " + length);
        int myRandomIndex = Random.Range(0, length);    //let random select a range between zero and length
        int result = activeTypes[myRandomIndex];
        if (result == 0){
            isActive = false;
            
        }else{
            isActive = true;
        }
        return isActive;
    }
    private string setPowerupType(){ //to get a powerup type of jump:0, speed:1 etc etc
        int[] powerTypes = new int[] { 0, 1};       //int pertaining to which powerup to choose
        int length = powerTypes.Length;                 
        string powerupType = "";
        Debug.Log("length of powerups: " + length);
        int myRandomIndex = Random.Range(0, length);    //let random select a range between zero and length
        
        int result = powerTypes[myRandomIndex];
        if (result == 0){
            powerupType = "jump";
            
        }
        else if (result == 1){
            powerupType = "speed";
            
        }
        return powerupType;
    }

}
