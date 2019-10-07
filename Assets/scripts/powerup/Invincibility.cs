using System.Collections;
using System; //for int parsing
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : Powerup
{
    private GameObject head;
    private Collider headCollider;
    private List<GameObject> body;
    public int myPlayerNum;
    private Movement movement;
    
    List <Collision> currentCollisions = new List <Collision> (); //used to test collisions with other objects
    
    public float maxTimeForInvincibility = 5f;
    public float invincibilityTimer;
    // Start is called before the first frame update
    void Start(){
        if (powerupManager == null){
            powerupManager = GetComponent<PowerupManager>();
            powerupUIScript = GetComponentInChildren<PowerupUIScript>();
            
        }
        if (movement == null){
            movement = GetComponent<Movement>();
            head = movement.head;
            body = movement.body;
            myPlayerNum = movement.playerNumber;
        }
        Debug.Log("invincibility set");
    }
    // public void setInvincibility(int _myPlayerNum , GameObject _head , Movement _movement, List<GameObject> _body){
    //     head = _head;
    //     body = movement.body;
        
    // }

    public override void activateNow(){
        invincibilityTimer = maxTimeForInvincibility;
        activate = true;
    }
    public override void deactivateNow(){
        powerupUIScript.setPowerupDisplay("jump");
        activate = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activate && invincibilityTimer >= 0){
            Debug.Log("invincibility activated");
            if (head != null){
                if (body.Count > 0){
                    for (int i = 0; i <  body.Count; i++){
                        // int j = GameStateHandler.playerList[playerToHit].GetComponent<Movement>().body.Count-1 ; j > tailNum ; j--  
                        // body[i].GetComponent<Rigidbody>().useGravity = false;
                        if (body[i] != null){
                            body[i].GetComponent<Rigidbody>().useGravity = false;
                            body[i].GetComponent<Collider>().enabled = false;
                        }else {
                            break;
                        }
                        
                    }
                }
                Debug.Log("disabling head collider");
                head.GetComponent<Rigidbody>().useGravity = false;
                head.GetComponent<Collider>().enabled = false;
            }
            invincibilityTimer -= Time.smoothDeltaTime;
        }else if(invincibilityTimer < 0 && activate){
            if (head != null){
                head.GetComponent<Collider>().enabled = true;
                head.GetComponent<Rigidbody>().useGravity = true;
                if (body.Count > 0){
                    for (int i = 0 ; i < body.Count; i++){
                        if (body[i] != null){
                            body[i].GetComponent<Collider>().enabled = true;
                            body[i].GetComponent<Rigidbody>().useGravity = true;
                        }else {
                            break;
                        }
                        
                        // body[i].GetComponent<Rigidbody>().useGravity = true;
                    }
                }
                
            }
            Debug.Log("***invincibility deactivated");
            deactivateNow();
        } 
    }
    
}
