using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public Stack<Powerup> powerups;
    private Movement movement;
    public int myPlayerNum;
    public bool powerupBeingUsed;
    public GameObject venomPrefab;
    public List<GameObject> otherPlayers;

    public void SetPowerupManager(){
        movement = this.gameObject.GetComponent<Movement>();
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
        powerups = new Stack<Powerup>();

        Powerup jumpDefault = this.gameObject.AddComponent<Jump>();
        jumpDefault.setPowerup("jump", myPlayerNum, true, false);
        pushPowerup(jumpDefault);
        
        powerupBeingUsed = false;

        Powerup speedTest = this.gameObject.AddComponent<Speed>();
        speedTest.setPowerup("speed", myPlayerNum ,true, false);
        pushPowerup(speedTest);

        Powerup venomShoot = this.gameObject.AddComponent<VenomShootingScript>();
        venomShoot.setPowerup("venom", myPlayerNum, true, false);
        pushPowerup(venomShoot);
        
        
       
    }
    
    public void pushPowerup(Powerup powerupToAdd){
        powerups.Push(powerupToAdd);
        // Debug.Log("powerupToAdd: " + powerupToAdd.powerupType);
        // Debug.Log("powerups.Count()" + powerups.Count);
    }

    public Powerup popPowerup(){
        Debug.Log("powerups.Count()" + powerups.Count);
        return powerups.Pop();
        
    }

    public Powerup peekPowerup(){
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
                Jump jump = (Jump)p;
                Debug.Log("powerup type " +jump.powerupType);
                jump.activateNow();
                Invoke("powerupInUse", jump.timeBetweenJumps);
            }
        }else if (powerups.Count > 1 && !powerupBeingUsed){ //for every other powerup
            Powerup p = popPowerup();
            if (p.powerupType == "speed"){
                powerupInUse(); //sets powerupBeingUsed to true
                Speed speed = (Speed)p;
                Debug.Log("powerup type " +speed.powerupType);
                speed.activateNow();
                Invoke("powerupInUse", speed.maxTimeToSpeed);
            }
            if (p.powerupType == "venom" && !powerupBeingUsed){
                powerupInUse();
                VenomShootingScript venomShoot = (VenomShootingScript)p;
                Debug.Log("powerup type " +venomShoot.powerupType);
                
                venomPrefab = Instantiate(venomPrefab as GameObject, transform.position + transform.forward, transform.rotation);
                Debug.Log("movement.playerNum: " + movement.playerNumber);
                venomShoot.setVenom(myPlayerNum, venomPrefab, otherPlayers);
                venomShoot.activateNow();
                Invoke("powerupInUse", venomShoot.maxTimeToShoot);
            }
        }
        
	}

    
}
