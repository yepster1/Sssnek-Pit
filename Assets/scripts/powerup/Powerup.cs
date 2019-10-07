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
    // public static List<GameObject> otherPlayers; //to be used for passive powerups
    public Rigidbody rb;
    public bool onGround;
    private GameObject player;
    protected PowerupManager powerupManager;
    protected PowerupUIScript powerupUIScript;
    
    public void setPowerup(string _powerupType, int _playerNum, List<GameObject> _otherPlayers, bool _isActive, bool _activate){
        powerupType = _powerupType;
        isActive = _isActive;
        activate = _activate;
        playerNum = _playerNum;
        // otherPlayers = _otherPlayers;
        // for (int i = 0 ; i < _otherPlayers.Count;i++){
        //     otherPlayers[i]= _otherPlayers[i];
        // }
        
        
        // if (powerupUIScript == null)
        //  Debug.Log ("---------> XX powerupUIScript null");
        // if (powerupUIScript != null){
        //     Debug.Log ("--------->powerupUIScript not null");
        //     powerupUIScript.setPowerupDisplay(powerupType);
        //     if (powerupType == "jump"){
                
        //     // addPowerup(jump);
            

        //     }
        //     if (powerupType == "venom"){
        //         // Debug.Log("pNum: " + playerNum);
                
        //     }
        // }
        
    }
    
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
            
        }else if (result == 2){
            powerupType = "venom";
        }
        return powerupType;
    }
    

}
