using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Powerup
{
    
    private GameObject floor;
    private Vector3 jump;

    private float jumpForce = Config.PLAYER_JUMP_FORCE;
    private float fallMultiplier = Config.PLAYER_FALL_MULTIPLIER;
    private float lowJumpMultiplier = Config.PLAYER_LOW_JUMP_MULTIPLIER;
    
    
    public override void setPowerup(string _powerupType, bool _isActive, bool _activate){
        powerupType = _powerupType;
        isActive = _isActive;
        activate = _activate;
    }
    // Start is called before the first frame update
    void Start()
    {
        // if (gc == null){
        //     gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        // }
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 1.0f, 0.0f);
        playerList = GameStateHandler.playerList; //to be used for passive powerup effects
        powerupType = "jump";
        isActive = true;
        activate = false;
       
        
    }
    void OnCollisionStay()
    {
        onGround = true;
    }
    public override void activateNow(){
        Debug.Log("Jump Activated");
        activate = true;
    }

    public override void deactivateNow(){
        Debug.Log("jump deactivated");
        activate = false;
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (powerupType == "jump" && onGround && activate){
            Debug.Log("jumping");
            rb.velocity = transform.up * jumpForce;
            onGround = false;
            deactivateNow();
            
        }

        if (rb.velocity.y > 0){
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier -1 ) * Time.smoothDeltaTime;
        }
         else if (rb.velocity.y < 0 ){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier -1 ) * Time.smoothDeltaTime;
        }
        
    }
}
