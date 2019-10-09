using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("snake")){
            Pickup(other);
        }
    }

    protected void Pickup(Collider _player){
        Debug.Log("picking up powerup");
        GameObject player = _player.gameObject;
        PowerupManager powerupManager = player.GetComponent<PowerupManager>();
        if (powerupManager != null){
            
            Powerup newPowerup  = player.GetComponent<Powerup>();
            newPowerup.setPowerup(powerupManager.myPlayerNum, powerupManager.otherPlayers, true, false);
            
            if (powerupManager.powerups.Count == 1){
                if(powerupManager.powerupBeingUsed == false){
                    powerupManager.pushPowerup(newPowerup);
                }
            
            }
            else if(powerupManager.powerups.Count  > 1){
                //uncomment to add venom
                if(powerupManager.powerupBeingUsed == false){
                    powerupManager.popPowerup(); //remove current powerup
                    powerupManager.pushPowerup(newPowerup);
                }
            }

            Destroy(this.gameObject);  
        }else{
            Debug.Log("could not find powerupManager component");
        }
    }
}
