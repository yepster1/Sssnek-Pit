using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public Stack<Powerup> powerups;
    public PowerupUIScript powerupUIScript;
    private Movement movement;
    public int myPlayerNum;
    public bool powerupBeingUsed;
    public GameObject venomPrefab;
    public List<GameObject> otherPlayers;

    public void SetPowerupManager(){
        movement = this.gameObject.GetComponent<Movement>();
        powerupUIScript = GetComponentInChildren<PowerupUIScript>();
        if(movement != null){
            if (movement.playerNumber == 0){ //name for head
                myPlayerNum = 0;
            }
            else if (movement.playerNumber == 1){
                myPlayerNum = 1;
            }
            else if (movement.playerNumber == 2){
                myPlayerNum = 2;
            }
            else if (movement.playerNumber == 3){
                myPlayerNum = 3;
            }
        }
        otherPlayers = new List<GameObject>();
        for (int i = 0 ; i < GameStateHandler.playerList.Count; i++){
            if(i != myPlayerNum){
                otherPlayers.Add(GameStateHandler.playerList[i]);
            }
        }
        Debug.Log ("for player: " + myPlayerNum);
        Debug.Log("other players are");
        for (int i = 0 ; i < otherPlayers.Count; i++){
            Debug.Log(otherPlayers[i] + ",");
        }

        powerups = new Stack<Powerup>();

        powerupBeingUsed = false;

        //leave jump uncommented - its the default
        
        // Powerup jumpDefault = this.gameObject.GetComponent<Jump>();
        // jumpDefault.setPowerup(myPlayerNum, otherPlayers, true, false);
        // jumpDefault.powerupType = "jump"; //just to change it back (default)
        // pushPowerup(jumpDefault);
        
        //uncomment one of these at a time to see how they work. 
        //will implement powerup pickup tonight

        // Powerup speedTest = this.gameObject.AddComponent<Speed>();
        // speedTest.setPowerup(myPlayerNum ,otherPlayers, true, false);
        // pushPowerup(speedTest);

        Powerup venomShoot = this.gameObject.AddComponent<VenomShootingScript>();
        venomShoot.setPowerup(myPlayerNum, otherPlayers ,true, false);
        venomShoot.powerupType = "venom";
        pushPowerup(venomShoot);
        
        // Powerup invincibilityTest = this.gameObject.AddComponent<Invincibility>();
        // invincibilityTest.setPowerup(myPlayerNum, otherPlayers, true, false);
        // invincibilityTest.powerupType = "invincibility";
        // pushPowerup(invincibilityTest);
        
       
    }
    
    public void pushPowerup(Powerup powerupToAdd){
        powerups.Push(powerupToAdd);
        powerupUIScript.setPowerupDisplay(powerupToAdd.powerupType);
        // Debug.Log("powerupToAdd: " + powerupToAdd.powerupType);
        // Debug.Log("powerups.Count()" + powerups.Count);
    }

    public Powerup popPowerup(){
        Debug.Log("powerups.Count()" + powerups.Count);
        return powerups.Pop();
        
    }

    public Powerup peekPowerup(){ //should only be used for jump
        return powerups.Peek();
    }
    public void powerupInUse(){ //sets it to the opposite
        
        powerupBeingUsed = ! powerupBeingUsed;
        Debug.Log("powerup in use: " + powerupBeingUsed);
    }
    
    public void activatePowerup()
	{   
        if (powerups.Count == 1 && !powerupBeingUsed){ //only for jump
            Powerup p = peekPowerup();
            if (p.powerupType == "jump" ){
                powerupInUse(); //sets powerupBeingUsed to true
                Jump jump = this.gameObject.GetComponent<Jump>();
                Debug.Log("powerup type " +jump.powerupType);
                jump.activateNow();
                Invoke("powerupInUse", jump.timeBetweenJumps);
            }
            if (p.powerupType == "venom" && !powerupBeingUsed){
                powerupInUse();
                VenomShootingScript venomShoot = this.gameObject.GetComponent<VenomShootingScript>();
                venomShoot.setVenom(myPlayerNum ,venomPrefab, otherPlayers);
                Debug.Log("powerup type " +venomShoot.powerupType);
                // venomShoot.setVenom(myPlayerNum, venomPrefab, otherPlayers);
                venomShoot.activateNow();
                Invoke("powerupInUse", venomShoot.maxTimeToShoot);
            }
        }else if (powerups.Count > 1 && !powerupBeingUsed){ //for every other powerup
            Powerup p = popPowerup();
            if (p.powerupType == "speed"){
                powerupInUse(); //sets powerupBeingUsed to true
                //  = this.gameObject.AddComponent<Speed>();
                Speed speed = this.gameObject.GetComponent<Speed>();
                Debug.Log("powerup type " +speed.powerupType);
                // powerupUIScript.setPowerupDisplay(speed.powerupType);
                speed.activateNow();
                Invoke("powerupInUse", speed.maxTimeToSpeed);
            }
            if (p.powerupType == "venom" && !powerupBeingUsed){
                powerupInUse();
                VenomShootingScript venomShoot = this.gameObject.GetComponent<VenomShootingScript>();
                venomShoot.setVenom(myPlayerNum ,venomPrefab, otherPlayers);
                Debug.Log("powerup type " +venomShoot.powerupType);
                // venomShoot.setVenom(myPlayerNum, venomPrefab, otherPlayers);
                venomShoot.activateNow();
                Invoke("powerupInUse", venomShoot.maxTimeToShoot);
            }
            if (p.powerupType == "invincibility" && !powerupBeingUsed){
                powerupInUse();
                Invincibility inv = this.gameObject.GetComponent<Invincibility>();
                // int _myPlayerNum , GameObject _head , Movement _movement, List<GameObject> _body
                // inv.setInvincibility(myPlayerNum, this.gameObject, movement, movement.body);
                inv.activateNow();
                Invoke("powerupInUse", inv.maxTimeForInvincibility);
            }
        }
        
	}

    
}
