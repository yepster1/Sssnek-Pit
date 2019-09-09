using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : Powerup
{
    
    public float maxSpeed = Config.MAX_PLAYER_SPEED * 1.3f;
    public float minSpeed = Config.MIN_PLAYER_SPEED * 1.0f;
    // private GameObject floor;
    // private Vector3 jump;

    private float speedUp = Config.PLAYER_SPEED;
    // private float xr = Config.PLAYER_FALL_MULTIPLIER;
    // private float xer = Config.PLAYER_LOW_JUMP_MULTIPLIER;
    public override void setPowerup(string _powerupType, bool _isActive, bool _activate){
        powerupType = _powerupType;
        isActive = _isActive;
        activate = _activate;
    }
    // Start is called before the first frame update
    void Start()
    {
        // playerList = GameStateHandler.playerList; //to be used for passive powerup effects
        powerupType = "speed";
        // isActive = true;
        activate = false;
        // maxSpeed;
        // minSpeed;
    }
    void OnCollisionStay()
    {
        onGround = true;
    }

    public override void activateNow(){
        Debug.Log("Speed Activated");
        speedTimer = 0.0f;
        activate = true;
    }

    public override void deactivateNow(){
        Debug.Log("Speed deactivated");
        activate = false;
    }
    
    void FixedUpdate()
    {
        if (powerupType == "speed" && onGround && activate && speedTimer <  maxTimeToSpeed){
            if (this.gameObject.tag == "snake"){
                Debug.Log("speeding");
                
                transform.Translate(transform.forward * maxSpeed * Time.deltaTime, Space.World);
                gameObject.GetComponent<Movement>().moveMyTail(maxSpeed, minSpeed);
                
            }
        }else if(speedTimer > maxTimeToSpeed && activate){
            deactivateNow();
            if (this.gameObject.tag == "snake"){
                this.gameObject.GetComponent<Movement>().MaxSpeed = Config.MAX_PLAYER_SPEED;
                this.gameObject.GetComponent<Movement>().MinSpeed = Config.MIN_PLAYER_SPEED;
                
                
            }
            
        }
        speedTimer += Time.smoothDeltaTime;
    }
}
