using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class Powerup : MonoBehaviour
{
    public string powerupType;
    public bool isActive; //!isActive is passive powerup
    public bool activate; 
    protected int playerNum;
    protected gameController gc;
    protected List<GameObject> playerList; //to be used for passive powerups
    protected Rigidbody rb;
    protected bool onGround;

    //for speed
    public float maxTimeToSpeed = 3.0f;
    public float speedTimer;
    
    
    
    // OnAwake(){
    //     // setPowerup("jump", true, false);
    // }
    // protected void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.tag.Equals("snake"))
    //     {
    //         powerupType = setPowerupType();
    //         isActive = setIsActive();
    //     }
    // }
    public virtual void setPowerup(string _powerupType, bool _isActive, bool _activate){}
    

    public virtual void activateNow(){
       Debug.Log("powerup activated");
    }
    public virtual void deactivateNow(){
        Debug.Log("powerup deactivated");
    }
    
    //this method randomly sets certain powerups to active/passive when collected 
    public bool setIsActive(){
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

    //this method randomly sets the type of powerup when collected 
    public string setPowerupType(){ //to get a powerup type of jump:0, speed:1 etc etc
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
