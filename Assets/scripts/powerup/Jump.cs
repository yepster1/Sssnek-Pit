using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Powerup
{
    
    private GameObject floor;
    private Vector3 jump;
    // private PowerupManager powerupManager;


    public float timeBetweenJumps = 4f;
    protected float jumpTimer;

    private float jumpForce = Config.PLAYER_JUMP_FORCE;
    private float fallMultiplier = Config.PLAYER_FALL_MULTIPLIER;
    private float lowJumpMultiplier = Config.PLAYER_LOW_JUMP_MULTIPLIER;
    
    
    // public override void setPowerup(string _powerupType, bool _isActive, bool _activate){
    //     powerupType = _powerupType;
    //     isActive = _isActive;
    //     activate = _activate;
    // }
    // Start is called before the first frame update
    void Start()
    {
        // if (gc == null){
        //     gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        // }
        // if (powerupManager == null){
        powerupManager = GetComponent<PowerupManager>();
        // }
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 1.0f, 0.0f);
        // playerList = GameStateHandler.playerList; //to be used for passive powerup effects
        // powerupType = "jump";
        isActive = true;
        activate = false;
        jumpTimer = timeBetweenJumps;
       
        
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
        jumpTimer = 0.0f;
        activate = false;
        
    }
    // public void powerupInUse(){
    //     powerupManager.powerupInUse();
        
    // }
    // Update is called once per frame
    void FixedUpdate()
    {
        jumpTimer += Time.smoothDeltaTime;
        if (powerupType == "jump" && onGround  && activate && jumpTimer > timeBetweenJumps ){
            Debug.Log("jumping");
            rb.velocity = transform.up * jumpForce;
            onGround = false;
            deactivateNow();
            // Invoke("powerupInUse", timeBetweenJumps);
        }

        if (rb.velocity.y > 0){
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier -1 ) * Time.smoothDeltaTime;
        }
        else if (rb.velocity.y < 0 ){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier -1 ) * Time.smoothDeltaTime;
        }
        
    }
}
