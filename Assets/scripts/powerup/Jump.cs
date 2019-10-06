using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Powerup
{
    
    private GameObject floor;
    private Vector3 jump;
    // private PowerupManager powerupManager;
    private List<GameObject> body;
    private Movement movement;

    public float timeBetweenJumps = 4f;
    protected float jumpTimer;
    public float timeSpentJumping;
    public float timeFromHeadToTail;
    public float tFHTTtimer; //used to allow entire snake to finish moving from head to tail
                            //before activating gravity again

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
        if (powerupManager == null){
            powerupManager = GetComponent<PowerupManager>();
        }
        if (movement == null){
            movement = GetComponent<Movement>();
            body = movement.body;
        }
        timeFromHeadToTail = calcTimeFromHeadToTail();
        
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
        // Debug.Log("jump deactivated");
        jumpTimer = 0.0f;
        activate = false;
        
    }
    public float calcTimeFromHeadToTail(){
        //in this case xyz should always be 1 
        float distanceToTravel = 1; 
        // Debug.Log("distance to travel: " +distanceToTravel);
        
        Vector3 vel = GetComponent<Rigidbody>().velocity;      //to get a Vector3 representation of the velocity
        float speed = vel.magnitude;  
        // Debug.Log ("speed : " + speed);
        float timeFromHeadToTail = distanceToTravel / speed;
        Debug.Log("time from head to tail: " +timeFromHeadToTail);
        return timeFromHeadToTail;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        jumpTimer += Time.smoothDeltaTime;
        if (powerupType == "jump" && onGround  && activate && jumpTimer > timeBetweenJumps ){
            Debug.Log("jumping");
            timeSpentJumping = 0.0f;
            tFHTTtimer = 0.0f;
            rb.velocity = transform.up * jumpForce;
            if (body.Count > 0){
                for (int i = 0 ; i < body.Count; i++){
                    if (body[i] != null){
                        body[i].GetComponent<Rigidbody>().useGravity = false;
                    }else {
                        break;
                    }
                    
                    // body[i].GetComponent<Rigidbody>().useGravity = true;
                }
            }
            // onGround = false;
            deactivateNow();
            // Invoke("powerupInUse", timeBetweenJumps);
        }
        tFHTTtimer += Time.smoothDeltaTime; 
        if (!onGround ){
            timeSpentJumping += Time.smoothDeltaTime;
            // Debug.Log("timeSpentJumping: " + timeSpentJumping);
        }
        if (tFHTTtimer >= timeFromHeadToTail){
            if (body.Count > 0){
                for (int i = 0 ; i < body.Count; i++){
                    if (body[i] != null){
                        body[i].GetComponent<Rigidbody>().useGravity = true;
                    }else {
                        break;
                    }
                    
                    
                }
            }
           
        }

        if (rb.velocity.y > 0){
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier -1 ) * Time.smoothDeltaTime;
        }
        else if (rb.velocity.y < 0 ){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier -1 ) * Time.smoothDeltaTime;
        }
        
    }
}
